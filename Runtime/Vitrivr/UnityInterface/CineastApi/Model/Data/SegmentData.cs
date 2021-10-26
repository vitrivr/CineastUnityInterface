using System;
using System.Threading;
using System.Threading.Tasks;
using Org.Vitrivr.CineastApi.Model;
using UnityEngine;
using Vitrivr.UnityInterface.CineastApi.Model.Registries;

namespace Vitrivr.UnityInterface.CineastApi.Model.Data
{
  /// <summary>
  /// Data object containing all information on a specific media segment. Use <see cref="SegmentRegistry"/> to
  /// instantiate.
  /// </summary>
  [Serializable]
  public class SegmentData
  {
    /// <summary>
    /// actual data
    /// </summary>
    private MediaSegmentDescriptor _descriptor;

    /// <summary>
    /// Segment ID uniquely identifying the corresponding media segment.
    /// </summary>
    private readonly string _id;

    // TODO: Consider combining lazy loading requests into batch requests every x seconds to reduce request overhead
    private static readonly SemaphoreSlim InitLock = new SemaphoreSlim(1, 1);

    public SegmentMetadataStore Metadata { get; private set; }


    public SegmentData(string id)
    {
      _id = id;
      Metadata = new SegmentMetadataStore(_id);
    }

    public SegmentData(MediaSegmentDescriptor descriptor)
    {
      _descriptor = descriptor;
      _id = descriptor.SegmentId;
      Metadata = new SegmentMetadataStore(_id);
      Initialized = true;
    }

    /// <summary>
    /// Runs asynchronous segment ID query to initialize the data for this segment.
    /// </summary>
    private async Task InitializeAsync(bool withMetadata = true)
    {
      await InitLock.WaitAsync();
      try
      {
        if (Initialized)
        {
          Debug.LogError($"Attempted to initialize already initialized segment with id \"{_id}\"!");
          return;
        }

        var queryResult = await CineastWrapper.SegmentApi.FindSegmentByIdAsync(_id);
        if (queryResult.Content.Count != 1)
        {
          throw new Exception(
            $"Unexpected number of segment data results for segment \"{_id}\": {queryResult.Content.Count}");
        }

        var result = queryResult.Content[0];
        Initialize(result);

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
    /// Initialize this <see cref="SegmentData"/> with a previously retrieved <see cref="MediaSegmentDescriptor"/>.
    /// </summary>
    /// <param name="data"><see cref="MediaSegmentDescriptor"/> containing the data for this media segment.</param>
    public void Initialize(MediaSegmentDescriptor data)
    {
      if (Initialized)
      {
        Debug.LogError($"Attempted to initialize already initialized segment with id \"{_id}\"!");
        return;
      }

      if (data.SegmentId != _id)
      {
        Debug.LogError($"Attempted to initialize segment with ID \"{_id}\" using MediaSegmentDescriptor" +
                       $" containing data for ID \"{data.SegmentId}\"!");
        return;
      }

      _descriptor = data;

      Initialized = true;
    }

    public void InitializeMeta(MediaSegmentMetadataQueryResult meta)
    {
      if (Metadata.Initialized)
      {
        Debug.LogWarning("Attempt to initialize already initialized segment metadata for media object with " +
                         $"id \"{Id}\". Using cached data.");
        return;
      }

      Metadata.Initialize(meta);
    }

    /// <summary>
    /// ID of the <see cref="MediaSegmentDescriptor"/>
    /// </summary>
    public string Id => Initialized ? _descriptor.SegmentId : _id;

    public MediaSegmentDescriptor Descriptor
    {
      get
      {
        if (Initialized)
        {
          return _descriptor;
        }

        throw new Exception("Not initialized"); // TODO
      }
    }


    /// <summary>
    /// Private flag whether actual data is available or not
    /// </summary>
    public bool Initialized { get; private set; }


    /// <summary>
    /// ID of the <see cref="MediaObjectDescriptor"/> this <see cref="MediaSegmentDescriptor"/> belongs to.
    /// </summary>
    /// <returns>Id of associated media object.</returns>
    public async Task<string> GetObjectId()
    {
      if (!Initialized)
      {
        await InitializeAsync();
      }

      return _descriptor.ObjectId;
    }

    /// <summary>
    /// Start of the {@link MediaSegmentDescriptor} within the {@link MediaObjectDescriptor} in frames (e.g. for videos or audio).
    /// </summary>
    /// <returns>Segment start in frames.</returns>
    public async Task<int> GetStart()
    {
      if (!Initialized)
      {
        await InitializeAsync();
      }

      return _descriptor.Start;
    }

    /// <summary>
    /// End of the {@link MediaSegmentDescriptor} within the {@link MediaObjectDescriptor} in frames (e.g. for videos or audio).
    /// </summary>
    /// <returns>Segment end in frames.</returns>
    public async Task<int> GetEnd()
    {
      if (!Initialized)
      {
        await InitializeAsync();
      }

      return _descriptor.End;
    }

    /// <summary>
    /// Relative position of the {@link MediaSegmentDescriptor} within the {@link MediaObjectDescriptor} (starts with 1)
    /// </summary>
    /// <returns>Sequence number of this segment.</returns>
    public async Task<int> GetSequenceNumber()
    {
      if (!Initialized)
      {
        await InitializeAsync();
      }

      return _descriptor.SequenceNumber;
    }

    /// <summary>
    /// Absolute start of the {@link MediaSegmentDescriptor} within the {@link MediaObjectDescriptor} in seconds (e.g. for videos or audio).
    /// </summary>
    /// <returns>Segment start in seconds.</returns>
    public async Task<float> GetAbsoluteStart()
    {
      if (!Initialized)
      {
        await InitializeAsync();
      }

      return _descriptor.Startabs;
    }

    /// <summary>
    /// Absolute end of the {@link MediaSegmentDescriptor} within the {@link MediaObjectDescriptor} in seconds (e.g. for videos or audio).
    /// </summary>
    /// <returns>Segment end in seconds.</returns>
    public async Task<float> GetAbsoluteEnd()
    {
      if (!Initialized)
      {
        await InitializeAsync();
      }

      return _descriptor.Endabs;
    }
  }
}