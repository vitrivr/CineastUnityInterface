using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.Assertions;

namespace Vitrivr.UnityInterface.CineastApi.Model.Data
{
  [Serializable]
  public abstract class MetadataStore
  {
    /// <summary>
    /// Actual internal storage of metadata
    /// </summary>
    protected Dictionary<string, Dictionary<string, string>> storage = new();

    public bool Initialized { get; protected set; }

    public Dictionary<string, Dictionary<string, string>> GetAll()
    {
      if (!Initialized)
      {
        throw new Exception("Metadata store being accessed before initialization!");
      }

      return storage;
    }

    public bool DomainExists(string domain)
    {
      Assert.IsTrue(Initialized);
      return storage.ContainsKey(domain);
    }

    public string Get(string domain, string key)
    {
      Assert.IsTrue(Initialized);
      return storage[domain][key];
    }

    /// <summary>
    /// Retrieves a metadata value using the DOMAIN.KEY notation.
    /// Requires metadata to be initialized.
    /// </summary>
    public string Get(string str)
    {
      Assert.IsTrue(Initialized);
      var domainAndKey = str.Split('.');
      if (domainAndKey.Length >= 1)
      {
        return storage[domainAndKey[0]][domainAndKey[1]];
      }

      throw new ArgumentException("Cannot retrieve without domain");
    }

    /// <summary>
    /// Retrieves all metadata of a specific domain.
    /// Requires metadata to be initialized.
    /// </summary>
    public List<(string Key, string Value)> GetDomain(string domain)
    {
      Assert.IsTrue(Initialized);
      var items = storage[domain];
      return items.Keys.Select(key => (key, items[key])).ToList();
    }

    /// <summary>
    /// Checks if a domain key pair exists in the metadata.
    /// Requires metadata to be initialized.
    /// </summary>
    public bool Exists(string domain, string key)
    {
      Assert.IsTrue(Initialized);
      return DomainExists(domain) && storage[domain].ContainsKey(key);
    }
  }
}