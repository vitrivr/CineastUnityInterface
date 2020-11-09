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
    /// The domain name for temporal meta data.
    /// Given by cineast
    /// </summary>
    public const string TEMPORAL_DOMAIN = "TIME";

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

      return store.Exists(SPATIAL_DOMAIN, SPATIAL_LATITUDE)
        ? double.Parse(store.Get(SPATIAL_DOMAIN, SPATIAL_LATITUDE))
        : double.NaN;
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

      return store.Exists(SPATIAL_DOMAIN, SPATIAL_LONGITUDE)
        ? double.Parse(store.Get(SPATIAL_DOMAIN, SPATIAL_LONGITUDE))
        : double.NaN;
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
      return store.Exists(SPATIAL_DOMAIN, SPATIAL_BEARING)
        ? double.Parse(store.Get(SPATIAL_DOMAIN, SPATIAL_BEARING)) : double.NaN;
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

      return store.Exists(TEMPORAL_DOMAIN, TEMPORAL_DATETIME) ? store.Get(TEMPORAL_DOMAIN, TEMPORAL_DATETIME) : "";
    }


  }
}