using System.Collections.Generic;
using System.Linq;
using Org.Vitrivr.CineastApi.Model;
using Vitrivr.UnityInterface.CineastApi.Model.Config;

namespace Vitrivr.UnityInterface.CineastApi.Utils
{
  public class QueryBuilder
  {
    /// <summary>
    /// Generic similarity query for given terms
    /// </summary>
    /// <param name="terms"></param>
    public static SimilarityQuery BuildSimilarityQuery(params QueryTerm[] terms)
    {
      return new SimilarityQuery(terms.ToList());
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
        CineastConfigManager.Instance.Config.categoryMappings.mapping[
          CategoryMappings.GLOBAL_COLOR_CATEGORY],
        CineastConfigManager.Instance.Config.categoryMappings.mapping[
          CategoryMappings.EDGE_CATEGORY],
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