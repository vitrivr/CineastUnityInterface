namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Legacy.Models.Messages.Query
{
    [System.Serializable]
    public class SimilarQuery
    {
        public string[] types = new[] {"IMAGE"};

        public TermContainer[] containers;

        public SimilarQuery(TermContainer[] containers)
        {
            this.containers = containers;
        }

        public void With(SimilarQuery query)
        {
            TermContainer[] tc = new[] {containers[0], query.containers[0]};
            containers = tc;
        }
    }
}