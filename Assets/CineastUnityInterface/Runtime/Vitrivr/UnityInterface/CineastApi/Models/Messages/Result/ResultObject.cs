namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Models.Messages.Result
{
    [System.Serializable]
    public class ResultObject
    {
        public const string MESSAGE_TYPE = "QR_SIMILARITY";

        public string queryId;
        public string category; // spatial or later temporal?
        public string messagetype; // QR_SIMILARITY
        public ContentObject[] content;

        public bool IsEmpty()
        {
            return content.Length == 0;
        }
    }
}