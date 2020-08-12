using System;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Model.Processing
{
  public abstract class ResponseHandler<T>
  {
    private Guid guid = new Guid();

    public Guid Guid => guid;

    public abstract void OnSuccess(T result);

    public abstract void OnFailure(string message);
  }
}