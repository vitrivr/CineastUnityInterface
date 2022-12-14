using System;
using Org.Vitrivr.CineastApi.Client;

namespace Vitrivr.UnityInterface.CineastApi.Model.Config
{
  [Serializable]
  public class CineastConfig
  {
    /// <summary>
    /// The host address of cineast.
    /// Defaults to http://localhost:4567/
    /// </summary>
    public string cineastHost;

    /// <summary>
    /// The host address for media items.
    /// Defaults to http://localhost/
    /// </summary>
    public string mediaHost;

    /// <summary>
    /// If true, cineast is expected to serve the media as "thumbnails/:s" and "objects/:o", hence we'll try to load them from there
    /// </summary>
    public bool cineastServesMedia;

    /// <summary>
    /// The path to thumbnail files.
    /// Defaults to "thumbnails/:o/:s"
    /// </summary>
    public string thumbnailPath;

    /// <summary>
    /// The thumbnail file extension.
    /// Defaults to ".jpg"
    /// </summary>
    public string thumbnailExtension;

    /// <summary>
    /// The path to original media files.
    /// Defaults to "collection/:p"
    /// </summary>
    public string mediaPath;

    /// <summary>
    /// The category names mapped to known names.
    /// Usually this can be left to default.
    /// </summary>
    public CategoryMappings categoryMappings;

    public CineastConfig()
    {
      // empty constructor
    }

    public CineastConfig(
      string cineastHost,
      string mediaHost,
      string thumbnailPath,
      string thumbnailExtension,
      string mediaPath)
    {
      this.cineastHost = SanitizeHost(cineastHost);
      this.mediaHost = SanitizeHost(mediaHost);
      this.thumbnailPath = thumbnailPath;
      this.thumbnailExtension = thumbnailExtension;
      this.mediaPath = mediaPath;
      categoryMappings = CategoryMappings.GetDefault();
    }

    public bool IsEmpty()
    {
      return string.IsNullOrEmpty(cineastHost) || string.IsNullOrEmpty(mediaHost);
    }

    /// <summary>
    /// Ensures that the host starts with "http://" and ends with "/"
    /// </summary>
    /// <param name="host">The host address to sanitize</param>
    /// <returns></returns>
    private string SanitizeHost(string host)
    {
      if (!string.IsNullOrEmpty(host) && !host.StartsWith("http://"))
      {
        host = "http://" + host;
      }

      if (!host.EndsWith("/"))
      {
        host += "/";
      }

      return host;
    }

    public static CineastConfig GetDefault()
    {
      return new CineastConfig("http://localhost:4567/", "http://localhost/", "thumbnails/:o/:s", ".jpg",
        "collection/:p");
    }

    public void SanitizeCategories()
    {
      if (categoryMappings.mapping.Count == 0)
      {
        categoryMappings = CategoryMappings.GetDefault();
      }
    }

    public Configuration getApiConfig()
    {
      return new Configuration { BasePath = cineastHost };
    }
  }
}