using System;
using System.Collections;
using System.Collections.Generic;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Legacy.Handlers;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Legacy.Models;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Legacy.Models.Messages.Query;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Legacy.Models.Messages.Result;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Legacy.Processing;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Legacy.Rest;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Legacy.Utils;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Processing;
using UnityEngine;

//using Logger = Assets.Modules.SimpleLogging.Logger;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Legacy
{
  [Obsolete("Formerly known as CineastApi, replaced by Vitrivr.UnityInterface.CineastApi.CineastWrapper")]
  public class LegacyCineastApi : MonoBehaviour
  {
    private bool earlyBreak;

    private FilterEngine filterEngine;

    private bool finished;

    private Logger logger;

    private WWW metaRequest;
    private MetaDataResult metaResult;
    private List<MultimediaObject> objectList;

    private WWW objectRequest;
    private ObjectsResult objectsResult;

    private Action<List<MultimediaObject>> queryFinishedCallback;

    private List<MultimediaObject> results;

    private WWW segmentRequest;
    private SegmentResult segmentResult;
    private WWW similarRequest;
    private SimilarResult similarResult;

    private IDictionary<String, CineastResponseHandler<List<MultimediaObject>>> _guidHandlerMap;
    private RestInterface rest = new RestInterface();
    private bool queryRunning = false;

    public bool QueryRunning => queryRunning;

    private void Awake()
    {
      filterEngine = new FilterEngine();
      if (CineastConfiguration.HasConfig())
      {
        CineastConfiguration config = CineastConfiguration.Load();
        if (!config.IsEmpty())
        {
          CineastUtils.Configuration = config;
        }
        else
        {
          CineastUtils.Configuration = CineastConfiguration.GetDefault();
        }
      }
      else
      {
        CineastConfiguration.StoreDefault();
      }

      _guidHandlerMap = new Dictionary<string, CineastResponseHandler<List<MultimediaObject>>>();
    }

    /// <summary>
    /// Performs an asynchronous request based on the similarity query provided.
    /// </summary>
    /// <param name="query">The query to query cineast</param>
    /// <param name="handler">The handler for handling success / failure</param>
    public void RequestAsync(SimilarQuery query, CineastResponseHandler<List<MultimediaObject>> handler)
    {
      if (queryRunning)
      {
        return;
      }

      _guidHandlerMap.Add(handler.Guid.ToString(), handler);
      queryRunning = true;
      StartCoroutine(ExecuteQuery(query, handler.Guid.ToString()));
    }


    public void RequestSimilarAndThen(SimilarQuery query, Action<List<MultimediaObject>> handler)
    {
      queryFinishedCallback = handler;
      StartCoroutine(ExecuteQuery(query));
    }

    public void RequestWeightedSimilarAndThen(SimilarQuery query, CategoryRatio ratio,
      Action<List<MultimediaObject>> handler)
    {
      queryFinishedCallback = handler;
      StartCoroutine(ExecuteMultiQuery(query, ratio));
    }

    private IEnumerator ExecuteMultiQuery(SimilarQuery query, CategoryRatio ratio, string guid = null)
    {
      // === SIMILAR ===
      // Initial SimilarQuery
      yield return similarRequest =
        CineastUtils.BuildSimilarRequest(CineastUtils.Configuration.FindSimilarSegmentsUrl(), query);

      // Parse response
      earlyBreak = !Parse(similarRequest.text, out similarResult);
      yield return similarResult;
      if (earlyBreak)
      {
        yield break;
      }

      // Check if empty
      if (similarResult.IsEmpty())
      {
        earlyBreak = true;
        yield break; // Stop and 
      }

      ContentObject[] tempResult = CineastUtils.ExtractContentObjects(similarResult);

      if (ratio != null && similarResult.results.Length > 1)
      {
        ResultMerger merger = new ResultMerger();
        tempResult = merger.Merge(similarResult.results, ratio)
          .ToArray();
      }

      // === SEGMENTS ===
      // segments
      yield return segmentRequest =
        CineastUtils.BuildSegmentRequest(CineastUtils.Configuration.FindSegmentsByIdUrl(),
          CineastUtils.ExtractIdArray(tempResult));

      // parse response
      earlyBreak = !Parse(segmentRequest.text, out segmentResult);
      yield return segmentResult;
      if (earlyBreak)
      {
        yield break;
      }

      // === METAS ===
      yield return metaRequest =
        CineastUtils.BuildMetadataRequest(CineastUtils.Configuration.FindMetadataUrl(),
          CineastUtils.ExtractIdArray(segmentResult.content));
      earlyBreak = !Parse(metaRequest.text, out metaResult);
      yield return metaResult;
      if (earlyBreak)
      {
        yield break;
      }

      // meta->mmo
      objectList = CineastUtils.Convert(metaResult.content);

      // === OBJECTS ===
      yield return objectRequest =
        CineastUtils.BuildObjectsRequest(CineastUtils.Configuration.FindObjectsUrl(),
          CineastUtils.ExtractIdArray(objectList.ToArray()));
      yield return objectsResult = JsonUtility.FromJson<ObjectsResult>(objectRequest.text);

      // merge results
      List<MultimediaObject> objects = CineastUtils.Convert(objectsResult.content);
      foreach (MultimediaObject mmo in objects)
      {
        if (objectList.Contains(mmo))
        {
          objectList.Find(o => o.Equals(mmo)).Merge(mmo);
        }
      }

      results = new List<MultimediaObject>(objectList);

      // === WRAPUP ===
      foreach (MultimediaObject mmo in objectList)
      {
        mmo.resultIndex = CineastUtils.GetIndexOf(mmo, similarResult) + 1;
      }

      // === SORT LIST ===
      objectList.Sort(Comparison);

      List<MultimediaObject> transferList;
      if (filterEngine != null)
      {
//                logger.Debug("FilterEngine installed with " + filterEngine.GetFilterCount() + " filters.");
        transferList = filterEngine.ApplyFilters(objectList);
      }
      else
      {
//                logger.Debug("No FilterEngine installed - no filtering");
        transferList = objectList;
      }


      // cleanup
      finished = true;
      if (guid == null)
      {
        // LEGACY
        if (queryFinishedCallback != null)
        {
          //                logger.Info("Query completed, passing resulting MultimediaObject list to callback");
          queryFinishedCallback.Invoke(transferList);
        }
      }
      else
      {
        CineastResponseHandler<List<MultimediaObject>> handler = _guidHandlerMap[guid];
        if (handler != null)
        {
          handler.onSuccess(transferList);
        }
      }

      yield return true;
    }

    public IEnumerator RequestSimilarAsync(SimilarQuery query, string guid)
    {
      if (!queryRunning)
      {
        yield break;
      }
      var similarHandler = new SimilarResultHandler(this, guid);
      yield return rest.PostJson(CineastUtils.Configuration.FindSimilarSegmentsUrl(), similarHandler, query);
    }

    public IEnumerator RequestSegmentsAsync(IdsQuery query, string guid)
    {
      if (!queryRunning)
      {
        yield break;
      }
      var segmentHandler = new SegmentResultHandler(this, guid);
      yield return rest.PostJson(CineastUtils.Configuration.FindSegmentsByIdUrl(), segmentHandler, query);
    }

    public void ReportFailure(string guid, string msg)
    {
      if (_guidHandlerMap.ContainsKey(guid))
      {
        _guidHandlerMap[guid].onFailure(msg);
        _guidHandlerMap.Remove(guid);
      }

      queryRunning = false;
    }

    private IEnumerator ExecuteQuery(SimilarQuery query, string guid = null)
    {
      // === SIMILAR ===
      // Initial SimilarQuery
      yield return similarRequest =
        CineastUtils.BuildSimilarRequest(CineastUtils.Configuration.FindSimilarSegmentsUrl(), query);

      // Parse response
      earlyBreak = !Parse(similarRequest.text, out similarResult);
      yield return similarResult;
      if (earlyBreak)
      {
//                logger.Error("HTTP error upon similar response");
        yield break;
      }

//            logger.Info("Successfully parsed similar response");

      // Check if empty
      if (similarResult.IsEmpty())
      {
        earlyBreak = true;
//                logger.Error("Empty similar result");
        yield break; // Stop and 
      }

      // === SEGMENTS ===
      // segments
//            logger.Debug("Starting segments query");
      yield return segmentRequest =
        CineastUtils.BuildSegmentRequest(CineastUtils.Configuration.FindSegmentsByIdUrl(),
          CineastUtils.ExtractIdArray(CineastUtils.ExtractContentObjects(similarResult)));

//            logger.Debug("Received segments response:\n" + segmentRequest.text);
      // parse response
      earlyBreak = !Parse(segmentRequest.text, out segmentResult);
      yield return segmentResult;
      if (earlyBreak)
      {
//                logger.Error("HTTP error upon segments response");
        yield break;
      }

//            logger.Info("Successfully parsed segments response");

      // === METAS ===
//            logger.Debug("Starting metadata request");
      yield return metaRequest =
        CineastUtils.BuildMetadataRequest(CineastUtils.Configuration.FindMetadataUrl(),
          CineastUtils.ExtractIdArray(segmentResult.content));
//            logger.Debug("Received metadata response:\n" + metaRequest.text);
      earlyBreak = !Parse(metaRequest.text, out metaResult);
      yield return metaResult;
      if (earlyBreak)
      {
//                logger.Error("HTTP error upon metadata response");
        yield break;
      }

//            logger.Info("Successfully parsed metadata response");
      // meta->mmo

      objectList = CineastUtils.Convert(metaResult.content);
//            logger.Info("Successfully converted metadata result to MultimediaObjects");


      // === OBJECTS ===
//            logger.Debug("Starting object query");
      yield return objectRequest =
        CineastUtils.BuildObjectsRequest(CineastUtils.Configuration.FindObjectsUrl(),
          CineastUtils.ExtractIdArray(objectList.ToArray()));
//            logger.Debug("Received objects response:\n" + objectRequest.text);

      yield return objectsResult = JsonUtility.FromJson<ObjectsResult>(objectRequest.text);

//            logger.Info("Successfully parsed objects response");

      // merge results
      List<MultimediaObject> objects = CineastUtils.Convert(objectsResult.content);
//            logger.Debug("Successfully converted object result to MultimediaObjects");
      foreach (MultimediaObject mmo in objects)
      {
        if (objectList.Contains(mmo))
        {
          objectList.Find(o => o.Equals(mmo)).Merge(mmo);
        }
      }

//            logger.Info("Finished merging different MultimediaObject lists");

      results = new List<MultimediaObject>(objectList);

      // === WRAPUP ===
//            logger.Debug("Applying result index to MultimediaObject list");
      foreach (MultimediaObject mmo in objectList)
      {
        mmo.resultIndex = CineastUtils.GetIndexOf(mmo, similarResult) + 1;
      }

//            logger.Info("Result contains " + objectList.Count + " entities");
//            logger.Debug("Full result list:\n" + DumpMMOList(objectList));


      // === SORT LIST ===
//            logger.Debug("Sorting list");
      objectList.Sort(
        Comparison);
//            logger.Debug("Sorted list: \n" + DumpMMOList(objectList));

      List<MultimediaObject> transferList;
      if (filterEngine != null)
      {
//                logger.Debug("FilterEngine installed with " + filterEngine.GetFilterCount() + " filters.");
        transferList = filterEngine.ApplyFilters(objectList);
      }
      else
      {
//                logger.Debug("No FilterEngine installed - no filtering");
        transferList = objectList;
      }


      // cleanup
      finished = true;
      if (guid == null)
      {
        // LEGACY
        if (queryFinishedCallback != null)
        {
          //                logger.Info("Query completed, passing resulting MultimediaObject list to callback");
          queryFinishedCallback.Invoke(transferList);
        }
      }
      else
      {
        CineastResponseHandler<List<MultimediaObject>> handler = _guidHandlerMap[guid];
        if (handler != null)
        {
          handler.onSuccess(transferList);
        }
      }

      yield return true;
    }

    private int Comparison(MultimediaObject mmo1, MultimediaObject mmo2)
    {
      return mmo1.resultIndex - mmo2.resultIndex;
    }

    private string DumpMMOList(List<MultimediaObject> list)
    {
      var ret = "[";

      foreach (MultimediaObject mmo in list)
      {
        ret += JsonUtility.ToJson(mmo);
        ret += ",";
      }

      return ret + "]";
    }


    public SimilarResult GetSimilarResult()
    {
      return similarResult;
    }

    public bool HasFinished()
    {
      return finished;
    }

    public bool HasEarlyBreak()
    {
      return earlyBreak;
    }

    public SegmentResult GetSegmentResult()
    {
      return segmentResult;
    }

    public MetaDataResult GetMetaResult()
    {
      return metaResult;
    }

    public List<MultimediaObject> GetMultimediaObjects()
    {
      return objectList;
    }

    private static bool HasHTTPErrorOccurred(string msg)
    {
      return msg.StartsWith("<html>");
    }

    /**
     *  RETURNS FALSE IF AN ERROR OCCURED
     */
    private static bool Parse<T>(string toParse, out T result)
    {
      if (HasHTTPErrorOccurred(toParse))
      {
        result = default(T);
        return false;
      }

      result = JsonUtility.FromJson<T>(toParse);
      return true;
    }

    public void Clean()
    {
      objectList.Clear();
    }

    public void AddCineastFilter(FilterStrategy strategy)
    {
      filterEngine.AddFilterStrategy(strategy);
    }

    public List<MultimediaObject> GetOriginalResults()
    {
      return new List<MultimediaObject>(results);
    }
  }
}