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
      var categories = new List<string> { "boolean" };
      const QueryTerm.TypeEnum type = QueryTerm.TypeEnum.BOOLEAN;

      return new QueryTerm(categories, type, data);
    }

    /// <summary>
    /// Builds a Boolean <see cref="QueryTerm"/> consisting of multiple conditions.
    /// </summary>
    /// <param name="conditions">Enumerable of conditions, where string conditions must already be in quotes</param>
    /// <returns>The corresponding query term</returns>
    public static QueryTerm BuildBooleanTerm(
      IEnumerable<(string attribute, RelationalOperator op, string[] values)> conditions)
    {
      var conditionsJson = conditions.Select(c => BuildBooleanTermJson(c.attribute, c.op, c.values));
      var data = Base64Converter.JsonToBase64($"[{string.Join(",", conditionsJson)}]");
      var categories = new List<string> { "boolean" };
      const QueryTerm.TypeEnum type = QueryTerm.TypeEnum.BOOLEAN;

      return new QueryTerm(categories, type, data);
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
      var valuesString = values.Length == 1 ? $"{values[0]}" : $"[{string.Join(",", values)}]";
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
      var qt = new QueryTerm(new List<string> { CategoryMappings.EdgeCategory }, QueryTerm.TypeEnum.IMAGE, data);
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
      var qt = new QueryTerm(new List<string> { CategoryMappings.GlobalColorCategory }, QueryTerm.TypeEnum.IMAGE, data);
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
      return new QueryTerm(categories, QueryTerm.TypeEnum.IMAGE, data);
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

    public static QueryTerm BuildLocationTerm(double latitude, double longitude,
      string spatialCategory = CategoryMappings.SpatialCategory)
    {
      return new QueryTerm(
        new List<string> { spatialCategory },
        QueryTerm.TypeEnum.LOCATION,
        $"[{latitude},{longitude}]");
    }

    /// <summary>
    ///  Builds a <see cref="QueryTerm"/> of type PARAMETRIZED_LOCATION with the given half similarity distance.
    /// </summary>
    /// <param name="latitude">Latitude of term</param>
    /// <param name="longitude">Longitude of term</param>
    /// <param name="halfSimilarityDistance">Distance at which similarity should equal 0.5</param>
    /// <param name="spatialCategory">Category to use for location term</param>
    /// <returns>The corresponding query term</returns>
    public static QueryTerm BuildLocationTerm(double latitude, double longitude, double halfSimilarityDistance,
      string spatialCategory = CategoryMappings.SpatialCategory)
    {
      return new QueryTerm(
        new List<string> { spatialCategory },
        QueryTerm.TypeEnum.PARAMETERISEDLOCATION,
        "{\"geoPoint\": " +
        "{\"latitude\": " +
        latitude +
        ", \"longitude\": " +
        longitude +
        "}, \"parameter\": " +
        halfSimilarityDistance +
        "}");
    }

    /// <summary>
    /// Builds a <see cref="QueryTerm"/> of type TAG with category tags
    /// </summary>
    /// <param name="tags">Base64 encoded JSON list of tags</param>
    /// <param name="tagsCategory">Category to use for tag term</param>
    /// <returns>The corresponding query term for the given tags string</returns>
    public static QueryTerm BuildTagTerm(string tags, string tagsCategory = CategoryMappings.TagsCategory)
    {
      return new QueryTerm(
        new List<string> { tagsCategory },
        QueryTerm.TypeEnum.TAG,
        Base64Converter.JsonPrefix + tags);
    }

    /// <summary>
    /// Builds a <see cref="QueryTerm"/> of type TAG with category tags
    /// </summary>
    /// <param name="tags">List of (tag ID, tag name) pairs</param>
    /// <param name="tagsCategory">Category to use for tag term</param>
    /// <returns>The corresponding query term for the given tags string</returns>
    public static QueryTerm BuildTagTerm(IEnumerable<(string id, string name)> tags,
      string tagsCategory = CategoryMappings.TagsCategory)
    {
      var tagStrings = tags.Select(tag =>
        $"{{\"id\":\"{tag.id}\",\"name\":\"{tag.name}\",\"description\":\"\"}}");

      var tagList = $"[{string.Join(",", tagStrings)}]";

      return new QueryTerm(
        new List<string> { tagsCategory },
        QueryTerm.TypeEnum.TAG,
        Base64Converter.JsonToBase64(tagList));
    }

    /// <summary>
    /// Builds a <see cref="QueryTerm"/> of type TEXT with the specified categories
    /// </summary>
    /// <param name="text">Text to search for</param>
    /// <param name="categories">List of categories to search</param>
    /// <returns>The corresponding query term for the given text string</returns>
    public static QueryTerm BuildTextTerm(string text, List<string> categories)
    {
      var qt = new QueryTerm(
        categories,
        QueryTerm.TypeEnum.TEXT,
        text);
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

      return new QueryTerm(
        categories,
        QueryTerm.TypeEnum.TEXT,
        text);
    }

    /// <summary>
    /// Builds a <see cref="QueryTerm"/> of type TIME with category temporal
    /// </summary>
    /// <param name="utcTime">The timestamp in utc time format</param>
    /// <param name="temporalCategory">Category to use for time term</param>
    /// <returns>The corresponding query term using the temporal category and time type</returns>
    public static QueryTerm BuildTimeTerm(string utcTime, string temporalCategory = CategoryMappings.TemporalCategory)
    {
      return new QueryTerm(new List<string> { temporalCategory }, QueryTerm.TypeEnum.TIME, utcTime);
    }
  }
}