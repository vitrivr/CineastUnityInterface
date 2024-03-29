using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Org.Vitrivr.CineastApi.Model;
using UnityEngine;

namespace Vitrivr.UnityInterface.CineastApi.Model.Data
{
  /// <summary>
  /// Access and local representation of metadata for segments.
  /// </summary>
  [Serializable]
  public class SegmentMetadataStore : MetadataStore
  {
    public SegmentMetadataStore(string id)
    {
      SegmentId = id;
      Initialized = false;
    }

    public string SegmentId { get; private set; }

    public void Initialize(MediaSegmentMetadataQueryResult data)
    {
      if (Initialized)
      {
        Debug.LogWarning($"Attempted to initialize already initialized metadata for media object {SegmentId}!");
        return;
      }

      foreach (var meta in data.Content.Where(meta => meta.SegmentId == SegmentId))
      {
        if (!storage.ContainsKey(meta.Domain))
        {
          storage.Add(meta.Domain, new Dictionary<string, string>());
        }

        var domain = storage[meta.Domain];
        domain.Add(meta.Key, meta.Value);
      }

      Initialized = true;
    }
  }
}