using System;
using System.Collections.Generic;
using System.Linq;
using Org.Vitrivr.CineastApi.Model;
using UnityEngine;

namespace Vitrivr.UnityInterface.CineastApi.Model.Data
{
  /// <summary>
  /// Access and local representation of metadata
  /// </summary>
  [Serializable]
  public class ObjectMetadataStore : MetadataStore
  {
    public ObjectMetadataStore(string id)
    {
      ObjectId = id;
      Initialized = false;
    }

    public string ObjectId { get; private set; }

    public void Initialize(MediaObjectMetadataQueryResult data)
    {
      if (Initialized)
      {
        Debug.LogWarning("Attempt to init already init'ed metadata container using cache data");
        return;
      }

      foreach (var meta in data.Content.Where(meta => meta.Objectid == ObjectId))
      {
        if (!Storage.ContainsKey(meta.Domain))
        {
          Storage.Add(meta.Domain, new Dictionary<string, string>());
        }

        var domain = Storage[meta.Domain];
        domain.Add(meta.Key, meta.Value);
      }

      Initialized = true;
    }
  }
}