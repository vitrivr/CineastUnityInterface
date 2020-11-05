using System;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Legacy.Models.Messages.Query
{
    [Serializable]
    public class TermsObject
    {
        public const string SPATIAL_CATEGORY = "spatial";
        public const string LOCATION_TYPE = "LOCATION";

        public const string TERMPORAL_CATEGORY = "temporal";
        public const string TIME_TYPE = "TIME";


        public const string IMAGE_TYPE = "IMAGE";
        public const string GLOBALCOLOR_CATEGORY = "globalcolor";
        public const string LOCALCOLOR_CATEGORY = "localcolor";
        public const string EDGE_CATEGORY = "edge";
        public const string QUANTIZED_CATEGORY = "quantized";

        public string[] categories;
        public string type;

        public string data;

        

        public TermsObject(string type, string[] categories)
        {
            this.type = type;
            this.categories = categories;
        }

        public static TermsObject BuildLocationTermsObject(double latitude, double longitude)
        {
            TermsObject built = new TermsObject(LOCATION_TYPE, new []{SPATIAL_CATEGORY});
            built.data = String.Format("[{0},{1}]", latitude, longitude);
            return built;
        }
    }
}