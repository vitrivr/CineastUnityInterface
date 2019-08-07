namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Models.Messages.Query
{
    [System.Serializable]
    public class TermContainer
    {
        public TermsObject[] terms;

        public TermContainer(TermsObject[] terms)
        {
            this.terms = terms;
        }
    }
}