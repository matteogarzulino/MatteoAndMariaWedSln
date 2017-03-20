using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingServices.Utilities
{
    public static class DateTimeUtilities
    {
        public static DateTime MaxDateTime()
        {
            return new DateTime(9999, 12, 31, 23, 59, 59, 0);
        }

        public static string ToItalianWithDayNameString(this DateTime data)
        {
            return data.ToString("D", CultureInfo.CreateSpecificCulture("it-IT"));
        }
    }
}
