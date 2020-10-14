using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Utils;
using Org.Vitrivr.CineastApi.Model;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Model.Data
{
  /// <summary>
  /// Data object containing all information associated with an executed query.
  /// </summary>
  [Serializable]
  public class QueryResponse
  {
    /// <summary>
    /// The executed query leading to the stored results.
    /// </summary>
    public readonly SimilarityQuery query;

    /// <summary>
    /// Dictionary of result lists by result category.
    /// </summary>
    public readonly Dictionary<string, List<ScoredSegment>> results;


    /// <param name="query">Query to store data for</param>
    /// <param name="results">Dictionary of results by result category</param>
    public QueryResponse(SimilarityQuery query, Dictionary<string, List<ScoredSegment>> results)
    {
      this.query = query;
      this.results = results;
    }

    /// <summary>
    /// Batch fetches segment data for the top scoring number of segments in the results set.
    /// </summary>
    /// <param name="number">Number of top scoring segments from each result category to prefetch data for</param>
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

    public List<ScoredSegment> GetMeanFusionResults()
    {
      return ResultUtils.MeanScoreFusion(results);
    }
  }
}