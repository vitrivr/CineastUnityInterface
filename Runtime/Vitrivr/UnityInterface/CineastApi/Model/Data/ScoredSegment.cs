using System;

namespace Vitrivr.UnityInterface.CineastApi.Model.Data
{
  /// <summary>
  /// Data class for segment with associated score.
  /// </summary>
  [Serializable]
  public class ScoredSegment : IComparable
  {
    public readonly SegmentData segment;
    public readonly double score;

    public ScoredSegment(SegmentData segment, double score)
    {
      this.segment = segment;
      this.score = score;
    }

    public int CompareTo(object obj)
    {
      switch (obj)
      {
        case ScoredSegment other:
          return -score.CompareTo(other.score); // Negative compare to because higher scores should come first
        default:
          throw new ArgumentException("Object is not a ScoredSegment!");
      }
    }
  }
}