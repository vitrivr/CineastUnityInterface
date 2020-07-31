using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Models.Messages.Result;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Rest;
using UnityEngine;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Handlers
{
    public abstract class AbstractMessageHandler<T>: IRestTypedJsonResponseHandler<T>
    {
        protected LegacyCineastApi api;
        protected string guid;

        protected AbstractMessageHandler(LegacyCineastApi api, string guid)
        {
            this.api = api;
            this.guid = guid;
        }

        public abstract void OnSuccess(T data);

        public void OnHttpError(long code, string msg)
        {
            Debug.LogErrorFormat("{0} - {1}", code, msg);
            api.ReportFailure(guid, string.Format("HTTP Error: {0} - {1}", code,msg));
        }

        public void OnFailure(string msg)
        {
            Debug.LogErrorFormat("Fatal REST failure: {0}", msg);
            api.ReportFailure(guid, string.Format("General Error: {0}", msg));
        }
    }
}