using System.Linq;
using System.Threading.Tasks;
using Org.Vitrivr.CineastApi.Model;
using Vitrivr.UnityInterface.CineastApi.Model.Registries;

namespace Vitrivr.UnityInterface.CineastApi.Model.Data
{
  public class TemporalQueryResponse
  {
    public readonly TemporalQuery Query;
    public readonly TemporalQueryResult Results;

    public TemporalQueryResponse(TemporalQuery query, TemporalQueryResult results)
    {
      Query = query;
      Results = results;
    }

    public async Task Prefetch(int number)
    {
      var segmentSet = Results.Content.Take(number)
        .SelectMany(result => result.Segments
          .Select(SegmentRegistry.GetSegment)
        ).ToHashSet();

      await SegmentRegistry.BatchFetchSegmentData(segmentSet.ToList());
    }
  }
}