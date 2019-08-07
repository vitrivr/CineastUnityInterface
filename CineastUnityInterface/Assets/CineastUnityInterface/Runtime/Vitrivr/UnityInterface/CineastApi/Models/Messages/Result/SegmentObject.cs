namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Models.Messages.Result
{
    [System.Serializable]
    public class SegmentObject
    {
        public string segmentId;
        public string objectId;
        public int start;
        public int end;
        public double startabs;
        public double endabs;
        public int count;
        public int sequenceNumber;
    }
}