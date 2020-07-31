namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Models.Messages.Result
{
    [System.Serializable]
    public class SimilarResult
    {
        public string[] categories;
        public ResultObject[] results;

        public bool IsEmpty()
        {
            return results.Length == 0;
        }
    }
}