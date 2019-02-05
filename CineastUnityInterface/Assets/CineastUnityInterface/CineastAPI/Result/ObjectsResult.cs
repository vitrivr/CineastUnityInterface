namespace CineastUnityInterface.CineastAPI.Result
{
    [System.Serializable]
    public class ObjectsResult
    {
        public const string MESSAGE_TYPE = "QR_OBJECT";

        public string queryId;
        public string messageType; // QR_OBJECT
        public CineastObject[] content;
    }
}