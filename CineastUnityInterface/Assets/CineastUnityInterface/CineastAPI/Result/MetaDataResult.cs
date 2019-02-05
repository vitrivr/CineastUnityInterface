namespace CineastUnityInterface.CineastAPI.Result
{
    [System.Serializable]
    public class MetaDataResult
    {
        public const string MESSAGE_TYPE = "QR_METADATA";
        public string queryId;
        public string messageType;
        public MetaDataObject[] content;
    }
}