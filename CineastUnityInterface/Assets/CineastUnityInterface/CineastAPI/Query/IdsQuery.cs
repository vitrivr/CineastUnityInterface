namespace CineastUnityInterface.CineastAPI.Query
{
    [System.Serializable]
    public class IdsQuery
    {
        public string[] ids;

        public IdsQuery(string[] ids)
        {
            this.ids = ids;
        }
    }
}