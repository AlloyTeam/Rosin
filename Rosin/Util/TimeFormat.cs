using System;
using System.Collections.Generic;
using System.Text;

namespace Rosin.Util
{
    public class TimeFormat
    {
        static public string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        static public DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return dtStart.AddMilliseconds(Convert.ToDouble(timeStamp));
        }
    }
}
