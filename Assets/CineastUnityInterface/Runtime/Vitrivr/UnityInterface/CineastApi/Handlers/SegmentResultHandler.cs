using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Models.Messages.Result;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Handlers
{
    public class SegmentResultHandler: AbstractMessageHandler<SegmentResult>
    {
        public SegmentResultHandler(CineastApi api, string guid) : base(api, guid)
        {
        }

        public override void OnSuccess(SegmentResult data)
        {
                        
        }
    }
}