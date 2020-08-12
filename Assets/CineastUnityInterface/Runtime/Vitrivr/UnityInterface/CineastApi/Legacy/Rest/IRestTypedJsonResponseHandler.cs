namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Legacy.Rest
{
    public interface IRestTypedJsonResponseHandler<T>
    {
        void OnSuccess(T data);

        void OnHttpError(long code, string msg);

        void OnFailure(string msg);
    }
}