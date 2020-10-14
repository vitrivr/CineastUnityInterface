using System.Collections.Generic;
using System.Linq;
using Org.Vitrivr.CineastApi.Model;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Utils
{
  public class QueryBuilder
  {
    /// <summary>
    /// Generic similarity query for given terms
    /// </summary>
    /// <param name="terms"></param>
    /// <returns></returns>
    public static SimilarityQuery BuildSimilarityQuery(params QueryTerm[] terms)
    {
      var qc = new QueryComponent(terms.ToList());
      var sq = new SimilarityQuery(new List<QueryComponent> {qc});
      return sq;
    }

    /// <summary>
    /// Convenience method to create spatial similarity query
    /// </summary>
    /// <param name="lat"></param>
    /// <param name="lon"></param>
    /// <returns></returns>
    public static SimilarityQuery BuildSpatialSimilarityQuery(double lat, double lon)
    {
      return BuildSimilarityQuery(QueryTermBuilder.BuildLocationTerm(lat, lon));
    }

    public static SimilarityQuery BuildTemporalSimilarityQuery(string utcTime)
    {
      return BuildSimilarityQuery(QueryTermBuilder.BuildTimeTerm(utcTime));
    }

    /// <summary>
    /// Convenience method to create tags only query
    /// </summary>
    /// <param name="tags">Tags to query</param>
    /// <returns></returns>
    public static SimilarityQuery BuildTagsSimilarityQuery(List<(string id, string name)> tags)
    {
      return BuildSimilarityQuery(QueryTermBuilder.BuildTagTerm(tags));
    }
  }
}