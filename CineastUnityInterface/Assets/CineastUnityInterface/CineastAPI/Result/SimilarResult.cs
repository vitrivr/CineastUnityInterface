namespace CineastUnityInterface.CineastAPI.Result
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