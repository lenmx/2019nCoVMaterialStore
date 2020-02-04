using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nCoVMSApi.Common
{
    public class TimeHelper
    {
        /// <summary>
        /// 数据库中的字段如果不能为空，用户录入时又没有填写时，采用此值表示为null
        /// </summary>
        public static DateTime DbNullDatetime = new DateTime(1800, 1, 1);

        /// <summary>
        /// 无效的时间年份
        /// </summary>
        public static readonly int NullDatetimeYear = 1800;

        /// <summary>
        /// 最小年份
        /// </summary>
        public static readonly int DefaultMinYear = NullDatetimeYear + 1;

        /// <summary>
        /// 最大年份
        /// </summary>
        public static readonly int DefaultMaxYear = 2099;

        public static bool IsNullDate(DateTime date)
        {
            return date.Year <= NullDatetimeYear;
        }

        public static string FormatDate(DateTime? date)
        {
            if (date != null && !IsNullDate((DateTime)date))
                return ((DateTime)date).ToString("yyyy-MM-dd");
            else
                return String.Empty;
        }

        /// <summary>
        /// 格式化时间 例如：2012-01-01 22:12:00
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string FormatDateTime(DateTime? date)
        {
            if (date != null && !IsNullDate((DateTime)date))
                return ((DateTime)date).ToString("yyyy-MM-dd HH:mm:ss");
            else
                return String.Empty;
        }

        /// <summary>
        /// 格式化时间到分钟精度
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string FormatDateToMinutes(DateTime? date)
        {
            if (date == null)
                return String.Empty;

            DateTime datetime = (DateTime)date;
            if (IsNullDate(datetime))
                return String.Empty;
            else
                return datetime.ToString("yyyy-MM-dd HH:mm");
        }

        /// <summary>
        /// 尝试将文本解析成时间时间，如果不成功则用NullDateTime代替
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DateTime TryParse(string source) =>
            DateTime.TryParse(source, out DateTime date) ? date : DbNullDatetime;

        /// <summary>
        /// 确保年份有效（如果超出系统允许的时间范围，采用边界值）
        /// </summary>
        /// <param name="year"></param>
        /// <param name="minYear"></param>
        /// <param name="maxYear"></param>
        /// <returns></returns>
        public static int EnsureYear(int year, int minYear = 0, int maxYear = 0)
        {
            if (minYear > 0 || maxYear > 0)
                return EnsureYear(Math.Max(minYear, Math.Min(maxYear, year)));
            else
                return Math.Max(DefaultMinYear, Math.Min(DefaultMaxYear, year));
        }

        /// <summary>
        /// 将月份数转换成季度数
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static int MonthToSeason(int month) =>
            month >= 1 && month <= 12 ? 1 + ((month - 1) / 3) : 1;

        /// <summary>
        /// 确保月份在1-12之间
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static int EnsureMonth(int month)
        {
            return Math.Max(1, Math.Min(12, month));
        }

        #region 月份的第一天或最后天，以及日期偏移计算
        /// <summary>
        /// 计算指定月份的第一天
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static DateTime MonthFirstDate(int year, int month)
        {
            return new DateTime(year, month, 1);
        }

        /// <summary>
        /// 计算指定的日期所在月份的第一天
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime MonthFirstDate(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// 计算指定的月份的最后一天的日期
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static DateTime MonthLastDate(int year, int month)
        {
            return NextMonthFirstDay(year, month).AddDays(-1);
        }

        /// <summary>
        /// 计算指定的日期所在月份的最后一天的日期
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime MonthLastDate(DateTime date)
        {
            return NextMonthFirstDay(date).AddDays(-1);
        }

        /// <summary>
        /// 计算指定月份的下一月份的第一天
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static DateTime NextMonthFirstDay(int year, int month)
        {
            var nextMonthDay = MonthFirstDate(year, month).AddDays(35); //下个月初的某天
            return new DateTime(nextMonthDay.Year, nextMonthDay.Month, 1);
        }

        /// <summary>
        /// 计算指定日期所在月份的下一月份的第一天
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime NextMonthFirstDay(DateTime date)
        {
            var nextMonthDay = new DateTime(date.Year, date.Month, 1).AddDays(35); //下个月初的某天
            return new DateTime(nextMonthDay.Year, nextMonthDay.Month, 1);
        }
        #endregion


        /// <summary>
        /// 日期是否是数据库空日期
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static bool IsDbNull(DateTime? datetime) =>
            datetime == null ? true : ((DateTime)datetime) < DbNullDatetime;

        /// <summary>
        /// 解析日期，如果日期无效，则返回DbNullDate
        /// </summary>
        /// <param name="datetimeText"></param>
        /// <returns></returns>
        public static DateTime Parse(string datetimeText) =>
            DateTime.TryParse(datetimeText, out DateTime date) ? date : DbNullDatetime;

        #region 获取 本周、本月、本季度、本年 的开始时间或结束时间
        /// <summary>
        /// 获取结束时间
        /// </summary>
        /// <param name="TimeType">week、month、season、year</param>
        /// <param name="now"></param>
        /// <returns></returns>
        public static DateTime? GetTimeStartByType(string TimeType, DateTime now)
        {
            switch (TimeType)
            {
                case "week":
                    return now.AddDays(-(int)now.DayOfWeek + 1);
                case "month":
                    return now.AddDays(-now.Day + 1);
                case "season":
                    var time = now.AddMonths(0 - ((now.Month - 1) % 3));
                    return time.AddDays(-time.Day + 1);
                case "year":
                    return now.AddDays(-now.DayOfYear + 1);
                default:
                    return null;
            }
        }

        /// <summary>
        /// 获取结束时间
        /// </summary>
        /// <param name="TimeType">week、month、season、year</param>
        /// <param name="now"></param>
        /// <returns></returns>
        public static DateTime? GetTimeEndByType(string TimeType, DateTime now)
        {
            switch (TimeType)
            {
                case "week":
                    return now.AddDays(7 - (int)now.DayOfWeek);
                case "month":
                    return now.AddMonths(1).AddDays(-now.AddMonths(1).Day + 1).AddDays(-1);
                case "season":
                    var time = now.AddMonths((3 - ((now.Month - 1) % 3) - 1));
                    return time.AddMonths(1).AddDays(-time.AddMonths(1).Day + 1).AddDays(-1);
                case "year":
                    var time2 = now.AddYears(1);
                    return time2.AddDays(-time2.DayOfYear);
                default:
                    return null;
            }
        }
        #endregion


        #region timestamp 和 DateTime 互转

        /// <summary>
        /// 将datetime 转为 UTC timestamp
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long GetTimeToTimestamp(DateTime time)
        {
            var start = new DateTime(1970, 1, 1);
            return (long)(time.ToUniversalTime() - start).TotalSeconds;
        }

        public static long GetTimeToTimestampMilliseconds(DateTime time)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return (long)(time.ToUniversalTime() - start).TotalMilliseconds;
        }

        /// <summary>
        /// 将timestamp 转化为 datetime
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="toLocalTime">如果timestamp 是UTC 时间</param>
        /// <returns></returns>
        public static DateTime? GetTimestampToTime(string timestamp, bool isUTC = true)
        {
            if (string.IsNullOrEmpty(timestamp))
                return null;

            timestamp = timestamp.PadRight(13, '0'); //转为毫秒级的时间戳
            var start = new DateTime(1970, 1, 1);
            var result = start.AddMilliseconds(long.Parse(timestamp));
            return isUTC ? result.AddHours(8) : result;
        }

        public static string GetTimeToTimestampMilliseconds()
        {
            var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }


        #endregion
    }
}
