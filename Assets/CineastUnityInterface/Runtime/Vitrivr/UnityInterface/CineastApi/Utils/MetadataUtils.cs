using System;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Model.Data;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Utils
{
  /// <summary>
  /// Utilities for metadata, including helper methods for "built-it" metadata
  /// </summary>
  public static class MetadataUtils
  {

    /// <summary>
    /// The domain name for spatial meta data
    /// Given by cineast
    /// </summary>
    public const string SPATIAL_DOMAIN = "LOCATION";
    /// <summary>
    /// The damain name for temporal meta data.
    /// Given by cineast
    /// </summary>
    public const string TEMPORAL_DOMAIIN = "TIME";

    /// <summary>
    /// Metadata key for latitude, in the spatial domain
    /// </summary>
    public const string SPATIAL_LATITUDE = "latitude";
    /// <summary>
    /// Metadata key for longitude, in the spatial domain
    /// </summary>
    public const string SPATIAL_LONGITUDE = "longitude";
    /// <summary>
    /// Metadata key for bearing, in the spatial domain
    /// </summary>
    public const string SPATIAL_BEARING = "bearing";

    /// <summary>
    /// Metadata key for datetime, in the temporal domain
    /// </summary>
    public const string TEMPORAL_DATETIME = "datetime";

    /// <summary>
    /// Quick-access to latitude
    /// </summary>
    /// <param name="store"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">If the store was not initialised previously</exception>
    public static double GetLatitude(MetadataStore store)
    {
      if (!store.Initialized)
      {
        throw new ArgumentException("MetadataStore has to be initialised!");
      }
      return double.Parse(store.Get(SPATIAL_DOMAIN, SPATIAL_LATITUDE));
    }

    /// <summary>
    /// Quick-access to longitude
    /// </summary>
    /// <param name="store"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">If the store was not initialised previously</exception>
    public static double GetLongitude(MetadataStore store)
    {
      if (!store.Initialized)
      {
        throw new ArgumentException("MetadataStore has to be initialised!");
      }
      return double.Parse(store.Get(SPATIAL_DOMAIN, SPATIAL_LONGITUDE));
    }

    /// <summary>
    /// Quick-access to bearing
    /// </summary>
    /// <param name="store"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">If the store was not initialised previously</exception>
    public static double GetBearing(MetadataStore store)
    {
      if (!store.Initialized)
      {
        throw new ArgumentException("MetadataStore has to be initialised!");
      }
      return double.Parse(store.Get(SPATIAL_DOMAIN, SPATIAL_BEARING));
    }

    /// <summary>
    /// Quick-access to datetime
    /// </summary>
    /// <param name="store"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">If the store was not initialised previously</exception>
    public static string GetDateTime(MetadataStore store)
    {
      if (!store.Initialized)
      {
        throw new ArgumentException("MetadataStore has to be initialised!");
      }
      return store.Get(TEMPORAL_DOMAIIN, TEMPORAL_DATETIME);
    }


  }
}