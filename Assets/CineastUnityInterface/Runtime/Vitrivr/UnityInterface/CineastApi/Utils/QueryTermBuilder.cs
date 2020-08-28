using System;
using System.Collections.Generic;
using System.Linq;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Model.Config;
using Org.Vitrivr.CineastApi.Model;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Utils
{
    public class QueryTermBuilder
    {
        /// <summary>
        /// Builds a QueryTerm of type IMAGE with category edge.
        /// </summary>
        /// <param name="data">Base64 encoded image</param>
        /// <returns>The corresponding query term</returns>
        public static QueryTerm BuildEdgeTerm(string data)
        {
            var qt = new QueryTerm(QueryTerm.TypeEnum.IMAGE, data, new List<string>());
            qt.Categories.Add(
                CineastConfigManager.Instance.Config.categoryMappings.mapping[CategoryMappings.EDGE_CATEGORY]);
            return qt;
        }

        /// <summary>
        /// Builds a <see cref="QueryTerm"/> of type IMAGE with category global color
        /// </summary>
        /// <param name="data">Base64 encoded image</param>
        /// <returns>The corresponding query term</returns>
        public static QueryTerm BuildGlobalColorTerm(string data)
        {
            var qt = new QueryTerm(QueryTerm.TypeEnum.IMAGE, data,
                new List<string>
                {
                    CineastConfigManager.Instance.Config.categoryMappings.mapping[
                        CategoryMappings.GLOBAL_COLOR_CATEGORY]
                });
            return qt;
        }

        public static QueryTerm BuildLocationTerm(double latitude, double longitude)
        {
            var qt = new QueryTerm(
                QueryTerm.TypeEnum.LOCATION,
                String.Format("[{0},{1}]", latitude, longitude),
                new List<string> {CineastConfigManager.Instance.Config.categoryMappings.mapping[CategoryMappings.SPATIAL_CATEGORY]});
            return qt;
        }

        /// <summary>
        /// Builds a <see cref="QueryTerm"/> of type TAG with category tags
        /// </summary>
        /// <param name="tags">Base64 encoded JSON list of tags</param>
        /// <returns>The corresponding query term for the given tags string</returns>
        public static QueryTerm BuildTagTerm(string tags)
        {
            var qt = new QueryTerm(QueryTerm.TypeEnum.TAG,
                "data:application/json;base64," + tags,
                new List<string> {CineastConfigManager.Instance.Config.categoryMappings.mapping[CategoryMappings.TAGS_CATEGORY]});
            return qt;
        }
        
        /// <summary>
        /// Builds a <see cref="QueryTerm"/> of type TAG with category tags
        /// </summary>
        /// <param name="tags">List of (tag ID, tag name) pairs</param>
        /// <returns>The corresponding query term for the given tags string</returns>
        public static QueryTerm BuildTagTerm(List<(string id, string name)> tags)
        {
            var tagStrings = tags.Select(tag => 
                $"{{\"id\":\"{tag.id}\",\"name\":\"{tag.name}\",\"description\":\"\"}}");

            var tagList = $"[{String.Join(",", tagStrings)}]";
            
            var qt = new QueryTerm(QueryTerm.TypeEnum.TAG,
                "data:application/json;base64," + StringConverter.ToBase64(tagList),
                new List<string> {CineastConfigManager.Instance.Config.categoryMappings.mapping[CategoryMappings.TAGS_CATEGORY]});
            return qt;
        }

        /// <summary>
        /// Builds a <see cref="QueryTerm"/> of type TIME with category temporal
        /// </summary>
        /// <param name="utcTime">The timestamp in utc time format</param>
        /// <returns>The corresponding query term using the temporal category and time type</returns>
        public static QueryTerm BuildTimeTerm(string utcTime)
        {
            var qt = new QueryTerm(QueryTerm.TypeEnum.TIME, utcTime,
                new List<string>
                {
                    CineastConfigManager.Instance.Config.categoryMappings.mapping[CategoryMappings.TEMPORAL_CATEGORY]
                });
            return qt;
        }
    }
}