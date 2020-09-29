using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Org.Vitrivr.CineastApi.Model;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Model.Data
{
  public class QueryData
  {
    public SimilarityQuery query;
    public List<(SegmentData segment, double score)> results;
    

    public QueryData(SimilarityQuery query, List<(SegmentData segment, double score)> results)
    {
      this.query = query;
      this.results = results;
    }

    /// <summary>
    /// Batch fetches segment data for the top scoring number of segments in the results set.
    /// </summary>
    /// <param name="number">Number of top scoring segments to prefetch data for</param>
    /// <returns></returns>
    public async Task Prefetch(int number)
    {
      var prefetchSegments = results.Take(number).Select(item => item.segment).ToList();

      await SegmentRegistry.BatchFetchSegmentData(prefetchSegments);
    }
  }
}
