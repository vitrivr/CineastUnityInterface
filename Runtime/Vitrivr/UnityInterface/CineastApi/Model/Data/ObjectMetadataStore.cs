using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

      foreach (var meta in data.Content.Where(meta => meta.ObjectId == ObjectId))
      {
        if (!DomainExists(meta.Domain))
        {
          _storage.Add(meta.Domain, new Dictionary<string, string>());
        }

        var domain = _storage[meta.Domain];
        domain.Add(meta.Key, meta.Value);
      }

      Initialized = true;
    }

    public override async Task InitializeAsync()
    {
      if (Initialized)
      {
        Debug.LogWarning($"Attempted to initialize already initialized metadata for media object {ObjectId}!");
        return;
      }

      var metadataResult = await CineastWrapper.MetadataApi.FindMetaByIdAsync(ObjectId);
      if (!Initialized)
      {
        Initialize(metadataResult);
      }
    }
  }
}