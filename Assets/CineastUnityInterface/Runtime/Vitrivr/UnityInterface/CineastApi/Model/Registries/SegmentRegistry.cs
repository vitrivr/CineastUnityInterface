using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Org.Vitrivr.CineastApi.Model;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Model.Data
{
  /// <summary>
  /// Class for the instantiation and management of <see cref="SegmentData"/> objects.
  /// </summary>
  public static class SegmentRegistry
  {
    /// <summary>
    /// Internal storage / dict of ids  SegmentData
    /// </summary>
    private static readonly ConcurrentDictionary<string, SegmentData> Registry =
      new ConcurrentDictionary<string, SegmentData>();

    /// <summary>
    /// Retrieves the segment for the given ID.
    /// If it does not exist, it is created uninitialized.
    /// </summary>
    /// <param name="id">The segment id of the segment to retrieve</param>
    /// <returns>The <see cref="SegmentData"/> corresponding to the id (or a new one).</returns>
    public static SegmentData GetSegment(string id)
    {
      if (!Registry.ContainsKey(id))
      {
        var segment = new SegmentData(id);
        Registry.TryAdd(id, segment);
      }

      return Registry[id];
    }

    /// <summary>
    /// Returns segments corresponding to an object, by its id.
    /// <b>Note</b> This expects the segments to be loaded beforehand!
    /// </summary>
    /// <param name="objectId"></param>
    /// <returns></returns>
    public static List<SegmentData> GetSegmentsOf(string objectId)
    {
      // TODO Could also be a dedicated cache / dict
      return Registry.Values.Where(seg => seg.Initialized).Where(seg => seg.GetObjectId().Result == objectId).ToList();
    }

    /// <summary>
    /// Batch fetches data for the given segments.
    /// </summary>
    /// <param name="segments">The segments for which to fetch the data.</param>
    /// <returns></returns>
    public static async Task BatchFetchSegmentData(List<SegmentData> segments)
    {
      var uninitializedSegments = segments.Where(segment => !segment.Initialized).ToList();
      if (uninitializedSegments.Count == 0)
      {
        // All segments already initialized
        return;
      }

      var segmentIds = uninitializedSegments.Select(segment => segment.Id).ToList();

      var results = await Task.Run(() => CineastWrapper.SegmentApi.FindSegmentByIdBatched(new IdList(segmentIds)));

      results.Content.ForEach(data => GetSegment(data.SegmentId).Initialize(data));
    }

    /// <summary>
    /// Extracts the object of previously initialised segments.
    /// </summary>
    /// <returns></returns>
    public static List<ObjectData> GetObjects()
    {
      var oids = new HashSet<string>();
      Registry.Values.Where(segment => segment.Initialized).ToList().ForEach(segment => oids.Add(segment.GetObjectId().Result));
      return oids.Select(oid => new ObjectData(oid)).ToList();
    }

    /// <summary>
    /// Resets the registry by clearing all segment entries.
    ///
    /// ATTENTION: Be aware that this means all <see cref="SegmentData"/> objects in use must be released as well to
    /// ensure data integrity.
    /// </summary>
    public static void Reset()
    {
      Registry.Clear();
    }

    /// <summary>
    /// Whether the given segmentId is known to this registry
    /// </summary>
    /// <param name="segmentId"></param>
    /// <returns></returns>
    public static bool Exists(string segmentId)
    {
      return Registry.ContainsKey(segmentId);
    }

    /// <summary>
    /// Gets the corresponding object of the segment specified
    /// </summary>
    /// <param name="segmentId"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static ObjectData GetObjectOf(string segmentId)
    {
      // TODO might require a dedicated cache
      if (!Exists(segmentId))
      {
        throw new ArgumentException($"Cannot get object of unkown segment with id {segmentId}");
      }

      var segment = Registry[segmentId];
      if (segment.Initialized)
      {
        return ObjectRegistry.GetObject(segment.GetObjectId().Result);
      }
      else
      {
        Task.WaitAll(segment.GetObjectId());
        return ObjectRegistry.GetObject(segment.GetObjectId().Result);
      }
    }
    
    
  }
}