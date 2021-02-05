using System;
using System.Threading;
using System.Threading.Tasks;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Model.Registries;
using Org.Vitrivr.CineastApi.Model;
using UnityEngine;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Model.Data
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


    public SegmentData(string id)
    {
      _id = id;
    }

    public SegmentData(MediaSegmentDescriptor descriptor)
    {
      _descriptor = descriptor;
      _id = descriptor.SegmentId;
      Initialized = true;
    }

    /// <summary>
    /// Runs asynchronous segment ID query to initialize the data for this segment.
    /// </summary>
    private async Task InitializeAsync()
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