using System.Collections.Generic;
using System.Threading.Tasks;
using Org.Vitrivr.CineastApi.Api;
using Org.Vitrivr.CineastApi.Model;
using UnityEngine;
using Vitrivr.UnityInterface.CineastApi.Model.Config;
using Vitrivr.UnityInterface.CineastApi.Model.Data;
using Vitrivr.UnityInterface.CineastApi.Utils;

namespace Vitrivr.UnityInterface.CineastApi
{
  /// <summary>
  /// Wrapper for cineast
  /// </summary>
  public class CineastWrapper : MonoBehaviour
  {
    public static readonly ObjectApi ObjectApi = new ObjectApi(CineastConfigManager.Instance.ApiConfiguration);
    public static readonly SegmentsApi SegmentsApi = new SegmentsApi(CineastConfigManager.Instance.ApiConfiguration);
    public static readonly SegmentApi SegmentApi = new SegmentApi(CineastConfigManager.Instance.ApiConfiguration);
    public static readonly TagApi TagApi = new TagApi(CineastConfigManager.Instance.ApiConfiguration);
    public static readonly MetadataApi MetadataApi = new MetadataApi(CineastConfigManager.Instance.ApiConfiguration);

    public static readonly CineastConfig CineastConfig = CineastConfigManager.Instance.Config;

    public bool QueryRunning { get; private set; }

    /// <summary>
    /// Executes a <see cref="SimilarityQuery"/>, reduces the result set to the specified number of maximum results and
    /// prefetches data for the given number of segments.
    /// </summary>
    /// <param name="query">Query to execute</param>
    /// <param name="maxResults">Maximum number of results to retain</param>
    /// <param name="prefetch">Number of segments to prefetch data for</param>
    /// <returns><see cref="QueryResponse"/> for the query including the result list</returns>
    public static async Task<QueryResponse> ExecuteQuery(SimilarityQuery query, int maxResults, int prefetch)
    {
      var queryResults = await Task.Run(() => SegmentsApi.FindSegmentSimilar(query));

      var querySegments = ResultUtils.ToSegmentData(queryResults, maxResults);

      var queryData = new QueryResponse(query, querySegments);
      if (prefetch > 0)
      {
        await queryData.Prefetch(prefetch);
      }

      return queryData;
    }

    /// <summary>
    /// Retrieves the unordered list of tags with names matching get given name.
    /// </summary>
    /// <param name="name">Name or partial name of the tags of interest</param>
    /// <returns>List of <see cref="Tag"/> objects matching the given name</returns>
    public static async Task<List<Tag>> GetMatchingTags(string name)
    {
      var result = await Task.Run(() => TagApi.FindTagsBy("matchingname", name));

      return result.Tags;
    }

    public async Task<SimilarityQueryResultBatch> RequestThreaded(SimilarityQuery query)
    {
      if (QueryRunning)
      {
        return null;
      }

      QueryRunning = true;
      var result = await Task.Run(() => SegmentsApi.FindSegmentSimilar(query));
      QueryRunning = false;
      return result;
    }

    /// <summary>
    /// Returns the URL to the Thumbnail of the given segment data. Needs to be registered and loaded previously!
    /// </summary>
    /// <param name="segment"></param>
    /// <returns></returns>
    public static string GetThumbnailUrlOf(SegmentData segment)
    {
      if (CineastConfig.cineastServesMedia)
      {
        return CineastConfig.cineastHost + "/thumbnails/" + segment.Id;
      }
      else
      {
        return CineastConfig.mediaHost + "thumbnails/" + segment.GetObjectId().Result + "/" + segment.Id + ".png"; // or .jpg
      }
    }

    /// <summary>
    /// Returns the URL to the media of the given object data. Needs to be registered and laoded previously!
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string GetMediaUrlOf(ObjectData obj)
    {
      if (CineastConfig.cineastServesMedia)
      {
        return CineastConfig.cineastHost + "/objects/" + obj.Id;
      }
      else
      {
        return CineastConfig.mediaHost + "/images/" + obj.GetPath().Result;
      }
    }
  }
}