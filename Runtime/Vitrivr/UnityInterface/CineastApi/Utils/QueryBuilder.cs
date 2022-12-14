using System.Collections.Generic;
using System.Linq;
using Org.Vitrivr.CineastApi.Model;
using Vitrivr.UnityInterface.CineastApi.Model.Config;

namespace Vitrivr.UnityInterface.CineastApi.Utils
{
  public static class QueryBuilder
  {
    /// <summary>
    /// Generic similarity query for given terms
    /// </summary>
    /// <param name="terms">Terms to use for query.</param>
    public static SimilarityQuery BuildSimilarityQuery(params QueryTerm[] terms)
    {
      return new SimilarityQuery(terms.ToList());
    }

    /// <summary>
    /// Staged similarity query for the given stages.
    /// </summary>
    /// <param name="stages">Enumerable of stages, each containing the respective <see cref="QueryTerm"/>s.</param>
    public static StagedSimilarityQuery BuildStagedQuery(IEnumerable<List<QueryTerm>> stages)
    {
      return new StagedSimilarityQuery(stages.Select(terms => new QueryStage(terms)).ToList());
    }

    /// <summary>
    /// Temporal similarity query for the given staged temporal contexts.
    /// </summary>
    /// <param name="temporalContexts">Enumerable of temporally ordered enumerables containing stages, containing <see cref="QueryTerm"/>s.</param>
    /// <returns></returns>
    public static TemporalQuery BuildTemporalQuery(IEnumerable<IEnumerable<List<QueryTerm>>> temporalContexts)
    {
      return new TemporalQuery(temporalContexts.Select(BuildStagedQuery).ToList());
    }

    /// <summary>
    /// Convenience method to create spatial similarity query
    /// </summary>
    /// <param name="lat">Latitude in WSG85 degrees</param>
    /// <param name="lon">Longitude in WSG85</param>
    public static SimilarityQuery BuildSpatialSimilarityQuery(double lat, double lon)
    {
      return BuildSimilarityQuery(QueryTermBuilder.BuildLocationTerm(lat, lon));
    }

    /// <summary>
    /// Convenience method to create temporal similarity query
    /// </summary>
    /// <param name="utcTime">The timestamp in UTC time format</param>
    public static SimilarityQuery BuildTemporalSimilarityQuery(string utcTime)
    {
      return BuildSimilarityQuery(QueryTermBuilder.BuildTimeTerm(utcTime));
    }

    /// <summary>
    /// A simple Query-by-Example query, using Edge and Global color categories
    /// </summary>
    /// <param name="base64">Base64 encoded image</param>
    public static SimilarityQuery BuildSimpleQbEQuery(string base64)
    {
      return BuildSimilarityQuery(QueryTermBuilder.BuildImageTermForCategories(base64, new List<string>
      {
        CategoryMappings.GlobalColorCategory,
        CategoryMappings.EdgeCategory
      }));
    }

    /// <summary>
    /// Convenience method to create tags only query
    /// </summary>
    /// <param name="tags">Tags to query</param>
    public static SimilarityQuery BuildTagsSimilarityQuery(IEnumerable<(string id, string name)> tags)
    {
      return BuildSimilarityQuery(QueryTermBuilder.BuildTagTerm(tags));
    }
  }
}