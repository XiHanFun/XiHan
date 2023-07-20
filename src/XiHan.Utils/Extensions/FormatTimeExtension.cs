#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:FormatTimeExtensions
// Guid:4598f6e0-78b7-46d3-9eb5-834f6699d7c6
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 03:36:28
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Utils.Extensions;

/// <summary>
/// 时间格式化拓展类
/// </summary>
public static class FormatTimeExtension
{
    /// <summary>
    /// 获取当前时间的时间戳
    /// </summary>
    /// <param name="thisValue"></param>
    /// <returns></returns>
    public static string GetDateToTimeStamp(this DateTime thisValue)
    {
        var ts = thisValue - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalSeconds).ToString();
    }

    /// <summary>
    /// 获取日期天的最小时间
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static DateTime GetDayMinDate(this DateTime dateTime)
    {
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
    }

    /// <summary>
    /// 获取日期天的最大时间
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static DateTime GetDayMaxDate(this DateTime dateTime)
    {
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
    }

    /// <summary>
    /// 获取日期开始时间
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="days"></param>
    /// <returns></returns>
    public static DateTime GetBeginTime(this DateTime? dateTime, int days = 0)
    {
        if (dateTime == DateTime.MinValue || dateTime == null)
        {
            return DateTime.Now.AddDays(days);
        }

        return (DateTime)dateTime;
    }

    /// <summary>
    /// 时间转换字符串
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static string FormatDateTimeToString(this DateTime? dateTime)
    {
        return dateTime != null ? dateTime.Value.ToString(dateTime.Value.Year == DateTime.Now.Year ? "MM-dd HH:mm" : "yyyy-MM-dd HH:mm") : string.Empty;
    }

    /// <summary>
    /// 时间转换字符串
    /// </summary>
    /// <param name="dateTimeBefore"></param>
    /// <param name="dateTimeAfter"></param>
    /// <returns></returns>
    public static string FormatDateTimeToString(this DateTime dateTimeBefore, DateTime dateTimeAfter)
    {
        if (dateTimeBefore >= dateTimeAfter) throw new Exception("开始日期必须小于结束日期");
        var timeSpan = dateTimeAfter - dateTimeBefore;
        return timeSpan.FormatTimeSpanToString();
    }

    /// <summary>
    /// 毫秒转换字符串
    /// </summary>
    /// <param name="milliseconds"></param>
    /// <returns></returns>
    public static string FormatMilliSecondsToString(this long milliseconds)
    {
        var timeSpan = TimeSpan.FromMilliseconds(milliseconds);
        return timeSpan.FormatTimeSpanToString();
    }

    /// <summary>
    /// 时刻转换字符串
    /// </summary>
    /// <param name="ticks"></param>
    /// <returns></returns>
    public static string FormatTimeTicksToString(this long ticks)
    {
        var timeSpan = TimeSpan.FromTicks(ticks);
        return timeSpan.FormatTimeSpanToString();
    }

    /// <summary>
    /// 毫秒转换字符串
    /// </summary>
    /// <param name="ms"></param>
    /// <returns></returns>
    public static string FormatTimeMilliSecondToString(this long ms)
    {
        const int ss = 1000;
        const int mi = ss * 60;
        const int hh = mi * 60;
        const int dd = hh * 24;

        var day = ms / dd;
        var hour = (ms - day * dd) / hh;
        var minute = (ms - day * dd - hour * hh) / mi;
        var second = (ms - day * dd - hour * hh - minute * mi) / ss;
        var milliSecond = ms - day * dd - hour * hh - minute * mi - second * ss;

        // 天
        var sDay = day < 10 ? "0" + day : "" + day;
        // 小时
        var sHour = hour < 10 ? "0" + hour : "" + hour;
        // 分钟
        var sMinute = minute < 10 ? "0" + minute : "" + minute;
        // 秒
        var sSecond = second < 10 ? "0" + second : "" + second;
        // 毫秒
        var sMilliSecond = milliSecond < 10 ? "0" + milliSecond : "" + milliSecond;
        sMilliSecond = milliSecond < 100 ? "0" + sMilliSecond : "" + sMilliSecond;

        return $"{sDay} 天 {sHour} 小时 {sMinute} 分 {sSecond} 秒 {sMilliSecond} 毫秒";
    }

    /// <summary>
    /// 时间跨度转换字符串
    /// </summary>
    /// <param name="timeSpan"></param>
    /// <returns></returns>
    public static string FormatTimeSpanToString(this TimeSpan timeSpan)
    {
        var day = timeSpan.Days;
        var hour = timeSpan.Hours;
        var minute = timeSpan.Minutes;
        var second = timeSpan.Seconds;
        var milliSecond = timeSpan.Milliseconds;

        // 天
        var sDay = day < 10 ? "0" + day : "" + day;
        // 小时
        var sHour = hour < 10 ? "0" + hour : "" + hour;
        // 分钟
        var sMinute = minute < 10 ? "0" + minute : "" + minute;
        // 秒
        var sSecond = second < 10 ? "0" + second : "" + second;
        // 毫秒
        var sMilliSecond = milliSecond < 10 ? "0" + milliSecond : "" + milliSecond;
        sMilliSecond = milliSecond < 100 ? "0" + sMilliSecond : "" + sMilliSecond;

        return $"{sDay} 天 {sHour} 小时 {sMinute} 分 {sSecond} 秒 {sMilliSecond} 毫秒";
    }

    /// <summary>
    /// 时间按天转换字符串
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string FormatDateTimeToEasyString(this DateTime value)
    {
        var now = DateTime.Now;
        var strDate = value.ToString("yyyy-MM-dd");
        if (now < value) return strDate;
        var dep = now - value;

        switch (dep.TotalMinutes)
        {
            case < 1:
                return "刚刚";

            case >= 1 and < 60:
                return dep.TotalMinutes.ParseToInt() + "分钟前";

            default:
                {
                    if (dep.TotalHours < 24)
                        return dep.TotalHours.ParseToInt() + "小时前";
                    else
                        switch (dep.TotalDays)
                        {
                            case < 7:
                                return dep.TotalDays.ParseToInt() + "天前";

                            case >= 7 and < 30:
                                {
                                    var defaultWeek = dep.TotalDays.ParseToInt() / 7;
                                    return defaultWeek + "周前";
                                }
                            default:
                                {
                                    if (dep.TotalDays.ParseToInt() >= 30 && dep.TotalDays.ParseToInt() < 365)
                                        return value.Month.ParseToInt() + "个月前";
                                    else return now.Year - value.Year + "年前";
                                }
                        }
                }
        }
    }

    /// <summary>
    /// 字符串转日期
    /// </summary>
    /// <param name="thisValue"></param>
    /// <returns></returns>
    public static DateTime FormatStringToDate(this string thisValue)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(thisValue)) return DateTime.MinValue;
            if (thisValue.Contains('-') || thisValue.Contains('/'))
            {
                return DateTime.Parse(thisValue);
            }
            else
            {
                var length = thisValue.Length;
                return length switch
                {
                    4 => DateTime.ParseExact(thisValue, "yyyy", System.Globalization.CultureInfo.CurrentCulture),
                    6 => DateTime.ParseExact(thisValue, "yyyyMM", System.Globalization.CultureInfo.CurrentCulture),
                    8 => DateTime.ParseExact(thisValue, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture),
                    10 => DateTime.ParseExact(thisValue, "yyyyMMddHH", System.Globalization.CultureInfo.CurrentCulture),
                    12 => DateTime.ParseExact(thisValue, "yyyyMMddHHmm", System.Globalization.CultureInfo.CurrentCulture),
                    14 => DateTime.ParseExact(thisValue, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture),
                    _ => DateTime.ParseExact(thisValue, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture)
                };
            }
        }
        catch
        {
            return DateTime.MinValue;
        }
    }
}