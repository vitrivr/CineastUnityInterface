namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Legacy.Models.Messages.Result
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