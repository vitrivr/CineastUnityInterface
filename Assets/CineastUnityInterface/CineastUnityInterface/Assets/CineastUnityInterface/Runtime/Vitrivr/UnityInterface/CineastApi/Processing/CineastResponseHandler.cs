namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Processing {
  
  /// <summary>
  /// Response handler for cineast quieries
  /// </summary>
  /// <typeparam name="T">The type of the response</typeparam>
  public interface CineastResponseHandler<T> {

    /// <summary>
    /// Callback upon success of the cineast query.
    /// </summary>
    /// <param name="result">The result of the query</param>
    void onSuccess(T result);

    
    void onFailure(string reason);
  }
}