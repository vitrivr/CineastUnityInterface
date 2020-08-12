using System;
using System.Collections;
using System.Collections.Generic;
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

    private CineastConfig cineastConfig;
    
    private void Awake()
    {
      this.cineastConfig = CineastConfigManager.Instance.Config;
      this.segmentsApi = new SegmentsApi(CineastConfigManager.Instance.ApiConfiguration);
    }

    private Dictionary<Guid, ResponseHandler<Object>> guidHandlerMap = new Dictionary<Guid, ResponseHandler<Object>>();
    
    private bool queryRunning = false;

    public bool QueryRunning => queryRunning;

    public void RequestAsync(SimilarityQuery query, ResponseHandler<Object> handler) // FIXME Use proper object
    {
      if (QueryRunning)
      {
        return;
      }

      guidHandlerMap.Add(handler.Guid, handler);
      queryRunning = true;
      StartCoroutine(ExecuteAsyncQuery(query, handler.Guid));
    }

    private IEnumerator ExecuteAsyncQuery(SimilarityQuery query, Guid guid)
    {
      // === Initial Similarity Query ===
      yield return this.segmentsApi.FindSegmentSimilarAsync(query);
      
        //...
    }
  }
  
  
}