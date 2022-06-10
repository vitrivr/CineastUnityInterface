using System.IO;
using Org.Vitrivr.CineastApi.Client;
using UnityEngine;
using Vitrivr.UnityInterface.CineastApi.Model.Config;

namespace Vitrivr.UnityInterface.CineastApi.Utils
{
  /// <summary>
  /// Manager to load and store the configuration from the file system
  /// </summary>
  public class CineastConfigManager
  {
    public const string FILE_NAME = "cineastapi";
    public const string FILE_EXTENSION = "json";


    private static CineastConfigManager _instance;

    /// <summary>
    /// The single instance of the cineast config manager
    /// </summary>
    public static CineastConfigManager Instance => _instance ??= new CineastConfigManager();


    private CineastConfigManager()
    {
      apiConfig = new Configuration { BasePath = Config.cineastHost };
    }

    /// <summary>
    /// Cached cineast config. Access via public Config
    /// </summary>
    private CineastConfig _config;

    /// <summary>
    /// The cineast config. First attempts to read from file, otherwise uses default.
    /// </summary>
    public CineastConfig Config
    {
      get
      {
        if (_config == null)
        {
          var filePath = GetFilePath();
          if (File.Exists(filePath))
          {
            var streamReader = File.OpenText(filePath);
            var json = streamReader.ReadToEnd();
            streamReader.Close();
            _config = CineastConfig.GetDefault();
            JsonUtility.FromJsonOverwrite(json, _config);
          }
          else
          {
            _config = CineastConfig.GetDefault();
          }
        }

        _config.SanitizeCategories();
        return _config;
      }
      set => _config = value;
    }

    /// <summary>
    /// Internal apiConfig
    /// </summary>
    private Configuration apiConfig;

    /// <summary>
    /// Api config setup with basePath from config file
    /// </summary>
    public Configuration ApiConfiguration => apiConfig;

    /// <summary>
    /// Stores the currently active configuration
    /// </summary>
    public void StoreConfig()
    {
      FileUtils.WriteJson(_config, $"{FILE_NAME}.{FILE_EXTENSION}");
    }

    public static string GetFilePath()
    {
      var fileName = $"{FILE_NAME}.{FILE_EXTENSION}";

      // Check local directory first
      if (File.Exists(fileName))
      {
        return fileName;
      }

      // Check persistent data path second
      var persistentLocation = Path.Combine(Application.persistentDataPath, fileName);

      // Check in assets directory last for possibly provided default
      return File.Exists(persistentLocation) ? persistentLocation : Path.Combine(Application.dataPath, fileName);
    }
  }
}