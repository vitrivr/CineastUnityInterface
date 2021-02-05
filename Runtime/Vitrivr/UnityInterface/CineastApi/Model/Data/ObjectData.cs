using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Model.Registries;
using Org.Vitrivr.CineastApi.Model;
using UnityEngine;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Model.Data
{
  /// <summary>
  ///   Wrapper object for <see cref="MediaObjectDescriptor" />, a specific media object.
  /// </summary>
  [Serializable]
  public class ObjectData
  {
    private static readonly SemaphoreSlim InitLock = new SemaphoreSlim(1, 1);

    /// <summary>
    ///   ObjectId uniquely identifying the corresponding media object.
    /// </summary>
    private readonly string _id;

    /// <summary>
    ///   The actual multimedia object.
    /// </summary>
    private MediaObjectDescriptor _descriptor;

    /// <summary>
    ///   The Segments contained in this Media Object.
    /// </summary>
    private List<SegmentData> _segments;

    /// <summary>
    ///   Constructs a new instance with the given id, for lazy loading.
    /// </summary>
    /// <param name="id"></param>
    public ObjectData(string id)
    {
      _id = id;
      Metadata = new MetadataStore(_id);
    }

    /// <summary>
    ///   Constructs a new instance with the given wrapper content.
    /// </summary>
    /// <param name="descriptor"></param>
    public ObjectData(MediaObjectDescriptor descriptor)
    {
      _id = descriptor.ObjectId;
      Metadata = new MetadataStore(_id);
      Initialize(descriptor);
    }

    /// <summary>
    ///   Private flag whether actual data is available or not
    /// </summary>
    public bool Initialized { get; private set; }

    public MetadataStore Metadata { get; private set; }

    /// <summary>
    ///   ID of this object's <see cref="MediaObjectDescriptor" />
    /// </summary>
    public string Id => Initialized ? _descriptor.ObjectId : _id;

    /// <summary>
    ///   Async (lazy loading) call to fill wrapper with content
    /// </summary>
    private async Task InitializeAsync(bool withMetadata = true)
    {
      await InitLock.WaitAsync();
      try
      {
        if (Initialized)
        {
          Debug.LogWarning($"Attempt to init already init'ed object with id \"{Id}\". Using cached data.");
          return;
        }

        var result = await CineastWrapper.ObjectApi.FindObjectsByAttributeAsync("id", _id);

        if (result.Content.Count != 1)
        {
          Debug.LogError($"Did not retrieve MediaObjectDescriptor for id {_id}");
        }
        else
        {
          Initialize(result.Content[0]);
        }

        if (withMetadata)
        {
          await Metadata.InitializeAsync();
        }
      }
      finally
      {
        InitLock.Release();
      }
    }

    /// <summary>
    /// Initialises this media object with the given descriptor.
    /// </summary>
    /// <param name="descriptor"></param>
    public void Initialize(MediaObjectDescriptor descriptor)
    {
      if (Initialized)
      {
        Debug.LogWarning($"Attempt to init already init'ed object with id \"{Id}\". Using cached data.");
        return;
      }

      if (descriptor.ObjectId != _id)
      {
        Debug.LogError($"Attempt to init failed. This id ({_id}) and descriptor's {descriptor.ObjectId} do not match.");
        return;
      }

      _descriptor = descriptor;
      Initialized = true;
    }

    public void InitializeMeta(MediaObjectMetadataQueryResult meta)
    {
      if (Metadata.Initialized)
      {
        Debug.LogWarning("Attempt to initialize already initialized object metadata for media object with id " +
                         $"\"{Id}\". Using cached data.");
        return;
      }

      Metadata.Initialize(meta);
    }

    /// <summary>
    ///   The name of the media object.
    /// </summary>
    /// <returns>Media object name.</returns>
    public async Task<string> GetName()
    {
      if (!Initialized)
      {
        await InitializeAsync();
      }

      return _descriptor.Name;
    }

    /// <summary>
    ///   The path that point to the file, relative to its original import direction.
    /// </summary>
    /// <returns>Path to media object file.</returns>
    public async Task<string> GetPath()
    {
      if (!Initialized)
      {
        await InitializeAsync();
      }

      return _descriptor.Path;
    }

    [Obsolete("This field is not properly set in cineast 3.0")]
    public async Task<string> GetContentUrl()
    {
      if (!Initialized)
      {
        await InitializeAsync();
      }

      return _descriptor.ContentURL;
    }

    /// <summary>
    /// Retrieves all <see cref="SegmentData"/> corresponding to this object.
    ///
    /// <b>Note</b> The segments have to be initialised beforehand, as only initialised segments are retrieved.
    /// </summary>
    /// <returns></returns>
    public async Task<List<SegmentData>> GetSegments()
    {
      if (_segments != null)
      {
        return _segments;
      }

      _segments = await SegmentRegistry.GetSegmentsOf(Id);

      return _segments;
    }

    private sealed class IdEqualityComparer : IEqualityComparer<ObjectData>
    {
      public bool Equals(ObjectData x, ObjectData y)
      {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x._id == y._id;
      }

      public int GetHashCode(ObjectData obj)
      {
        return obj._id != null ? obj._id.GetHashCode() : 0;
      }
    }

    public static IEqualityComparer<ObjectData> IdComparer { get; } = new IdEqualityComparer();

    public MediaObjectDescriptor MediaObject
    {
      get
      {
        if (Initialized)
        {
          return _descriptor;
        }

        throw new ArgumentException("Cannot get descriptor of uninitialised object: " + Id);
      }
    }
  }
}