using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Org.Vitrivr.CineastApi.Model;
using Vitrivr.UnityInterface.CineastApi.Model.Data;

namespace Vitrivr.UnityInterface.CineastApi.Model.Registries
{
  /// <summary>
  /// Class for the instantiation and management of <see cref="SegmentData"/> objects.
  /// </summary>
  public static class SegmentRegistry
  {
    /// <summary>
    /// Internal storage / dict of ids &lt;-&gt; SegmentData
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
    /// </summary>
    /// <param name="objectId"></param>
    /// <returns></returns>
    public static async Task<List<SegmentData>> GetSegmentsOf(string objectId)
    {
      var results = await Task.Run(() => CineastWrapper.SegmentApi.FindSegmentByObjectId(objectId));
      var segmentDescriptors = results.Content;
      return segmentDescriptors.Select(descriptor =>
      {
        var segment = GetSegment(descriptor.SegmentId);
        if (!segment.Initialized)
        {
          segment.Initialize(descriptor);
        }

        return segment;
      }).ToList();
    }

    /// <summary>
    /// Batch fetches data for the given segments.
    /// </summary>
    /// <param name="segments">The segments for which to fetch the data.</param>
    public static async Task BatchFetchSegmentData(IEnumerable<SegmentData> segments)
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
    /// Batch fetches all uninitialized segments in the registry.
    /// </summary>
    public static async Task BatchFetchAll()
    {
      await BatchFetchSegmentData(Registry.Values);
    }

    /// <summary>
    /// Extracts the object of previously initialised segments.
    /// </summary>
    /// <returns></returns>
    public static List<ObjectData> GetObjects()
    {
      var oIds = new HashSet<string>();
      Registry.Values.Where(segment => segment.Initialized).ToList()
        .ForEach(segment => oIds.Add(segment.GetObjectId().Result));
      return oIds.Select(oid => new ObjectData(oid)).ToList();
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
    public static async Task<ObjectData> GetObjectOf(string segmentId)
    {
      // TODO might require a dedicated cache
      if (!Exists(segmentId))
      {
        throw new ArgumentException($"Cannot get object of unknown segment with id {segmentId}");
      }

      var segment = Registry[segmentId];
      if (segment.Initialized)
      {
        return ObjectRegistry.GetObject(segment.GetObjectId().Result);
      }

      var objectId = await segment.GetObjectId();
      return ObjectRegistry.GetObject(objectId);
    }
  }
}