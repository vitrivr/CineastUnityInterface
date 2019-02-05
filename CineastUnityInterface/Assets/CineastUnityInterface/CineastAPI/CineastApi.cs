using System;
using System.Collections;
using System.Collections.Generic;
using CineastUnityInterface.CineastAPI.Query;
using CineastUnityInterface.CineastAPI.Result;
using UnityEngine;
using Logger = Assets.Modules.SimpleLogging.Logger;

namespace CineastUnityInterface.CineastAPI {
    public class CineastApi : MonoBehaviour {
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

        private void Awake() {
            logger = LogManager.GetInstance().GetLogger(GetType());
            logger.Debug("Awake");
            filterEngine = new FilterEngine();
            if (CineastConfiguration.HasConfig()) {
                CineastConfiguration config = CineastConfiguration.Load();
                if (!config.IsEmpty()) {
                    CineastUtils.Configuration = config;
                    logger.Debug("Set configuration!");
                } else {
                    CineastUtils.Configuration = CineastConfiguration.GetDefault();
                    logger.Warn("Using default configuration!");
                }

                logger.Debug("Successfully loaded config {0}", JsonUtility.ToJson(config));
            } else {
                logger.Debug("No config found");
                CineastConfiguration.StoreEmpty();
                logger.Debug("Wrote empty config to disk");
            }
        }


        public void RequestSimilarAndThen(SimilarQuery query, Action<List<MultimediaObject>> handler) {
            logger.Debug("RequestSimilarAndThen");
            queryFinishedCallback = handler;
            StartCoroutine(ExecuteQuery(query));
        }

        public void RequestWeightedSimilarAndThen(SimilarQuery query, CategoryRatio ratio,
            Action<List<MultimediaObject>> handler) {
            logger.Debug("RequestWeightedSimilarAndThen");
            queryFinishedCallback = handler;
            StartCoroutine(ExecuteMultiQuery(query, ratio));
        }

        private IEnumerator ExecuteMultiQuery(SimilarQuery query, CategoryRatio ratio) {
            // === SIMILAR ===
            // Initial SimilarQuery
            logger.Debug("Starting initial similar request.\n" + JsonUtility.ToJson(query));
            yield return similarRequest = CineastUtils.BuildSimilarRequest(CineastUtils.Configuration.FindSimilarSegmentsUrl(), query);
            logger.Debug("Received similar response: " + similarRequest.text);

            // Parse response
            earlyBreak = !Parse(similarRequest.text, out similarResult);
            yield return similarResult;
            if (earlyBreak) {
                logger.Error("HTTP error upon similar response");
                yield break;
            }

            logger.Info("Successfully parsed similar response");

            // Check if empty
            if (similarResult.IsEmpty()) {
                earlyBreak = true;
                logger.Error("Empty similar result");
                yield break; // Stop and 
            }

            ContentObject[] tempResult = CineastUtils.ExtractContentObjects(similarResult);

            if (ratio != null && similarResult.results.Length > 1) {
                logger.Debug("Merging...");
                foreach (ResultObject ro in similarResult.results) {
                    logger.Debug("Result for category {0} contains {1} entries.\n\t{2}", ro.category, ro.content.Length,
                        ContentObject.ArrayToStrig(ro.content));
                }

                ResultMerger merger = new ResultMerger();
                tempResult = merger.Merge(similarResult.results, ratio)
                    .ToArray();
                logger.Debug("After merge: {0} unique entries\n\t{1}", tempResult.Length,
                    ContentObject.ArrayToStrig(tempResult));
            }

            // === SEGMENTS ===
            // segments
            logger.Debug("Starting segments query");
            yield return segmentRequest =
                CineastUtils.BuildSegmentRequest(CineastUtils.Configuration.FindSegmentsByIdUrl(),
                    CineastUtils.ExtractIdArray(tempResult));

            logger.Debug("Received segments response:\n" + segmentRequest.text);
            // parse response
            earlyBreak = !Parse(segmentRequest.text, out segmentResult);
            yield return segmentResult;
            if (earlyBreak) {
                logger.Error("HTTP error upon segments response");
                yield break;
            }

            logger.Info("Successfully parsed segments response");

            // === METAS ===
            logger.Debug("Starting metadata request");
            yield return metaRequest =
                CineastUtils.BuildMetadataRequest(CineastUtils.Configuration.FindMetadataUrl(),
                    CineastUtils.ExtractIdArray(segmentResult.content));
            logger.Debug("Received metadata response:\n" + metaRequest.text);
            earlyBreak = !Parse(metaRequest.text, out metaResult);
            yield return metaResult;
            if (earlyBreak) {
                logger.Error("HTTP error upon metadata response");
                yield break;
            }

            logger.Info("Successfully parsed metadata response");
            // meta->mmo

            objectList = CineastUtils.Convert(metaResult.content);
            logger.Info("Successfully converted metadata result to MultimediaObjects");


            // === OBJECTS ===
            logger.Debug("Starting object query");
            yield return objectRequest =
                CineastUtils.BuildObjectsRequest(CineastUtils.Configuration.FindObjectsUrl(),
                    CineastUtils.ExtractIdArray(objectList.ToArray()));
            logger.Debug("Received objects response:\n" + objectRequest.text);

            yield return objectsResult = JsonUtility.FromJson<ObjectsResult>(objectRequest.text);

            logger.Info("Successfully parsed objects response");

            // merge results
            List<MultimediaObject> objects = CineastUtils.Convert(objectsResult.content);
            logger.Debug("Successfully converted object result to MultimediaObjects");
            foreach (MultimediaObject mmo in objects) {
                if (objectList.Contains(mmo)) {
                    objectList.Find(o => o.Equals(mmo)).Merge(mmo);
                }
            }

            logger.Info("Finished merging different MultimediaObject lists");

            results = new List<MultimediaObject>(objectList);

            // === WRAPUP ===
            logger.Debug("Applying result index to MultimediaObject list");
            foreach (MultimediaObject mmo in objectList) {
                mmo.resultIndex = CineastUtils.GetIndexOf(mmo, similarResult) + 1;
            }

            logger.Info("Result contains " + objectList.Count + " entities");
            logger.Debug("Full result list:\n" + DumpMMOList(objectList));


            // === SORT LIST ===
            logger.Debug("Sorting list");
            objectList.Sort(
                Comparison);
            logger.Debug("Sorted list: \n" + DumpMMOList(objectList));

            List<MultimediaObject> transferList;
            if (filterEngine != null) {
                logger.Debug("FilterEngine installed with " + filterEngine.GetFilterCount() + " filters.");
                transferList = filterEngine.ApplyFilters(objectList);
            } else {
                logger.Debug("No FilterEngine installed - no filtering");
                transferList = objectList;
            }


            // cleanup
            finished = true;
            if (queryFinishedCallback != null) {
                logger.Info("Query completed, passing resulting MultimediaObject list to callback");
                queryFinishedCallback.Invoke(transferList);
            }

            yield return true;
        }


        private IEnumerator ExecuteQuery(SimilarQuery query) {
            // === SIMILAR ===
            // Initial SimilarQuery
            logger.Debug("Starting initial similar request.\n" + JsonUtility.ToJson(query));
            yield return similarRequest = CineastUtils.BuildSimilarRequest(CineastUtils.Configuration.FindSimilarSegmentsUrl(), query);
            logger.Debug("Received similar response: " + similarRequest.text);

            // Parse response
            earlyBreak = !Parse(similarRequest.text, out similarResult);
            yield return similarResult;
            if (earlyBreak) {
                logger.Error("HTTP error upon similar response");
                yield break;
            }

            logger.Info("Successfully parsed similar response");

            // Check if empty
            if (similarResult.IsEmpty()) {
                earlyBreak = true;
                logger.Error("Empty similar result");
                yield break; // Stop and 
            }

            // === SEGMENTS ===
            // segments
            logger.Debug("Starting segments query");
            yield return segmentRequest =
                CineastUtils.BuildSegmentRequest(CineastUtils.Configuration.FindSegmentsByIdUrl(),
                    CineastUtils.ExtractIdArray(CineastUtils.ExtractContentObjects(similarResult)));

            logger.Debug("Received segments response:\n" + segmentRequest.text);
            // parse response
            earlyBreak = !Parse(segmentRequest.text, out segmentResult);
            yield return segmentResult;
            if (earlyBreak) {
                logger.Error("HTTP error upon segments response");
                yield break;
            }

            logger.Info("Successfully parsed segments response");

            // === METAS ===
            logger.Debug("Starting metadata request");
            yield return metaRequest =
                CineastUtils.BuildMetadataRequest(CineastUtils.Configuration.FindMetadataUrl(),
                    CineastUtils.ExtractIdArray(segmentResult.content));
            logger.Debug("Received metadata response:\n" + metaRequest.text);
            earlyBreak = !Parse(metaRequest.text, out metaResult);
            yield return metaResult;
            if (earlyBreak) {
                logger.Error("HTTP error upon metadata response");
                yield break;
            }

            logger.Info("Successfully parsed metadata response");
            // meta->mmo

            objectList = CineastUtils.Convert(metaResult.content);
            logger.Info("Successfully converted metadata result to MultimediaObjects");


            // === OBJECTS ===
            logger.Debug("Starting object query");
            yield return objectRequest =
                CineastUtils.BuildObjectsRequest(CineastUtils.Configuration.FindObjectsUrl(),
                    CineastUtils.ExtractIdArray(objectList.ToArray()));
            logger.Debug("Received objects response:\n" + objectRequest.text);

            yield return objectsResult = JsonUtility.FromJson<ObjectsResult>(objectRequest.text);

            logger.Info("Successfully parsed objects response");

            // merge results
            List<MultimediaObject> objects = CineastUtils.Convert(objectsResult.content);
            logger.Debug("Successfully converted object result to MultimediaObjects");
            foreach (MultimediaObject mmo in objects) {
                if (objectList.Contains(mmo)) {
                    objectList.Find(o => o.Equals(mmo)).Merge(mmo);
                }
            }

            logger.Info("Finished merging different MultimediaObject lists");

            results = new List<MultimediaObject>(objectList);

            // === WRAPUP ===
            logger.Debug("Applying result index to MultimediaObject list");
            foreach (MultimediaObject mmo in objectList) {
                mmo.resultIndex = CineastUtils.GetIndexOf(mmo, similarResult) + 1;
            }

            logger.Info("Result contains " + objectList.Count + " entities");
            logger.Debug("Full result list:\n" + DumpMMOList(objectList));


            // === SORT LIST ===
            logger.Debug("Sorting list");
            objectList.Sort(
                Comparison);
            logger.Debug("Sorted list: \n" + DumpMMOList(objectList));

            List<MultimediaObject> transferList;
            if (filterEngine != null) {
                logger.Debug("FilterEngine installed with " + filterEngine.GetFilterCount() + " filters.");
                transferList = filterEngine.ApplyFilters(objectList);
            } else {
                logger.Debug("No FilterEngine installed - no filtering");
                transferList = objectList;
            }


            // cleanup
            finished = true;
            if (queryFinishedCallback != null) {
                logger.Info("Query completed, passing resulting MultimediaObject list to callback");
                queryFinishedCallback.Invoke(transferList);
            }

            yield return true;
        }

        private int Comparison(MultimediaObject mmo1, MultimediaObject mmo2) {
            return mmo1.resultIndex - mmo2.resultIndex;
        }

        private string DumpMMOList(List<MultimediaObject> list) {
            var ret = "[";

            foreach (MultimediaObject mmo in list) {
                ret += JsonUtility.ToJson(mmo);
                ret += ",";
            }

            return ret + "]";
        }


        public SimilarResult GetSimilarResult() {
            return similarResult;
        }

        public bool HasFinished() {
            return finished;
        }

        public bool HasEarlyBreak() {
            return earlyBreak;
        }

        public SegmentResult GetSegmentResult() {
            return segmentResult;
        }

        public MetaDataResult GetMetaResult() {
            return metaResult;
        }

        public List<MultimediaObject> GetMultimediaObjects() {
            return objectList;
        }

        private static bool HasHTTPErrorOccurred(string msg) {
            return msg.StartsWith("<html>");
        }

        /**
         *  RETURNS FALSE IF AN ERROR OCCURED
         */
        private static bool Parse<T>(string toParse, out T result) {
            if (HasHTTPErrorOccurred(toParse)) {
                result = default(T);
                return false;
            }

            result = JsonUtility.FromJson<T>(toParse);
            return true;
        }

        public void Clean() {
            objectList.Clear();
        }

        public void AddCineastFilter(FilterStrategy strategy) {
            filterEngine.AddFilterStrategy(strategy);
        }

        public List<MultimediaObject> GetOriginalResults() {
            return new List<MultimediaObject>(results);
        }
    }
}