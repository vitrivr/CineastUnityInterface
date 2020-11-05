namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Utils
{
    public class PathResolver
    {
        /// <summary>
        /// Resolves a media path by replacing any resource tokens with their respective values.
        /// </summary>
        /// <param name="path">The path to resolve</param>
        /// <param name="objectId">The ID of the media object</param>
        /// <param name="segmentId">The ID of the media object segment</param>
        /// <param name="mediaPath">The path of the media object in its resource location</param>
        /// <param name="mediaType">The media type of the media object</param>
        /// <returns>The resolved path</returns>
        public static string ResolvePath(string path,
            string objectId = null,
            string segmentId = null,
            string mediaPath = null,
            string mediaType = null)
        {
            return path
                .Replace(":o", objectId)
                .Replace(":s", segmentId)
                .Replace(":p", mediaPath)
                .Replace(":t", mediaType);
        }
    }
}