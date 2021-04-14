namespace Vitrivr.UnityInterface.CineastApi.Utils
{
  public class TimeUtils
  {
    /// <summary>
    /// Converts the given year (numerical value) to a ISO8601 conform timestamp.
    /// This conversion isn't smart and doesn't check for negative values or too large ones.
    /// </summary>
    /// <param name="year">The year as a numerical value (usually a 4 digit, positive integer)</param>
    /// <returns>A ISO8601 conform timestamp string, set to January the first at noon in this year. No timezone specified</returns>
    public static string ConvertYearToISO8601(int year)
    {
      return ConvertToISO8601(year, 1, 1, 12, 0, 0);
    }

    /// <summary>
    /// Converts the given time specification to a ISO8601 conform timestamp representation.
    /// This conversion isn't smart and doesn't perform any sanity checks (e.g. 0 &lt; minutes &lt; 59 )
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="dayOfMonth"></param>
    /// <param name="hours"></param>
    /// <param name="minutes"></param>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public static string ConvertToISO8601(int year, int month, int dayOfMonth, int hours, int minutes, int seconds)
    {
      return $"{year:D4}-{month:D2}-{dayOfMonth:D2}T{hours:D2}:{minutes:D2}:{seconds:D2}Z"; // year-month-day[THH:MM:SSZ]
    }
  }
}