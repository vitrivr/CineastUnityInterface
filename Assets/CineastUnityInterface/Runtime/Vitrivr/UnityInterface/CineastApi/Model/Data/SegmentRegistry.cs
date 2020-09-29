using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Org.Vitrivr.CineastApi.Model;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Model.Data
{
  public static class SegmentRegistry
  {
    private static readonly ConcurrentDictionary<string, SegmentData> Registry =
      new ConcurrentDictionary<string, SegmentData>();

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
    /// Batch fetches data for the given segments.
    /// </summary>
    /// <param name="segments">The segments for which to fetch the data.</param>
    /// <returns></returns>
    public static async Task BatchFetchSegmentData(List<SegmentData> segments)
    {
      var uninitializedSegments = segments.Where(segment => !segment.IsInitialized()).ToList();
      if (uninitializedSegments.Count == 0)
      {
        // All segments already initialized
        return;
      }

      var segmentIds = uninitializedSegments.Select(segment => segment.GetId()).ToList();

      var results = await Task.Run(() => CineastWrapper.SegmentApi.FindSegmentByIdBatched(new IdList(segmentIds)));

      results.Content.ForEach(data => GetSegment(data.SegmentId).Initialize(data));
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
  }
}