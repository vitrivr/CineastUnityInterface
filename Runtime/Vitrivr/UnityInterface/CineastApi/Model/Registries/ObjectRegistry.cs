﻿using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Org.Vitrivr.CineastApi.Model;
using UnityEngine;
using Vitrivr.UnityInterface.CineastApi.Model.Data;

namespace Vitrivr.UnityInterface.CineastApi.Model.Registries
{
  /// <summary>
  ///   Class for instantiation and management of <see cref="ObjectData"/>.
  /// </summary>
  public static class ObjectRegistry
  {
    /// <summary>
    /// Internal storage and dictionary for media objects
    /// </summary>
    private static readonly ConcurrentDictionary<string, ObjectData> Registry =
      new ConcurrentDictionary<string, ObjectData>();

    public static List<ObjectData> Objects => Registry.Values.ToList();

    /// <summary>
    /// Returns whether the registry has an item for the given id.
    /// </summary>
    /// <param name="id">The ObjectID to look for</param>
    /// <returns>TRUE if such an item exists, FALSE otherwise</returns>
    public static bool Exists(string id)
    {
      return Registry.ContainsKey(id);
    }

    /// <summary>
    /// Resets the registry by clearing all object entries.
    /// <b>Caution</b> This will actively clear the local cache.
    /// </summary>
    public static void Reset()
    {
      Registry.Clear();
    }

    /// <summary>
    ///   Retrieves the object for the given ID. If it does not exist, it will be created.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static ObjectData GetObject(string id)
    {
      if (!Exists(id))
      {
        Registry.TryAdd(id, new ObjectData(id));
      }

      return Registry[id];
    }

    /// <summary>
    /// Initialises the registry with data from a previously initialised <see cref="SegmentRegistry"/>
    /// </summary>
    /// <param name="withMeta">Whether to initialise with metadata or not</param>
    /// <returns></returns>
    public static async Task Initialize(bool withMeta = false)
    {
      if (withMeta)
      {
        await BatchFetchObjectDataWithMeta(SegmentRegistry.GetObjects());
      }
      else
      {
        await BatchFetchObjectData(SegmentRegistry.GetObjects());
      }
    }

    public static async Task<ObjectData> GetObjectOf(string segmentId)
    {
      return await SegmentRegistry.GetObjectOf(segmentId);
    }

    public static async Task BatchFetchObjectData(IEnumerable<ObjectData> objects)
    {
      var toInit = objects.Where(obj => !obj.Initialized).Select(obj => obj.Id).ToList();
      var results = await Task.Run(() => CineastWrapper.ObjectApi.FindObjectsByIdBatched(new IdList(toInit)));
      results.Content.ForEach(dto => GetObject(dto.ObjectId).Initialize(dto));
    }

    public static async Task BatchFetchObjectDataWithMeta(List<ObjectData> objects)
    {
      Debug.Log("Fetching obj data, then metadata");
      await BatchFetchObjectData(objects);
      await BatchFetchObjectMetadata(objects);
    }

    public static async Task BatchFetchObjectMetadata(IEnumerable<ObjectData> objects)
    {
      var toInitObj = objects.Where(obj => !obj.ObjectMetadata.Initialized).ToList();
      var toInit = toInitObj.Select(obj => obj.Id).ToList();
      Debug.Log($"Having to initialise {toInit.Count} obj's metadata");
      var result = await Task.Run(() =>
        CineastWrapper.MetadataApi.FindMetadataForObjectIdBatchedAsync(new OptionallyFilteredIdList(ids: toInit)));
      foreach (var obj in toInitObj)
      {
        obj.ObjectMetadata.Initialize(result);
      }
      Debug.Log("Finished fetching obj");
    }
  }
}