using System;
using System.Collections.Generic;
using System.Linq;
using Org.Vitrivr.CineastApi.Model;
using UnityEngine;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Model.Data
{
  /// <summary>
  /// Access and local representation of metadata
  /// </summary>
  [Serializable]
  public class MetadataStore
  {

    public MetadataStore(string id)
    {
      ObjectId = id;
      Initialized = false;
    }
    
    /// <summary>
    /// Actual internal storage of metadata
    /// </summary>
    private Dictionary<string, Dictionary<string, string>> storage = new Dictionary<string, Dictionary<string, string>>();

    public string ObjectId { get; private set; }

    public bool Initialized { get; private set; }

    public void Initialize(MediaObjectMetadataQueryResult data)
    {
      if (Initialized)
      {
        Debug.LogWarning($"Attempt to init already init'ed metadata container. using cache data");
        return;
      }

      foreach (var meta in data.Content.Where(meta => meta.ObjectId == ObjectId))
      {
        if (!DomainExists(meta.Domain))
        {
          storage.Add(meta.Domain, new Dictionary<string, string>());          
        }

        var domain = storage[meta.Domain];
        domain.Add(meta.Key, meta.Value);
      }

      Initialized = true;
    }

    public bool DomainExists(string domain)
    {
      return storage.ContainsKey(domain);
    }

    public string Get(string domain, string key)
    {
      return storage[domain][key];
    }

    /// <summary>
    /// Retrieves a metadata value using the DOMAIN.KEY notation
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public string Get(string str)
    {
      var domainAndKey = str.Split('.');
      if (domainAndKey.Length >= 1)
      {
        return storage[domainAndKey[0]][domainAndKey[1]];
      }
      else
      {
        throw new ArgumentException("Cannot retrieve without domain");
      }
    }

    public List<(string Key, string Value)> GetDomain(string domain)
    {
      var items = storage[domain];
      return items.Keys.Select(key => (key, items[key])).ToList();
    }

    public bool Exists(string domain, string key)
    {
      if (DomainExists(domain))
      {
        foreach (var valueTuple in GetDomain(domain))
        {
          if (valueTuple.Key == key)
          {
            return true;
          }
        }
      }

      return false;
    }
    
    
    
  }
}