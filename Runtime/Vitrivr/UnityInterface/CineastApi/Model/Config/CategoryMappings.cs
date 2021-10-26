using System;
using System.Collections.Generic;

namespace Vitrivr.UnityInterface.CineastApi.Model.Config
{
  
  /// <summary>
  /// Holds mappings to the required Cineast categories
  /// </summary>
  [Serializable]
  public class CategoryMappings
  {

    public const string SPATIAL_CATEGORY = "spatialdistance";
    public const string TEMPORAL_CATEGORY = "temporaldistance";
    public const string SPATIOTEMPORAL_CATEGORY = "spatiotemporal";

    public const string GLOBAL_COLOR_CATEGORY = "globalcolor";
    public const string EDGE_CATEGORY = "edge";

    public const string TAGS_CATEGORY = "tags";
    
    /// <summary>
    /// The mappings. This class provides *_NAME as mapping name and
    /// the result should be used as categories.
    /// </summary>
    public Dictionary<string,string> mapping = new Dictionary<string, string>();

    public CategoryMappings()
    {
      
    }
    
    

    
    /// <summary>
    /// Returns the default mappings.
    /// They are as follows:
    /// <ul>
    /// <li>spatialdistance maps to spatialdistance</li>
    /// <li>temporaldistance maps to temporaldistance</li>
    /// <li>spatiotemporal maps to spatiotemporal</li>
    /// <li>globalcolor maps to globalcolor</li>
    /// <li>edge maps to edge</li>
    /// </ul>
    /// </summary>
    /// <returns></returns>
    public static CategoryMappings GetDefault()
    {
      var m = new CategoryMappings();
      m.mapping.Add(SPATIAL_CATEGORY, SPATIAL_CATEGORY);
      m.mapping.Add(TEMPORAL_CATEGORY, TEMPORAL_CATEGORY);
      m.mapping.Add(SPATIOTEMPORAL_CATEGORY, SPATIOTEMPORAL_CATEGORY);
      m.mapping.Add(GLOBAL_COLOR_CATEGORY, GLOBAL_COLOR_CATEGORY);
      m.mapping.Add(EDGE_CATEGORY, EDGE_CATEGORY);
      m.mapping.Add(TAGS_CATEGORY, TAGS_CATEGORY);
      return m;
    }

  }
}