using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Model.Config;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Model.Processing;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Utils;
using Org.Vitrivr.CineastApi.Api;
using Org.Vitrivr.CineastApi.Client;
using Org.Vitrivr.CineastApi.Model;
using UnityEngine;
using Object = System.Object;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi
{
  /// <summary>
  /// Wrapper for cineast
  /// </summary>
  public class CineastWrapper : MonoBehaviour
  {

    public static readonly SegmentsApi SegmentsApi = new SegmentsApi(CineastConfigManager.Instance.ApiConfiguration);
    public static readonly SegmentApi SegmentApi = new SegmentApi(CineastConfigManager.Instance.ApiConfiguration);

    public static readonly CineastConfig CineastConfig = CineastConfigManager.Instance.Config;

    private Dictionary<Guid, ResponseHandler<Object>> guidHandlerMap = new Dictionary<Guid, ResponseHandler<Object>>();
    
    private bool queryRunning;

    public bool QueryRunning => queryRunning;

    public async Task<SimilarityQueryResultBatch> RequestThreaded(SimilarityQuery query)
    {
      if (QueryRunning)
      {
        return null;
      }

      queryRunning = true;
      var result = await Task.Run(() => SegmentsApi.FindSegmentSimilar(query));
      queryRunning = false;
      return result;
    }

    public async void SimilarityRequestAsync(SimilarityQuery query, ResponseHandler<Object> handler) // FIXME Use proper object
    {
      if (QueryRunning)
      {
        return;
      }

      guidHandlerMap.Add(handler.Guid, handler);
      queryRunning = true;
      // === Initial Similarity Query ===
      var similarResults = await Task.Run(() => SegmentsApi.FindSegmentSimilarAsync(query));
      
      // TODO handle errors
      
      // === SEGMENTS ===
      var segmentResults = await Task.Run(() => SegmentApi.FindSegmentByIdBatched(ResultUtils.IdsOf(similarResults)));
    }
  }
  
  
}