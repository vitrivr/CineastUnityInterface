using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Org.Vitrivr.CineastApi.Model;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Model.Data
{
  public class QueryData
  {
    public SimilarityQuery query;
    public Dictionary<string, List<(SegmentData segment, double score)>> results;


    public QueryData(SimilarityQuery query, Dictionary<string, List<(SegmentData segment, double score)>> results)
    {
      this.query = query;
      this.results = results;
    }

    /// <summary>
    /// Batch fetches segment data for the top scoring number of segments in the results set.
    /// </summary>
    /// <param name="number">Number of top scoring segments from each result category to prefetch data for</param>
    /// <returns></returns>
    public async Task Prefetch(int number)
    {
      // TODO: Prevent more than the number of segments to be prefetched in total
      var segmentSet = new HashSet<SegmentData>();
      foreach (var segmentList in results.Values)
      {
        segmentList.Take(number).Select(item => item.segment).ToList().ForEach(segment => segmentSet.Add(segment));
      }

      await SegmentRegistry.BatchFetchSegmentData(segmentSet.ToList());
    }
  }
}