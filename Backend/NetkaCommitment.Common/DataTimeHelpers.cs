using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace NetkaCommitment.Common
{
    public static class DataTimeHelpers
    {
        public static DateTime? ConvertToDateTime(string dateTime)
        {
            try
            {
                return DateTime.ParseExact(dateTime, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DateTime? ConvertToDateTime(string dateTime, string format)
        {
            try
            {
                return DateTime.ParseExact(dateTime, String.IsNullOrEmpty(format) ? "dd/MM/yyyy" : format, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DateTime? ConvertToDateTime(string dateTime, string format, DateTime? defaultValue = null)
        {
            try
            {
                return DateTime.ParseExact(dateTime, String.IsNullOrEmpty(format) ? "dd/MM/yyyy" : format, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public static string ConvertToDuration(TimeSpan ts)
        {
            List<string> lsTemp = new List<string>();
            lsTemp.Add(ts.Hours == 0 ? null : string.Format("{0} Hrs", ts.Hours));
            lsTemp.Add(ts.Minutes == 0 ? null : string.Format("{0} Mins", ts.Minutes));
            lsTemp = lsTemp.Where(t => !string.IsNullOrEmpty(t)).ToList();
            return string.Join(" ", lsTemp.ToArray()); // Example "4 Hours 30 Minutes"
        }

    }
}
