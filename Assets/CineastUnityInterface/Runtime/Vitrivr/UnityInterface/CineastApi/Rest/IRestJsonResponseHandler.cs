namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Rest
{
    public interface IRestJsonResponseHandler<T>
    {
        void OnSuccess(T data);

        void OnHttpError(long code, string msg);

        void OnFailure(string msg);
    }
}