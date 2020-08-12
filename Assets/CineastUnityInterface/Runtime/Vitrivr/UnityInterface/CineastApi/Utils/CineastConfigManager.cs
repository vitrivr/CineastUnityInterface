using System.IO;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Model;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Model.Config;
using NUnit.Framework.Constraints;
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
            config = ReadJsonUnity<CineastConfig>(GetFilePath());
          }
          else
          {
            config = CineastConfig.GetDefault();
          }
        }
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
      WriteJsonUnity(config, GetFilePath());
    }

    /// <summary>
    /// Attempts to read the with path specified json file
    /// </summary>
    /// <param name="path"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private static T ReadJsonUnity<T>(string path)
    {
      StreamReader sr = File.OpenText(path);
      string content = sr.ReadToEnd();
      sr.Close();
      return UnityEngine.JsonUtility.FromJson<T>(content);
    }

    /// <summary>
    /// Requries Serializable on obj
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="path"></param>
    private static void WriteJsonUnity(object obj, string path)
    {
      StreamWriter sw = File.CreateText(path);
      sw.Write(JsonUtility.ToJson(obj));
      sw.WriteLine(""); // empty line at EOF
      sw.Flush();
      sw.Close();
    }

    public static string GetFilePath()
    {
      var folder = "";
#if UNITY_EDITOR
      folder = Application.dataPath;
#else
      folder = Application.persistentDataPath;
#endif
      return folder + $"/{FILE_NAME}.{FILE_EXTENSION}";
    }
  }
}