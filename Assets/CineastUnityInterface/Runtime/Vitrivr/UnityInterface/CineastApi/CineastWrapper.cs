using System;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Utils;
using Org.Vitrivr.CineastApi.Client;
using UnityEngine;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi
{
  /// <summary>
  /// Wrapper for cineast
  /// </summary>
  public class CineastWrapper : MonoBehaviour
  {
    
    private void Awake()
    {
      var cineastHost = CineastConfigManager.Instance.Config.cineastHost;
    }
  }
  
  
}