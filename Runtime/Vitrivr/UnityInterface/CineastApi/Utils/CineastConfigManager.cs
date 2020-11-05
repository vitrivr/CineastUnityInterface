using System.IO;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Model.Config;
using Org.Vitrivr.CineastApi.Client;
using UnityEngine;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Utils
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
    public static CineastConfigManager Instance => _instance ?? (_instance = new CineastConfigManager());


    private CineastConfigManager()
    {
      apiConfig = new Configuration {BasePath = Config.cineastHost};
    }
    
    /// <summary>
    /// Cached cineast config. Acceess via public Config
    /// </summary>
    private CineastConfig config;

    /// <summary>
    /// The cineast config. First attempts to read from file, otherwise uses default.
    /// </summary>
    public CineastConfig Config
    {
      get
      {
        if (config == null)
        {
          if (File.Exists(GetFilePath()))
          {
            var streamReader = File.OpenText(GetFilePath());
            var json = streamReader.ReadToEnd();
            streamReader.Close();
            config = CineastConfig.GetDefault();
            JsonUtility.FromJsonOverwrite(json, config);
          }
          else
          {
            config = CineastConfig.GetDefault();
          }
        }

        config.SanitizeCategories();
        return config;
      }
      set => config = value;
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
      FileUtils.WriteJson(config, GetFilePath());
    }

    public static string GetFilePath()
    {
      var folder = "";
#if UNITY_EDITOR
      folder = Application.dataPath;
#else
      folder = Application.persistentDataPath;
#endif
      return Path.Combine(folder, $"{FILE_NAME}.{FILE_EXTENSION}");
    }
  }
}