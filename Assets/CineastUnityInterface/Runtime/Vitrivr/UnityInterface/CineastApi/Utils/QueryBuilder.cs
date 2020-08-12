using System;
using System.Collections.Generic;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Model;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Models.Messages.Query;
using Org.Vitrivr.CineastApi.Model;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Utils
{
  public class QueryBuilder
  {
    
    /// <summary>
    /// Builds a <see cref="QueryTerm"/> of type TIME with category global color
    /// </summary>
    /// <param name="utcTime">The timestamp in utc time format</param>
    /// <returns>The corresponding query term using the temporal category and time type</returns>
    public static QueryTerm BuildTimeTerm(string utcTime)
    {
      var qt = new QueryTerm(QueryTerm.TypeEnum.TIME, utcTime, new List<string>());
      qt.Categories.Add(CineastConfigManager.Instance.Config.categoryMappings.mapping[CategoryMappings.TEMPORAL_CATEGORY]);
      return qt;
    }

    /// <summary>
    /// Builds a <see cref="QueryTerm"/> of type IMAGE with category global color
    /// </summary>
    /// <param name="data">Base64 encoded image</param>
    /// <returns>The corresponding query term</returns>
    public static QueryTerm BuildGlobalColorTerm(string data)
    {
      var qt = new QueryTerm(QueryTerm.TypeEnum.IMAGE, data, new List<string>());
      qt.Categories.Add(CineastConfigManager.Instance.Config.categoryMappings.mapping[CategoryMappings.GLOBAL_COLOR_CATEGORY]);
      return qt;
    }

    /// <summary>
    /// Builds a QueryTerm of type IMAGE with category edge.
    /// </summary>
    /// <param name="data">Base64 encoded image</param>
    /// <returns>The corresponding query term</returns>
    public static QueryTerm BuildEdgeTerm(string data)
    {
      var qt = new QueryTerm(QueryTerm.TypeEnum.IMAGE, data, new List<string>());
      qt.Categories.Add(CineastConfigManager.Instance.Config.categoryMappings.mapping[CategoryMappings.EDGE_CATEGORY]);
      return qt;
    }

    public static QueryTerm BuildLocationTerm(double latitude, double longitude)
    {
      var qt = new QueryTerm(
        QueryTerm.TypeEnum.LOCATION,
        String.Format("[{0},{1}]",latitude,longitude),
        new List<string>());
      qt.Categories.Add(CineastConfigManager.Instance.Config.categoryMappings.mapping[CategoryMappings.SPATIAL_CATEGORY]);
      return qt;
    }

    /// <summary>
    /// Generic similarity query for given terms
    /// </summary>
    /// <param name="terms"></param>
    /// <returns></returns>
    public static SimilarityQuery BuildSimilarityQuery(params QueryTerm[] terms)
    {
      var qc = new QueryComponent(new List<QueryTerm>(terms));
      var sq = new SimilarityQuery(components: new List<QueryComponent>(), messageType:SimilarityQuery.MessageTypeEnum.QSIM);
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
      return BuildSimilarityQuery(BuildLocationTerm(lat, lon));
    }
    
  }
}