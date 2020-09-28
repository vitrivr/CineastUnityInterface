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

    public static async Task BatchFetchSegmentData(List<SegmentData> segments)
    {
      var uninitializedSegments = segments.Where(segment => !segment.IsInitialized());
      var segmentIds = uninitializedSegments.Select(segment => segment.GetId()).ToList();

      var results = await Task.Run(() => CineastWrapper.SegmentApi.FindSegmentByIdBatched(new IdList(segmentIds)));

      results.Content.ForEach(data => GetSegment(data.SegmentId).Initialize(data));
    }
  }
}