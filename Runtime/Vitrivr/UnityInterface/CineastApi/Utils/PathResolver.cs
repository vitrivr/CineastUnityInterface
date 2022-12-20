using System;

namespace Vitrivr.UnityInterface.CineastApi.Utils
{
  public class PathResolver
  {
    /// <summary>
    /// Resolves a media path by replacing any resource tokens with their respective values.
    /// </summary>
    /// <param name="path">The path to resolve</param>
    /// <param name="objectId">The ID of the media object</param>
    /// <param name="segmentId">The ID of the media object segment</param>
    /// <param name="objectName">The file name of the media object</param>
    /// <param name="objectPath">The path of the media object in its resource location</param>
    /// <param name="mediaType">The media type of the media object</param>
    /// <param name="extension">The file extension</param>
    /// <returns>The resolved path</returns>
    public static string ResolvePath(string path,
      string objectId = null,
      string segmentId = null,
      string objectName = null,
      string objectPath = null,
      string mediaType = null,
      string extension = null)
    {
      return path
        .Replace(":o", objectId)
        .Replace(":s", segmentId)
        .Replace(":S", segmentId?[3..])
        .Replace(":n", objectName)
        .Replace(":p", objectPath)
        .Replace(":t", mediaType)
        .Replace(":T", mediaType?.ToUpper())
        .Replace(":x", extension);
    }

    /// <summary>
    /// Combines URL parts similarly to how Path.Combine combines paths.
    /// </summary>
    /// <param name="parts">URL parts to combine.</param>
    /// <returns>The combined URL.</returns>
    public static string CombineUrl(params string[] parts)
    {
      var url = new Uri(parts[0]);
      for (var i = 1; i < parts.Length; i++)
      {
        url = new Uri(url, parts[i]);
      }

      return url.ToString();
    }
  }
}