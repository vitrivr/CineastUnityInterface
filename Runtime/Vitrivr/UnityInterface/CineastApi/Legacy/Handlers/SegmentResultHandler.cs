using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Legacy.Models.Messages.Result;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Legacy.Handlers
{
    public class SegmentResultHandler: AbstractMessageHandler<SegmentResult>
    {
        public SegmentResultHandler(LegacyCineastApi api, string guid) : base(api, guid)
        {
        }

        public override void OnSuccess(SegmentResult data)
        {
                        
        }
    }
}