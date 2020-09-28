using System;
using System.Collections.Generic;
using System.Linq;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Model.Data;
using Org.Vitrivr.CineastApi.Model;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Utils
{
  public class ResultUtils
  {
    /// <summary>
    /// Extracts an <see cref="IdList"/> from a given similarity result.
    /// </summary>
    /// <param name="result">The similarity result to extract the ids of</param>
    /// <param name="maxResults">Limit the number of ids</param>
    /// <returns>An id list of the results</returns>
    /// <exception cref="IndexOutOfRangeException">If <paramref name="result"/> is empty</exception>
    public static IdList IdsOf(SimilarityQueryResultBatch result, int maxResults = 1000)
    {
      if (result.Results.Count < 1)
      {
        throw new IndexOutOfRangeException("Cannot extract ids from nonexisting results object");
      }

      var theIds = result.Results[0].Content.Take(maxResults).Select(res => res.Key).ToList();
      return new IdList(theIds);
    }

    public static List<(SegmentData segment, double score)> ToSegmentData(SimilarityQueryResultBatch results)
    {
      return results.Results[0].Content
        .Select(result => (SegmentRegistry.GetSegment(result.Key), result.Value))
        .ToList();
    }
  }
}