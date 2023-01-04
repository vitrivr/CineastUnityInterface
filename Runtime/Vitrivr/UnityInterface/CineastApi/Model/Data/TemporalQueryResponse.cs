using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Org.Vitrivr.CineastApi.Model;
using UnityEngine;
using Vitrivr.UnityInterface.CineastApi.Model.Registries;

namespace Vitrivr.UnityInterface.CineastApi.Model.Data
{
  public class TemporalQueryResponse
  {
    public readonly TemporalQuery Query;
    public readonly TemporalQueryResult ResultsMessage;
    public readonly List<TemporalResult> Results;

    private readonly MultimediaRegistry _multimediaRegistry;

    public TemporalQueryResponse(TemporalQuery query, TemporalQueryResult resultsMessage,
      MultimediaRegistry multimediaRegistry)
    {
      Query = query;
      ResultsMessage = resultsMessage;
      _multimediaRegistry = multimediaRegistry;
      Results = ResultsMessage.Content.Select(TemporalObjectToResult).ToList();
    }

    public async Task Prefetch(int number)
    {
      // Segments
      // Convert to hash set to ensure uniqueness
      var segmentSet = ResultsMessage.Content.Take(number)
        .SelectMany(result => result.Segments
          .Select(_multimediaRegistry.GetSegment)
        ).ToHashSet();

      await _multimediaRegistry.BatchFetchSegmentData(segmentSet.ToList());

      // Objects
      var objectSet = ResultsMessage.Content.Take(number)
        .Select(result => _multimediaRegistry.GetObject(result.ObjectId)
        ).ToHashSet();

      await _multimediaRegistry.BatchFetchObjectData(objectSet.ToList());
    }

    private TemporalResult TemporalObjectToResult(TemporalObject temporalObject)
    {
      var mediaObject = _multimediaRegistry.GetObject(temporalObject.ObjectId);
      var segments = temporalObject.Segments.Select(_multimediaRegistry.GetSegment).ToList();

      return new TemporalResult(mediaObject, segments, temporalObject.Score);
    }
  }
}