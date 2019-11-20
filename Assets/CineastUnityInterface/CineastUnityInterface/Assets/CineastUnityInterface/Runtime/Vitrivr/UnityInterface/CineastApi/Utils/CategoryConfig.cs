using System.Collections.Generic;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Utils
{
    /// <summary>
    /// Configuration model to map Cineast and GoFind categories
    /// </summary>
    [System.Serializable]
    public class CategoryConfig
    {
        /// <summary>
        /// The key to the spatial category.
        /// </summary>
        public const string SPATIAL_CATEGORY_KEY = "gofind.spatial";
        /// <summary>
        /// The key to the temporal category
        /// </summary>
        public const string TEMPROAL_CATEGORY_KEY = "gofind.temporal";
        
        /// <summary>
        /// The actual dictionary where the mapping of category-key to category name is stored
        /// </summary>
        public Dictionary<string, string> mapping = new Dictionary<string, string>();

        public string FindCategoryName(string key)
        {
            if (mapping.ContainsKey(key))
            {
                return mapping[key];
            }
            else
            {
                return null;
            }
        }
        
        // TODO Add QbE category stuff
    }
}