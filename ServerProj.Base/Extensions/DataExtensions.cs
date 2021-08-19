using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerProj.Base.Extensions
{
    public static class DateExtensions
    {
        private readonly static DateTime _dt1970 = new DateTime(1970, 1, 1);
        /// <summary>
        /// 时间转 yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToChString(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 时间转毫秒时间戳
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static long ToTimeStamp(this DateTime date)
        {
            TimeSpan ts = date.ToLocalTime().Subtract(_dt1970.ToLocalTime());
            return Convert.ToInt64(ts.TotalMilliseconds);
        }
        /// <summary>
        /// 时间转秒时间戳
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int ToTimeStampSecond(this DateTime date)
        {
            TimeSpan ts = date.ToLocalTime().Subtract(_dt1970.ToLocalTime());
            return Convert.ToInt32(ts.TotalSeconds);
        }
        /// <summary>
        /// 毫秒时间戳转时间
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this long ts)
        {
            DateTime date = _dt1970.ToLocalTime();
            if (ts <= 0) return date;
            return date.AddMilliseconds(ts);
        }

        /// <summary>
        /// 秒时间戳转时间
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this int ts)
        {
            DateTime date = _dt1970.ToLocalTime();
            if (ts <= 0) return date;
            return date.AddSeconds(ts);
        }
    }
}
