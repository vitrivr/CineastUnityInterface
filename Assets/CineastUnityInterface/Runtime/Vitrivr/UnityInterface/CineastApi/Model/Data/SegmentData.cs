using System.Threading;
using System.Threading.Tasks;
using Org.Vitrivr.CineastApi.Model;
using UnityEngine;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Model.Data
{
  public class SegmentData
  {
    private readonly string id;
    private string objectId;
    private int start, end, number;

    private float startabs, endabs;
    // TODO: What is the "exists" flag actually used for or rather what is its purpose?

    private bool initialized;

    // TODO: Consider combining lazy loading requests into batch requests every x seconds to reduce request overhead
    private static SemaphoreSlim initLock = new SemaphoreSlim(1, 1);


    public SegmentData(string id)
    {
      this.id = id;
    }

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
        await InitializeData();
      }

      return objectId;
    }

    public async Task<int> GetStart()
    {
      if (!initialized)
      {
        await InitializeData();
      }

      return start;
    }

    public async Task<int> GetEnd()
    {
      if (!initialized)
      {
        await InitializeData();
      }

      return end;
    }

    public async Task<int> GetSequenceNumber()
    {
      if (!initialized)
      {
        await InitializeData();
      }

      return number;
    }

    public async Task<float> GetAbsoluteStart()
    {
      if (!initialized)
      {
        await InitializeData();
      }

      return startabs;
    }

    public async Task<float> GetAbsoluteEnd()
    {
      if (!initialized)
      {
        await InitializeData();
      }

      return endabs;
    }

    private async Task InitializeData()
    {
      await initLock.WaitAsync();
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
        initLock.Release();
      }
    }
  }
}