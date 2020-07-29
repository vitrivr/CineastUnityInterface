using System;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Utils;
using Org.Vitrivr.Cineast.Api;
using Org.Vitrivr.Cineast.Client;
using Org.Vitrivr.Cineast.Model;
using UnityEngine;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi
{
  public class ExperimentalApi
  {
    private CineastConfiguration config;

    private ObjectApi objectApi;

    public ExperimentalApi(CineastConfiguration config)
    {
      this.config = config;
      InitApis();
    }

    private void InitApis()
    {
      Configuration.Default.BasePath = config.cineastHost;
      objectApi = new ObjectApi(Configuration.Default);
    }

    public MediaObjectQueryResult ListAllObjects()
    {
      return objectApi.FindObjectsAll("unused");
    }
  }
}