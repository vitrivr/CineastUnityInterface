using System;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Model
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
    
    public CineastConfig()
    {
      // empty constructor
    }

    public CineastConfig(
      string cineastHost,
      string mediaHost)
    {
      this.cineastHost = SanitizeHost(cineastHost);
      this.mediaHost = SanitizeHost(mediaHost);
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
        host += "";
      }
      return host;
    }
    
  }
}