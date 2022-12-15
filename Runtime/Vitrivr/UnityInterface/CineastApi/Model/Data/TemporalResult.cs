using System.Collections.Generic;

namespace Vitrivr.UnityInterface.CineastApi.Model.Data
{
  public record TemporalResult(ObjectData MediaObject, List<SegmentData> Segments, double Score);
}

// Workaround while Unity does not use .NET 5
namespace System.Runtime.CompilerServices
{
  internal static class IsExternalInit
  {
  }
}