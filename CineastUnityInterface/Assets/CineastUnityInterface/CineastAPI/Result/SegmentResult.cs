namespace CineastUnityInterface.CineastAPI.Result
{
    [System.Serializable]
    public class SegmentResult
    {
        public const string MESSAGE_TYPE = "QR_SEGMENT";

        public string queryId;
        public string messageType;
        public SegmentObject[] content;
    }
}