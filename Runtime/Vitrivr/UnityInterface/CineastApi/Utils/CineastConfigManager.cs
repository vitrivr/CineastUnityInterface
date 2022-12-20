using System.IO;
using UnityEngine;
using Vitrivr.UnityInterface.CineastApi.Model.Config;

namespace Vitrivr.UnityInterface.CineastApi.Utils
{
  /// <summary>
  /// Manager to load the configuration from the file system
  /// </summary>
  public static class CineastConfigManager
  {
    /// <summary>
    /// The Cineast config. First attempts to read from file, otherwise uses default.
    /// The path is expected to be relative to the persistent file path.
    /// </summary>
    public static CineastConfig LoadConfigOrDefault(string configPath)
    {
#if UNITY_EDITOR
      var folder = Application.dataPath;
#else
      var folder = Application.persistentDataPath;
#endif
      var filePath = Path.Combine(folder, configPath);
      CineastConfig config;
      if (File.Exists(filePath))
      {
        var json = File.ReadAllText(filePath);
        config = CineastConfig.GetDefault();
        JsonUtility.FromJsonOverwrite(json, config);
      }
      else
      {
        config = CineastConfig.GetDefault();
      }

      return config;
    }

    /// <summary>
    /// Stores the currently active configuration
    /// </summary>
    public static void WriteConfig(CineastConfig cineastConfig, string filePath)
    {
      FileUtils.WriteJson(cineastConfig, filePath);
    }
  }
}