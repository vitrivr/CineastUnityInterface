using System.Collections.Generic;
using System.Threading.Tasks;
using Org.Vitrivr.CineastApi.Api;
using Org.Vitrivr.CineastApi.Model;
using Vitrivr.UnityInterface.CineastApi.Model.Config;
using Vitrivr.UnityInterface.CineastApi.Model.Data;
using Vitrivr.UnityInterface.CineastApi.Model.Registries;
using Vitrivr.UnityInterface.CineastApi.Utils;

namespace Vitrivr.UnityInterface.CineastApi
{
  /// <summary>
  /// Wrapper for cineast
  /// </summary>
  public class CineastClient
  {
    public readonly ObjectApi ObjectApi;
    public readonly SegmentsApi SegmentsApi;
    public readonly SegmentApi SegmentApi;
    public readonly TagApi TagApi;
    public readonly MetadataApi MetadataApi;
    public readonly MiscApi MiscApi;

    public readonly CineastConfig CineastConfig;

    public readonly MultimediaRegistry MultimediaRegistry;

    public CineastClient(CineastConfig config)
    {
      CineastConfig = config;
      var apiConfig = config.GetApiConfig();
      ObjectApi = new ObjectApi(apiConfig);
      SegmentsApi = new SegmentsApi(apiConfig);
      SegmentApi = new SegmentApi(apiConfig);
      TagApi = new TagApi(apiConfig);
      MetadataApi = new MetadataApi(apiConfig);
      MiscApi = new MiscApi(apiConfig);

      MultimediaRegistry = new MultimediaRegistry(this);
    }

    /// <summary>
    /// Executes a <see cref="SimilarityQuery"/>, reduces the result set to the specified number of maximum results and
    /// prefetches data for the given number of segments.
    /// </summary>
    /// <param name="query">Query to execute</param>
    /// <param name="prefetch">Number of segments to prefetch data for</param>
    /// <returns><see cref="QueryResponse"/> for the query including the result list</returns>
    public async Task<QueryResponse> ExecuteQuery(SimilarityQuery query, int prefetch)
    {
      var queryResults = await SegmentsApi.FindSegmentSimilarAsync(query);

      // TODO: max results should be specified in the respective query config
      var querySegments = ResultUtils.ToSegmentData(queryResults, MultimediaRegistry);

      var queryData = new QueryResponse(query, querySegments);
      if (prefetch > 0)
      {
        await queryData.Prefetch(prefetch, MultimediaRegistry);
      }

      return queryData;
    }

    /// <summary>
    /// Executes a staged query.
    /// </summary>
    public async Task<QueryResponse> ExecuteQuery(StagedSimilarityQuery query, int prefetch)
    {
      var queryResults = await SegmentsApi.FindSegmentSimilarStagedAsync(query);

      // TODO: max results should be specified in the respective query config
      var querySegments = ResultUtils.ToSegmentData(queryResults, MultimediaRegistry);

      var queryData = new QueryResponse(query, querySegments);
      if (prefetch > 0)
      {
        await queryData.Prefetch(prefetch, MultimediaRegistry);
      }

      return queryData;
    }

    /// <summary>
    /// Executes a temporal query.
    /// </summary>
    public async Task<TemporalQueryResponse> ExecuteQuery(TemporalQuery query, int prefetch)
    {
      var queryResults = await SegmentsApi.FindSegmentSimilarTemporalAsync(query);

      var queryData = new TemporalQueryResponse(query, queryResults, MultimediaRegistry);
      if (prefetch > 0)
      {
        await queryData.Prefetch(prefetch);
      }

      return queryData;
    }

    /// <summary>
    /// Retrieves the unordered list of tags with names matching get given name.
    /// </summary>
    /// <param name="tagName">Name or partial name of the tags of interest</param>
    /// <returns>List of <see cref="Tag"/> objects matching the given name</returns>
    public async Task<List<Tag>> GetMatchingTags(string tagName)
    {
      var result = await Task.Run(() => TagApi.FindTagsBy("matchingname", tagName));

      return result.Tags;
    }

    /// <summary>
    /// Retrieves the distinct values of a specific Boolean query table and column.
    /// </summary>
    /// <param name="table">Table name</param>
    /// <param name="column">Column name</param>
    /// <returns>List of distinct values occuring in the specified column of the specified table</returns>
    public async Task<List<string>> GetDistinctTableValues(string table, string column)
    {
      var columnSpec = new ColumnSpecification(column, table);
      var results = await Task.Run(() => MiscApi.FindDistinctElementsByColumn(columnSpec));
      return results.DistinctElements;
    }

    /// <summary>
    /// Returns the URL to the Thumbnail of the given segment data. Needs to be registered and loaded previously!
    /// </summary>
    /// <param name="segment">Segment to get thumbnail URL of.</param>
    /// <returns>Thumbnail URL of the segment.</returns>
    public string GetThumbnailUrlOf(SegmentData segment)
    {
      return GetThumbnailUrlOfAsync(segment).Result;
    }

    /// <summary>
    /// Returns the URL to the Thumbnail of the given segment data asynchronously.
    /// </summary>
    /// <param name="segment">Segment to get thumbnail URL of.</param>
    /// <returns>Thumbnail URL of the segment.</returns>
    public async Task<string> GetThumbnailUrlOfAsync(SegmentData segment)
    {
      if (CineastConfig.cineastServesMedia)
      {
        return PathResolver.CombineUrl(CineastConfig.cineastHost, $"/thumbnails/{segment.Id}");
      }

      var objectId = await segment.GetObjectId();
      var path = PathResolver.ResolvePath(CineastConfig.thumbnailPath, objectId, segment.Id);
      return PathResolver.CombineUrl(CineastConfig.mediaHost, path + CineastConfig.thumbnailExtension);
    }

    /// <summary>
    /// Returns the URL to the media of the given object data. Needs to be registered and loaded previously!
    /// </summary>
    /// <param name="obj">Media object to get URL of.</param>
    /// <returns>URL of this media object file.</returns>
    public string GetMediaUrlOf(ObjectData obj)
    {
      return GetMediaUrlOfAsync(obj).Result;
    }

    /// <summary>
    /// Returns the URL to the media of the given object data asynchronously.
    /// </summary>
    /// <param name="obj">Media object to get URL of.</param>
    /// <param name="segmentId">Optional segment ID to support legacy use cases.</param>
    /// <returns>URL of this media object file.</returns>
    public async Task<string> GetMediaUrlOfAsync(ObjectData obj, string segmentId = null)
    {
      if (CineastConfig.cineastServesMedia)
      {
        return PathResolver.CombineUrl(CineastConfig.cineastHost, $"/objects/{obj.Id}");
      }

      var objectPath = await obj.GetPath();
      var objectName = await obj.GetName();
      var mediaType = await obj.GetMediaType();
      var path = PathResolver.ResolvePath(CineastConfig.mediaPath, obj.Id, segmentId, objectName, objectPath,
        mediaType.ToString());
      return PathResolver.CombineUrl(CineastConfig.mediaHost, path);
    }
  }
}