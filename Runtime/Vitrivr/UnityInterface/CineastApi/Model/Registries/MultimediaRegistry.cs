using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Org.Vitrivr.CineastApi.Model;
using UnityEngine;
using Vitrivr.UnityInterface.CineastApi.Model.Data;

namespace Vitrivr.UnityInterface.CineastApi.Model.Registries
{
  /// <summary>
  /// Class for the instantiation and management of <see cref="SegmentData"/> and <see cref="ObjectData"/> objects.
  /// </summary>
  public class MultimediaRegistry
  {
    private readonly ConcurrentDictionary<string, SegmentData> _segmentRegistry = new();
    private readonly ConcurrentDictionary<string, ObjectData> _objectRegistry = new();

    private readonly CineastClient _cineastClient;

    public MultimediaRegistry(CineastClient cineastClient)
    {
      _cineastClient = cineastClient;
    }

    /// <summary>
    /// Retrieves the segment for the given ID.
    /// If it does not exist, it is created uninitialized.
    /// </summary>
    /// <param name="id">The segment id of the segment to retrieve</param>
    /// <returns>The <see cref="SegmentData"/> corresponding to the id (or a new one).</returns>
    public SegmentData GetSegment(string id)
    {
      if (_segmentRegistry.ContainsKey(id)) return _segmentRegistry[id];

      var segment = new SegmentData(id, this);
      _segmentRegistry.TryAdd(id, segment);

      return _segmentRegistry[id];
    }

    /// <summary>
    /// Returns segments corresponding to an object, by its id.
    /// </summary>
    /// <param name="objectId"></param>
    /// <returns></returns>
    public async Task<List<SegmentData>> GetSegmentsOf(string objectId)
    {
      var results = await _cineastClient.SegmentApi.FindSegmentByObjectIdAsync(objectId);
      var segmentDescriptors = results.Content;
      return segmentDescriptors.Select(descriptor =>
      {
        var segment = GetSegment(descriptor.SegmentId);
        if (!segment.Initialized)
        {
          segment.Initialize(descriptor);
        }

        return segment;
      }).ToList();
    }

    /// <summary>
    /// Batch fetches data for the given segments.
    /// </summary>
    /// <param name="segments">The segments for which to fetch the data.</param>
    public async Task BatchFetchSegmentData(IEnumerable<SegmentData> segments)
    {
      var uninitializedSegments = segments.Where(segment => !segment.Initialized).ToList();
      if (uninitializedSegments.Count == 0)
      {
        // All segments already initialized
        return;
      }

      var segmentIds = uninitializedSegments.Select(segment => segment.Id).ToList();

      var results = await _cineastClient.SegmentApi.FindSegmentByIdBatchedAsync(new IdList(segmentIds));

      results.Content.ForEach(data => GetSegment(data.SegmentId).Initialize(data));
    }

    /// <summary>
    /// Batch fetches all uninitialized segments in the registry.
    /// </summary>
    public async Task BatchFetchAllSegments()
    {
      await BatchFetchSegmentData(_segmentRegistry.Values);
    }

    /// <summary>
    /// Extracts the object of previously initialised segments.
    /// </summary>
    /// <returns></returns>
    public List<ObjectData> GetObjects()
    {
      var oIds = new HashSet<string>();
      _segmentRegistry.Values.Where(segment => segment.Initialized).ToList()
        .ForEach(segment => oIds.Add(segment.GetObjectId().Result));
      return oIds.Select(oid => new ObjectData(oid, this)).ToList();
    }

    /// <summary>
    /// Resets the registry by clearing all segment entries.
    ///
    /// ATTENTION: Be aware that this means all <see cref="SegmentData"/> objects in use must be released as well to
    /// ensure data integrity.
    /// </summary>
    public void Reset()
    {
      _segmentRegistry.Clear();
      _objectRegistry.Clear();
    }

    /// <summary>
    /// Whether the given segmentId is known to this registry.
    /// </summary>
    /// <param name="segmentId"></param>
    /// <returns></returns>
    public bool SegmentExists(string segmentId)
    {
      return _segmentRegistry.ContainsKey(segmentId);
    }

    /// <summary>
    /// Returns whether the registry has an item for the given id.
    /// </summary>
    /// <param name="id">The ObjectID to look for</param>
    /// <returns>TRUE if such an item exists, FALSE otherwise</returns>
    public bool ObjectExists(string id)
    {
      return _objectRegistry.ContainsKey(id);
    }

    /// <summary>
    ///   Retrieves the object for the given ID. If it does not exist, it will be created.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ObjectData GetObject(string id)
    {
      if (!ObjectExists(id))
      {
        _objectRegistry.TryAdd(id, new ObjectData(id, this));
      }

      return _objectRegistry[id];
    }

    /// <summary>
    /// Gets the corresponding object of the segment specified.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public async Task<ObjectData> GetObjectOf(string segmentId)
    {
      if (!SegmentExists(segmentId))
      {
        throw new ArgumentException($"Cannot get object of unknown segment with id {segmentId}");
      }

      var segment = _segmentRegistry[segmentId];
      if (segment.Initialized)
      {
        return GetObject(segment.GetObjectId().Result);
      }

      var objectId = await segment.GetObjectId();
      return GetObject(objectId);
    }

    public async Task InitializeObject(ObjectData objectData)
    {
      // Ensure this object actually originates from this registry
      if (!ObjectExists(objectData.Id))
      {
        Debug.LogError($"Unknown object attempted initialization: {objectData.Id}");
        return;
      }

      var result = await _cineastClient.ObjectApi.FindObjectsByAttributeAsync("id", objectData.Id);

      if (result.Content.Count != 1)
      {
        throw new Exception(
          $"Unexpected number of object data results for object \"{objectData.Id}\": {result.Content.Count}");
      }

      objectData.Initialize(result.Content[0]);
    }

    public async Task InitializeObjectMetadata(ObjectData objectData)
    {
      // Ensure the object metadata is not already initialized
      if (objectData.Metadata.Initialized)
      {
        Debug.LogError($"Metadata of object already initialized: {objectData.Id}");
      }

      var metadataResult = await _cineastClient.MetadataApi.FindMetaByIdAsync(objectData.Id);
      objectData.InitializeMeta(metadataResult);
    }

    public async Task InitializeSegment(SegmentData segmentData)
    {
      // Ensure this segment actually originates from this registry
      if (!SegmentExists(segmentData.Id))
      {
        Debug.LogError($"Unknown segment attempted initialization: {segmentData.Id}");
        return;
      }

      var result = await _cineastClient.SegmentApi.FindSegmentByIdAsync(segmentData.Id);

      if (result.Content.Count != 1)
      {
        throw new Exception(
          $"Unexpected number of segment data results for segment \"{segmentData.Id}\": {result.Content.Count}");
      }

      segmentData.Initialize(result.Content[0]);
    }

    public async Task InitializeSegmentMetadata(SegmentData segmentData)
    {
      // Ensure the segment metadata is not already initialized
      if (segmentData.Metadata.Initialized)
      {
        Debug.LogError($"Metadata of segment already initialized: {segmentData.Id}");
      }

      var metadataResult = await _cineastClient.MetadataApi.FindSegMetaByIdAsync(segmentData.Id);
      segmentData.InitializeMeta(metadataResult);
    }

    /// <summary>
    /// Initialises the registry with data from a previously initialised <see cref="MultimediaRegistry"/>
    /// </summary>
    /// <param name="withMeta">Whether to initialise with metadata or not</param>
    /// <returns></returns>
    public async Task BatchFetchAllObjects(bool withMeta = false)
    {
      if (withMeta)
      {
        await BatchFetchObjectDataWithMeta(GetObjects());
      }
      else
      {
        await BatchFetchObjectData(GetObjects());
      }
    }

    public async Task BatchFetchObjectData(IEnumerable<ObjectData> objects)
    {
      var toInit = objects.Where(obj => !obj.Initialized).Select(obj => obj.Id).ToList();
      var results = await _cineastClient.ObjectApi.FindObjectsByIdBatchedAsync(new IdList(toInit));
      results.Content.ForEach(dto => GetObject(dto.Objectid).Initialize(dto));
    }

    public async Task BatchFetchObjectDataWithMeta(List<ObjectData> objects)
    {
      await BatchFetchObjectData(objects);
      await BatchFetchObjectMetadata(objects);
    }

    public async Task BatchFetchObjectMetadata(IEnumerable<ObjectData> objects)
    {
      var toInitObj = objects.Where(obj => !obj.Metadata.Initialized).ToList();
      var toInit = toInitObj.Select(obj => obj.Id).ToList();
      var result =
        await _cineastClient.MetadataApi.FindMetadataForObjectIdBatchedAsync(new OptionallyFilteredIdList(ids: toInit));
      foreach (var obj in toInitObj)
      {
        obj.Metadata.Initialize(result);
      }
    }

    public async Task<string> GetMediaUrlOfAsync(ObjectData objectData, string segmentId = null)
    {
      return await _cineastClient.GetMediaUrlOfAsync(objectData, segmentId);
    }

    public async Task<string> GetThumbnailUrlOfAsync(SegmentData segment)
    {
      return await _cineastClient.GetThumbnailUrlOfAsync(segment);
    }

    public async Task<List<Tag>> GetTags(string segmentId)
    {
      var tagIds = await _cineastClient.MetadataApi.FindTagInformationByIdAsync(segmentId);
      var tagsResult = await _cineastClient.TagApi.FindTagsByIdAsync(new IdList(tagIds.TagIDs));

      return tagsResult.Tags;
    }
  }
}