using System;

namespace Vitrivr.UnityInterface.CineastApi.Model.Config
{
  /// <summary>
  /// Holds default Cineast categories
  /// </summary>
  [Serializable]
  public class CategoryMappings
  {
    public const string SpatialCategory = "spatialdistance";
    public const string TemporalCategory = "temporaldistance";
    public const string SpatiotemporalCategory = "spatiotemporal";

    public const string GlobalColorCategory = "globalcolor";
    public const string EdgeCategory = "edge";

    public const string TagsCategory = "tags";
  }
}