using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Legacy.Models.Messages.Query;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Legacy.Models.Messages.Result;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Legacy.Utils;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Legacy.Handlers
{
  /// <summary>
  /// Handler for SimilarResult messages.
  /// On success and non-empty id list, issues a SegmentByIds request
  /// </summary>
  public class SimilarResultHandler : AbstractMessageHandler<SimilarResult>
  {
    public SimilarResultHandler(LegacyCineastApi api, string guid) : base(api, guid)
    {
    }

    public override void OnSuccess(SimilarResult data)
    {
      if (data.IsEmpty())
      {
        api.ReportFailure(guid, "SimilarityResult empty");
      }
      else
      {
        var ids = CineastUtils.ExtractIdArray(CineastUtils.ExtractContentObjects(data));
        var query = new IdsQuery(ids);
        api.StartCoroutine(api.RequestSegmentsAsync(query, guid));
      }
    }
  }
}