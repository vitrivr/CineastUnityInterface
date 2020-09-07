using System;
using System.Collections.Generic;
using Org.Vitrivr.CineastApi.Model;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Model.Retrieval
{
  /// <summary>
  /// Wrapper for <see cref="MediaSegmentDescriptor"/>
  /// </summary>
  [Serializable]
  public class MultimediaSegment : MediaSegmentDescriptor, IEquatable<MultimediaSegment>
  {
    // DEV NOTE: Extend upon need
    private static readonly IdEqualityComparer _comparer = new IdEqualityComparer();

    public string Name { set; get; }
    public string Path { set; get; }

    public bool Equals(MultimediaSegment other)
    {
      return _comparer.Equals(this, other);
    }

    /// <summary>
    /// Internal comparer for segments by segment id
    /// </summary>
    public sealed class IdEqualityComparer : IEqualityComparer<MultimediaSegment>
    {
      public bool Equals(MultimediaSegment x, MultimediaSegment y)
      {
        if (ReferenceEquals(x, y))
        {
          return true;
        }

        if (ReferenceEquals(x, null))
        {
          return false;
        }

        if (ReferenceEquals(y, null))
        {
          return false;
        }

        if (x.GetType() != y.GetType())
        {
          return false;
        }

        return string.Equals(x.SegmentId, y.SegmentId);
      }
      
      public int GetHashCode(MultimediaSegment obj)
      {
        return obj.SegmentId != null ? obj.SegmentId.GetHashCode() : 0;
      }
    }
  }
}