using System;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Utils;
using UnityEngine;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi
{
  /// <summary>
  /// Wrapper for cineast
  /// </summary>
  public class CineastWrapper : MonoBehaviour
  {
    /// <summary>
    /// Initialises the wrapper
    /// </summary>
    private void Awake()
    {
      LoadConfig();
    }

    /// <summary>
    /// Loads or stores the default configuration
    /// </summary>
    private void LoadConfig()
    {
      if (CineastConfiguration.HasConfig())
      {
        var cfg = CineastConfiguration.Load();
        if (!cfg.IsEmpty())
        {
          CineastUtils.Configuration = cfg;
        }
        else
        {
          CineastUtils.Configuration = CineastConfiguration.GetDefault();
        }
      }
      else
      {
        CineastConfiguration.StoreDefault();
      }
    }
  }
}