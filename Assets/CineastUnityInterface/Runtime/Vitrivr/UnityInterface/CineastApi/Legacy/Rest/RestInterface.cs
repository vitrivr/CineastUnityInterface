using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Legacy.Rest
{
    public class RestInterface
    {

        public IEnumerator PostJson<T>(string url, IRestTypedJsonResponseHandler<T> handler, object payload)
        {
            using (UnityWebRequest req = new UnityWebRequest(url))
            {
                req.method = UnityWebRequest.kHttpVerbPOST;
                req.SetRequestHeader("Content-Type", "application/json");
                var uploader = new UploadHandlerRaw(Encoding.UTF8.GetBytes(ToJson(payload)));
                uploader.contentType = "application/json";
                var downloader = new DownloadHandlerBuffer();
                req.uploadHandler = uploader;
                req.downloadHandler = downloader;
                yield return req.SendWebRequest();

                if (req.isHttpError)
                {
                    handler.OnHttpError(req.responseCode, req.error);
                }else if (req.isNetworkError)
                {
                    handler.OnFailure(req.error);
                }
                else
                {
                    handler.OnSuccess(FromJson<T>(req.downloadHandler.text));
                }
            }
        }
        

        public T PostJsonBlocking<T>(string url, object payload)
        {
            using (UnityWebRequest req = new UnityWebRequest(url))
            {
                req.method = UnityWebRequest.kHttpVerbPOST;
                req.SetRequestHeader("Content-Type","application/json");
                var uploader = new UploadHandlerRaw(Encoding.UTF8.GetBytes(ToJson(payload)));
                uploader.contentType = "application/json";
                var downloader = new DownloadHandlerBuffer();
                req.uploadHandler = uploader;
                req.downloadHandler = downloader;
                var async = req.SendWebRequest();
                while (!async.isDone)
                {
                    // waiting...
                }

                if (req.isHttpError | req.isNetworkError)
                {
                    return default(T);
                }
                else
                {
                    return FromJson<T>(req.downloadHandler.text);
                }
            }
        }

        public T GetJsonBlocking<T>(string url)
        {
            // TODO On failure? --> Optional?
            using (UnityWebRequest req = UnityWebRequest.Get(url))
            {
                var async = req.SendWebRequest();
                while (!async.isDone)
                {
                    // waiting...
                }

                if (req.isHttpError || req.isNetworkError)
                {
                    return default(T);
                }
                else
                {
                    return FromJson<T>(req.downloadHandler.text);
                }
            }
        }
        
        public IEnumerator GetJson<T>(string url, IRestTypedJsonResponseHandler<T> handler)
        {
            using (UnityWebRequest req = UnityWebRequest.Get(url))
            {
                yield return req.SendWebRequest();

                if (req.isHttpError)
                {
                    handler.OnHttpError(req.responseCode, req.error);
                }else if (req.isNetworkError)
                {
                    handler.OnFailure(req.error);
                }
                else
                {
                    handler.OnSuccess(FromJson<T>(req.downloadHandler.text));
                }
            }
        }

        public static T FromJson<T>(string json)
        {
            return JsonUtility.FromJson<T>(json);
        }

        public static string ToJson(object obj)
        {
            return JsonUtility.ToJson(obj);
        }

    }
}