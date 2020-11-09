using System;
using System.Collections.Generic;
using System.Linq;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Model.Data;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Model.Registries;
using Org.Vitrivr.CineastApi.Model;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Utils
{
  public static class ResultUtils
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
        throw new IndexOutOfRangeException("Cannot extract ids from nonexistent results object");
      }

      var theIds = result.Results[0].Content.Take(maxResults).Select(res => res.Key).ToList();
      return new IdList(theIds);
    }

    /// <summary>
    /// Converts a query result into a more easily processable list of <see cref="SegmentData"/> and score.
    /// </summary>
    /// <param name="results">The query results to convert</param>
    /// <param name="maxResults">The maximum number of results to include from each results category</param>
    /// <returns>Dictionary of results by result category</returns>
    public static Dictionary<string, List<ScoredSegment>> ToSegmentData(
      SimilarityQueryResultBatch results, int maxResults)
    {
      return results.Results.Where(similarityQueryResult => similarityQueryResult.Content.Count > 0)
        .ToDictionary(
          similarityQueryResult => similarityQueryResult.Category,
          similarityQueryResult => similarityQueryResult.Content.Take(maxResults)
            .Select(result => new ScoredSegment(SegmentRegistry.GetSegment(result.Key), result.Value))
            .ToList()
        );
    }

    public static List<ScoredSegment> ScoreFusion(Dictionary<string, List<ScoredSegment>> results,
      Func<double[], double> fusion)
    {
      if (results.Keys.Count < 2)
      {
        return results.Values.FirstOrDefault();
      }

      var categoryMaps = results.Select(entry =>
          entry.Value.ToDictionary(scoredSegment => scoredSegment.segment, scoredSegment => scoredSegment.score))
        .ToList();

      var segmentSet = categoryMaps.Aggregate(new HashSet<SegmentData>(), (set, map) =>
      {
        set.UnionWith(map.Keys);
        return set;
      });

      var mergedResults = segmentSet.Select(segment => new ScoredSegment(segment,
        fusion(
          categoryMaps.Select(map => map.TryGetValue(segment, out var score) ? score : 0.0)
            .ToArray()
        )
      )).ToList();

      mergedResults.Sort();

      return mergedResults;
    }

    public static List<ScoredSegment> MeanScoreFusion(Dictionary<string, List<ScoredSegment>> results)
    {
      return ScoreFusion(results, scores => scores.Average());
    }
  }
}