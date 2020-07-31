using System;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Processing {
  
  /// <summary>
  /// Response handler for cineast quieries
  /// </summary>
  /// <typeparam name="T">The type of the response</typeparam>
  public abstract class CineastResponseHandler<T>
  {

    private Guid _guid = new Guid();

    public Guid Guid
    {
      get { return _guid; }
    }

    /// <summary>
    /// Callback upon success of the cineast query.
    /// </summary>
    /// <param name="result">The result of the query</param>
    public abstract void onSuccess(T result);

    
    public abstract void onFailure(string reason);
  }
}