using System.Collections.Generic;
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
      var qc = new QueryComponent(new List<QueryTerm>(terms));
      var sq = new SimilarityQuery(components: new List<QueryComponent>());
      sq.Components.Add(qc);
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
    
    /// <summary>
    /// Convenience method to create tags only query
    /// </summary>
    /// <param name="lat"></param>
    /// <param name="lon"></param>
    /// <returns></returns>
    public static SimilarityQuery BuildTagsQuery(List<(string id, string name)> tags)
    {
      return BuildSimilarityQuery(QueryTermBuilder.BuildTagTerm(tags));
    }
    
  }
}