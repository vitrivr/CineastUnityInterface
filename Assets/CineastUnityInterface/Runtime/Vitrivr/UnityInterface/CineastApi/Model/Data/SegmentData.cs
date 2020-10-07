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
    /// Segment ID uniquely identifying the corresponding media segment.
    /// </summary>
    private readonly string id;

    /// <summary>
    /// Object ID uniquely identifying the media object this media segment originates from.
    /// </summary>
    private string objectId;

    /// <summary>
    /// The frame within the media object this media segment starts from.
    /// </summary>
    private int start;

    /// <summary>
    /// The frame within the media object this media segment ends.
    /// </summary>
    private int end;

    /// <summary>
    /// This media segments zero-based sequence number in relation to the other media segments of the same media object.
    /// </summary>
    private int number;

    /// <summary>
    /// The time in seconds within the media object this media segment starts from.
    /// </summary>
    private float startabs;

    /// <summary>
    /// The time in seconds within the media object this media segment ends.
    /// </summary>
    private float endabs;
    // TODO: What is the "exists" flag actually used for or rather what is its purpose?

    private bool initialized;

    // TODO: Consider combining lazy loading requests into batch requests every x seconds to reduce request overhead
    private static readonly SemaphoreSlim InitLock = new SemaphoreSlim(1, 1);


    public SegmentData(string id)
    {
      this.id = id;
    }

    public SegmentData(string id, string objectId, int start, int end, int number, float startabs, float endabs)
    {
      this.id = id;
      this.objectId = objectId;
      this.start = start;
      this.end = end;
      this.number = number;
      this.startabs = startabs;
      this.endabs = endabs;

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

      objectId = data.ObjectId;
      start = data.Start;
      end = data.End;
      number = data.SequenceNumber;
      startabs = data.Startabs;
      endabs = data.Endabs;

      initialized = true;
    }

    public string GetId()
    {
      return id;
    }

    public bool IsInitialized()
    {
      return initialized;
    }

    public async Task<string> GetObjectId()
    {
      if (!initialized)
      {
        await InitializeAsync();
      }

      return objectId;
    }

    public async Task<int> GetStart()
    {
      if (!initialized)
      {
        await InitializeAsync();
      }

      return start;
    }

    public async Task<int> GetEnd()
    {
      if (!initialized)
      {
        await InitializeAsync();
      }

      return end;
    }

    public async Task<int> GetSequenceNumber()
    {
      if (!initialized)
      {
        await InitializeAsync();
      }

      return number;
    }

    public async Task<float> GetAbsoluteStart()
    {
      if (!initialized)
      {
        await InitializeAsync();
      }

      return startabs;
    }

    public async Task<float> GetAbsoluteEnd()
    {
      if (!initialized)
      {
        await InitializeAsync();
      }

      return endabs;
    }
  }
}