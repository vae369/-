
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mir.Core.utils
{
    public class U_DateTimeFormat : IsoDateTimeConverter
    {
        public U_DateTimeFormat()
        {
            base.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        }

    }

    public static class DateTimeHelper
    {
        /// <summary>
        /// 将x时x分x秒的字符串列表转换为秒
        /// </summary>
        public static int ConvertDateTimeStringList(string timeList)
        {
            int hours = 0, minutes = 0, seconds = 0;
            string res = "";

            if (!string.IsNullOrWhiteSpace(timeList))
            {
                timeList = timeList.Replace("小", "");
                hours = Convert.ToInt32(timeList.Split('时')[0]);
                res = timeList.Split('时')[1];
                minutes = Convert.ToInt32(res.Split('分')[0]);
                res = res.Split('分')[1];
                seconds = Convert.ToInt32(res.Replace("秒", ""));
            }

            seconds += minutes * 60 + (hours * 60 * 60);
            return seconds;
        }
        /// <summary>
        /// 获取本周周一
        /// </summary> 
        public static DateTime getMonday(string parms)
        {
            DateTime now = DateTime.Parse(parms);
            DateTime temp = new DateTime(now.Year, now.Month, now.Day);
            int count = now.DayOfWeek - DayOfWeek.Monday;
            if (count == -1) count = 6;
            return temp.AddDays(-count);
        }

        /// <summary>
        /// 获取本周周天
        /// </summary> 
        public static DateTime getSunday(string parms)
        {
            DateTime now = DateTime.Parse(parms);
            DateTime temp = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
            int count = now.DayOfWeek - DayOfWeek.Sunday;
            if (count != 0) count = 7 - count;
            return temp.AddDays(count);
        }

        /// <summary>  
        /// 获取时间戳  13位
        /// </summary>  
        /// <returns></returns>  
        public static long GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds * 1000);
        }

        /// <summary>  
        /// 获取时间戳  10位
        /// </summary>  
        /// <returns></returns>  
        public static long GetTimeStamp(DateTime time)
        {
            DateTime starTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (int)(time - starTime).TotalSeconds;

        }


        /// 将时间戳转换为日期类型，并格式化
        /// </summary>
        /// <param name="longDateTime"></param>
        /// <returns></returns>
        public static DateTime LongDateTimeToDateTimeString(long TimeStamp, bool AccurateToMilliseconds = false)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            if (AccurateToMilliseconds)
            {
                return startTime.AddTicks(TimeStamp * 10000);
            }
            else
            {
                return startTime.AddTicks(TimeStamp * 10000000);
            }
        }



        /// <summary>
        /// 日期加减年份，针对月底特殊处理
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime AddYearsExt(this DateTime dateTime, int value)
        {
            var next = dateTime.AddYears(value);
            //如果当前日期是月底，加减N年后，目标日期依然设为最后一天
            if (dateTime.Day == DateTime.DaysInMonth(dateTime.Year, dateTime.Month))
            {
                next = new DateTime(next.Year, next.Month, DateTime.DaysInMonth(next.Year, next.Month));
            }
            return next;
        }
        /// <summary>
        /// 日期加减月份 ，针对月底特殊处理
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime AddMonthsExt(this DateTime dateTime, int value, int hours)
        {
            var next = dateTime.AddMonths(value);
            //如果当前日期是月底，加减N年后，目标日期依然设为最后一天
            if (dateTime.Day == DateTime.DaysInMonth(dateTime.Year, dateTime.Month))
            {
                next = new DateTime(next.Year, next.Month, DateTime.DaysInMonth(next.Year, next.Month), hours, 0, 0);
            }
            return next;
        }
    }

}
