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

    private SegmentsApi segmentsApi;
    private SegmentApi segmentApi;

    private CineastConfig cineastConfig;
    
    private void Awake()
    {
      cineastConfig = CineastConfigManager.Instance.Config;
      segmentsApi = new SegmentsApi(CineastConfigManager.Instance.ApiConfiguration);
      segmentApi = new SegmentApi(CineastConfigManager.Instance.ApiConfiguration);
    }

    private Dictionary<Guid, ResponseHandler<Object>> guidHandlerMap = new Dictionary<Guid, ResponseHandler<Object>>();
    
    private bool queryRunning = false;

    public bool QueryRunning => queryRunning;

    public async Task<SimilarityQueryResultBatch> RequestThreaded(SimilarityQuery query)
    {
      if (QueryRunning)
      {
        return null;
      }

      queryRunning = true;
      var result = await Task.Run(() => segmentsApi.FindSegmentSimilar(query));
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
      var similarResults = await Task.Run(() => this.segmentsApi.FindSegmentSimilarAsync(query));
      
      // TODO handle errors
      
      // === SEGMENTS ===
      var segmentResults = await Task.Run(() => this.segmentApi.FindSegmentByIdBatched(ResultUtils.IdsOf(similarResults)));
      
    }
  }
  
  
}