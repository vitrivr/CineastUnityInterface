using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Legacy.Models.Messages.Query;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Legacy.Utils
{
    public static class QueryFactory
    {
        
        public static SimilarQuery BuildSpatialSimilarQuery(double latitude, double longitude)
        {
            TermsObject to = BuildLocationTerm(latitude, longitude);
            TermContainer tc = new TermContainer(new []{to});
            return new SimilarQuery(new []{tc});
        }

        public static SimilarQuery BuildTemporalSimilarQuery(string utcTime)
        {
            TermsObject to = BuildTimeTerm(utcTime);
            TermContainer tc = new TermContainer(new[] { to });
            return new SimilarQuery(new[] { tc });
        }

        public static SimilarQuery BuildGlobalColEdgeSimilarQuery(string data) {
            TermsObject to = new TermsObject(TermsObject.IMAGE_TYPE, new [] {
                TermsObject.GLOBALCOLOR_CATEGORY, TermsObject.EDGE_CATEGORY
            });
            to.data = data;
            TermContainer tc = new TermContainer(new[] {to});
            return new SimilarQuery(new[]{tc});
        }

        public static SimilarQuery BuildMultiCategoryQuery(string[] categories, string data) {
            TermsObject to = new TermsObject(TermsObject.IMAGE_TYPE, categories);
            to.data = data;
            TermContainer tc = new TermContainer(new[]{to});
            return new SimilarQuery(new[]{tc});
        }

        public static SimilarQuery BuildMultiTermQuery(TermsObject[] terms) {
            return new SimilarQuery(new[]{new TermContainer(terms) });
        }
        
        /// <summary>
        /// Builds a new similarity query for the category 'edge'.
        /// </summary>
        /// <param name="data">A dataurl encoded image.</param>
        /// <returns></returns>
        public static SimilarQuery BuildEdgeSimilarQuery(string data) {
            TermsObject to = BuildEdgeTerm(data);
            TermContainer tc = new TermContainer(new[]{to});
            return new SimilarQuery(new[]{tc});
        }

        public static SimilarQuery BuildGlobalcolorSimilarQuery(string data) {
            TermsObject to = BuildGlobalcolorTerm(data);
            TermContainer tc = new TermContainer(new[]{to});
            return new SimilarQuery(new[]{tc});
        }

        public static TermsObject BuildLocationTerm(double latitude, double longitude)
        {
            var to = new TermsObject(TermsObject.LOCATION_TYPE, new[]
            {
                CineastUtils.Configuration.categories.spatial
            });
            to.data = string.Format("[{0},{1}]", latitude, longitude);
            return to;
        }

        public static TermsObject BuildEdgeTerm(string data) {
            var to = new TermsObject(TermsObject.IMAGE_TYPE, new []{TermsObject.EDGE_CATEGORY});
            to.data = data;
            return to;
        }

        public static TermsObject BuildQbETerm(string data) {
            var to = new TermsObject(TermsObject.IMAGE_TYPE, new[]{TermsObject.EDGE_CATEGORY,TermsObject.GLOBALCOLOR_CATEGORY});
            to.data = data;
            return to;
        }

        public static TermsObject BuildGlobalcolorTerm(string data) {
            var to = new TermsObject(TermsObject.IMAGE_TYPE, new []{TermsObject.GLOBALCOLOR_CATEGORY});
            to.data = data;
            return to;
        }

        public static TermsObject BuildTimeTerm(string utcTime)
        {
            var to = new TermsObject(TermsObject.TIME_TYPE,
                new[] {CineastUtils.Configuration.categories.temporal});
            to.data = utcTime;
            return to;
        }
    }
}