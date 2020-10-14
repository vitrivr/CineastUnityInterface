using System;
using System.Threading;
using System.Threading.Tasks;
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
    private MediaSegmentDescriptor descriptor;
    
    /// <summary>
    /// Segment ID uniquely identifying the corresponding media segment.
    /// </summary>
    private readonly string id;

/// <summary>
/// Private flag whether actual data is available or not
/// </summary>
    private bool initialized;

    // TODO: Consider combining lazy loading requests into batch requests every x seconds to reduce request overhead
    private static readonly SemaphoreSlim InitLock = new SemaphoreSlim(1, 1);


    public SegmentData(string id)
    {
      this.id = id;
    }

    public SegmentData(MediaSegmentDescriptor descriptor)
    {
      this.descriptor = descriptor;
      this.id = descriptor.SegmentId;
      initialized = true;
    }

    /// <summary>
    /// Runs asynchronous segment ID query to initialize the data for this segment.
    /// </summary>
    private async Task InitializeAsync()
    {
      await InitLock.WaitAsync();
      try
      {
        if (initialized)
        {
          Debug.LogError($"Attempted to initialize already initialized segment with id \"{id}\"!");
          return;
        }

        var queryResult = await CineastWrapper.SegmentApi.FindSegmentByIdAsync(id);
        // TODO: Error handling in the data breaking case there is no or more than one segment returned
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
      if (initialized)
      {
        Debug.LogError($"Attempted to initialize already initialized segment with id \"{id}\"!");
        return;
      }

      if (data.SegmentId != id)
      {
        Debug.LogError($"Attempted to initialize segment with ID \"{id}\" using MediaSegmentDescriptor" +
                       $" containing data for ID \"{data.SegmentId}\"!");
        return;
      }

      descriptor = data;

      initialized = true;
    }
    /// <summary>
    /// ID of the <see cref="MediaSegmentDescriptor"/>
    /// </summary>
    public string Id => Initialized ? descriptor.SegmentId : id;

    public MediaSegmentDescriptor Descriptor
    {
      get
      {
        if (Initialized)
        {
          return descriptor;
        }
        throw new Exception("Not initialized"); // TODO
      } 
    }


    public bool Initialized => initialized;

    /// <summary>
    /// ID of the <see cref="MediaObjectDescriptor"/> this <see cref="MediaSegmentDescriptor"/> belongs to.
    /// </summary>
    /// <returns></returns>
    public async Task<string> GetObjectId()
    {
      if (!Initialized)
      {
        await InitializeAsync();
      }

      return descriptor.ObjectId;
    }

    /// <summary>
    /// Start of the {@link MediaSegmentDescriptor} within the {@link MediaObjectDescriptor} in frames (e.g. for videos or audio).
    /// </summary>
    /// <returns></returns>
    public async Task<int> GetStart()
    {
      if (!Initialized)
      {
        await InitializeAsync();
      }

      return descriptor.Start;
    }

    /// <summary>
    /// End of the {@link MediaSegmentDescriptor} within the {@link MediaObjectDescriptor} in frames (e.g. for videos or audio).
    /// </summary>
    /// <returns></returns>
    public async Task<int> GetEnd()
    {
      if (!Initialized)
      {
        await InitializeAsync();
      }

      return descriptor.End;
    }

    /// <summary>
    /// Relative position of the {@link MediaSegmentDescriptor} within the {@link MediaObjectDescriptor} (starts with 1)
    /// </summary>
    /// <returns></returns>
    public async Task<int> GetSequenceNumber()
    {
      if (!Initialized)
      {
        await InitializeAsync();
      }

      return descriptor.SequenceNumber;
    }

    /// <summary>
    /// Absolute start of the {@link MediaSegmentDescriptor} within the {@link MediaObjectDescriptor} in seconds (e.g. for videos or audio).
    /// </summary>
    /// <returns></returns>
    public async Task<float> GetAbsoluteStart()
    {
      if (!Initialized)
      {
        await InitializeAsync();
      }

      return descriptor.Startabs;
    }

    /// <summary>
    /// Absolute end of the {@link MediaSegmentDescriptor} within the {@link MediaObjectDescriptor} in seconds (e.g. for videos or audio).
    /// </summary>
    /// <returns></returns>
    public async Task<float> GetAbsoluteEnd()
    {
      if (!Initialized)
      {
        await InitializeAsync();
      }

      return descriptor.Endabs;
    }
    
    
    
  }
}