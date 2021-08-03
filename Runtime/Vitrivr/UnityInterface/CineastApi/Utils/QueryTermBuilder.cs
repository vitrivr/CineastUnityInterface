using System.Collections.Generic;
using System.Linq;
using Org.Vitrivr.CineastApi.Model;
using UnityEngine;
using Vitrivr.UnityInterface.CineastApi.Model.Config;
using Vitrivr.UnityInterface.CineastApi.Model.Query;

namespace Vitrivr.UnityInterface.CineastApi.Utils
{
  public class QueryTermBuilder
  {
    /// <summary>
    /// Builds a Boolean <see cref="QueryTerm"/> consisting of a single condition.
    /// </summary>
    /// <param name="attribute">Attribute of condition</param>
    /// <param name="op">Relational operator specifying condition operation</param>
    /// <param name="values">Conditional values</param>
    /// <returns>The corresponding query term</returns>
    public static QueryTerm BuildBooleanTerm(string attribute, RelationalOperator op, params string[] values)
    {
      var expressionJson = BuildBooleanTermJson(attribute, op, values);
      var data = Base64Converter.JsonToBase64($"[{expressionJson}]");

      return new QueryTerm(QueryTerm.TypeEnum.BOOLEAN, data, new List<string> {"boolean"});
    }

    /// <summary>
    /// Builds a Boolean <see cref="QueryTerm"/> consisting of multiple conditions.
    /// </summary>
    /// <param name="conditions">Enumerable of conditions</param>
    /// <returns>The corresponding query term</returns>
    public static QueryTerm BuildBooleanTerm(
      IEnumerable<(string attribute, RelationalOperator op, string[] values)> conditions)
    {
      var conditionsJson = conditions.Select(c => BuildBooleanTermJson(c.attribute, c.op, c.values));
      var data = Base64Converter.JsonToBase64($"[{string.Join(",", conditionsJson)}]");

      return new QueryTerm(QueryTerm.TypeEnum.BOOLEAN, data, new List<string> {"boolean"});
    }

    /// <summary>
    /// Converts the given Boolean query term condition parameters into the expected JSON format.
    /// </summary>
    /// <param name="attribute">Attribute of condition</param>
    /// <param name="op">Relational operator specifying condition operation</param>
    /// <param name="values">Conditional values</param>
    /// <returns>The condition as JSON string</returns>
    public static string BuildBooleanTermJson(string attribute, RelationalOperator op, params string[] values)
    {
      var attributeJson = $"\"attribute\":\"{attribute}\"";
      var operatorJson = $"\"operator\":\"{op.ToString().ToUpper()}\"";
      var valuesString = values.Length == 1 ? $"\"{values[0]}\"" : $"[\"{string.Join("\",\"", values)}\"]";
      var valuesJson = $"\"values\":{valuesString}";

      return $"{{{attributeJson},{operatorJson},{valuesJson}}}";
    }

    /// <summary>
    /// Builds a <see cref="QueryTerm"/> of type IMAGE with category edge.
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
    /// Builds a <see cref="QueryTerm"/> of type IMAGE with category edge.
    /// </summary>
    /// <param name="image">Image to use for query</param>
    /// <returns>The corresponding query term</returns>
    public static QueryTerm BuildEdgeTerm(Texture2D image)
    {
      var encodedImage = Base64Converter.PNGPrefix + Base64Converter.ImageToBase64PNG(image);
      return BuildEdgeTerm(encodedImage);
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

    /// <summary>
    /// Builds a <see cref="QueryTerm"/> of type IMAGE with category global color
    /// </summary>
    /// <param name="image">Image to use for query</param>
    /// <returns>The corresponding query term</returns>
    public static QueryTerm BuildGlobalColorTerm(Texture2D image)
    {
      var encodedImage = Base64Converter.PNGPrefix + Base64Converter.ImageToBase64PNG(image);
      return BuildGlobalColorTerm(encodedImage);
    }

    /// <summary>
    /// Builds a <see cref="QueryTerm"/> of type IMAGE with given categories
    /// </summary>
    /// <param name="data">Base64 encoded image</param>
    /// <param name="categories">A list of categories</param>
    /// <returns>The corresponding query term</returns>
    public static QueryTerm BuildImageTermForCategories(string data, List<string> categories)
    {
      return new QueryTerm(QueryTerm.TypeEnum.IMAGE, data, categories);
    }

    /// <summary>
    /// Builds a <see cref="QueryTerm"/> of type IMAGE with given categories
    /// </summary>
    /// <param name="image">Image to use for query</param>
    /// <param name="categories">A list of categories</param>
    /// <returns>The corresponding query term</returns>
    public static QueryTerm BuildImageTermForCategories(Texture2D image, List<string> categories)
    {
      var encodedImage = Base64Converter.PNGPrefix + Base64Converter.ImageToBase64PNG(image);
      return BuildImageTermForCategories(encodedImage, categories);
    }

    public static QueryTerm BuildLocationTerm(double latitude, double longitude)
    {
      var qt = new QueryTerm(
        QueryTerm.TypeEnum.LOCATION,
        $"[{latitude},{longitude}]",
        new List<string>
          {CineastConfigManager.Instance.Config.categoryMappings.mapping[CategoryMappings.SPATIAL_CATEGORY]});
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
        Base64Converter.JsonPrefix + tags,
        new List<string>
          {CineastConfigManager.Instance.Config.categoryMappings.mapping[CategoryMappings.TAGS_CATEGORY]});
      return qt;
    }

    /// <summary>
    /// Builds a <see cref="QueryTerm"/> of type TAG with category tags
    /// </summary>
    /// <param name="tags">List of (tag ID, tag name) pairs</param>
    /// <returns>The corresponding query term for the given tags string</returns>
    public static QueryTerm BuildTagTerm(IEnumerable<(string id, string name)> tags)
    {
      var tagStrings = tags.Select(tag =>
        $"{{\"id\":\"{tag.id}\",\"name\":\"{tag.name}\",\"description\":\"\"}}");

      var tagList = $"[{string.Join(",", tagStrings)}]";

      var qt = new QueryTerm(QueryTerm.TypeEnum.TAG,
        Base64Converter.JsonToBase64(tagList),
        new List<string>
          {CineastConfigManager.Instance.Config.categoryMappings.mapping[CategoryMappings.TAGS_CATEGORY]});
      return qt;
    }

    /// <summary>
    /// Builds a <see cref="QueryTerm"/> of type TEXT with the specified categories
    /// </summary>
    /// <param name="text">Text to search for</param>
    /// <param name="categories">List of categories to search</param>
    /// <returns>The corresponding query term for the given text string</returns>
    public static QueryTerm BuildTextTerm(string text, List<string> categories)
    {
      var qt = new QueryTerm(QueryTerm.TypeEnum.TEXT,
        text,
        categories);
      return qt;
    }

    /// <summary>
    /// Builds a <see cref="QueryTerm"/> of type TEXT with the specified categories
    /// </summary>
    /// <param name="text">Text to search for</param>
    /// <param name="ocr">Whether ocr data should be searched</param>
    /// <param name="asr">Whether asr data should be searched</param>
    /// <param name="scenecaption">Whether scene caption data should be searched</param>
    /// <returns>The corresponding query term for the given text string</returns>
    public static QueryTerm BuildTextTerm(string text, bool ocr = false, bool asr = false, bool scenecaption = false)
    {
      var categories = new List<string>();

      if (ocr)
      {
        categories.Add("ocr");
      }

      if (asr)
      {
        categories.Add("asr");
      }

      if (scenecaption)
      {
        categories.Add("scenecaption");
      }

      var qt = new QueryTerm(QueryTerm.TypeEnum.TEXT,
        text,
        categories);
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