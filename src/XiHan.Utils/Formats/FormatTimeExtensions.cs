#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:FormatTimeExtensions
// Guid:4598f6e0-78b7-46d3-9eb5-834f6699d7c6
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 03:36:28
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Utils.Formats;

/// <summary>
/// 时间格式化拓展类
/// </summary>
public static class FormatTimeExtensions
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
    /// 时间转换字符串
    /// </summary>
    /// <param name="dateTimeBefore"></param>
    /// <param name="dateTimeAfter"></param>
    /// <returns></returns>
    public static string FormatDateTimeToString(this DateTime dateTimeBefore, DateTime dateTimeAfter)
    {
        if (dateTimeBefore < dateTimeAfter)
        {
            var timeSpan = dateTimeAfter - dateTimeBefore;
            return FormatTimeSpanToString(timeSpan);
        }

        throw new Exception("开始日期必须小于结束日期");
    }

    /// <summary>
    /// 毫秒转换字符串
    /// </summary>
    /// <param name="milliseconds"></param>
    /// <returns></returns>
    public static string FormatMilliSecondsToString(this long milliseconds)
    {
        var timeSpan = TimeSpan.FromMilliseconds(milliseconds);
        return FormatTimeSpanToString(timeSpan);
    }

    /// <summary>
    /// 时刻转换字符串
    /// </summary>
    /// <param name="ticks"></param>
    /// <returns></returns>
    public static string FormatTimeTicksToString(this long ticks)
    {
        var timeSpan = TimeSpan.FromTicks(ticks);
        return FormatTimeSpanToString(timeSpan);
    }

    /// <summary>
    /// 时间跨度转换字符串
    /// </summary>
    /// <param name="timeSpan"></param>
    /// <returns></returns>
    public static string FormatTimeSpanToString(this TimeSpan timeSpan)
    {
        var result = string.Empty;
        if (timeSpan.Days >= 1)
        {
            result = timeSpan.Days + "天";
        }
        if (timeSpan.Hours >= 1)
        {
            result += timeSpan.Hours + "时";
        }
        if (timeSpan.Minutes >= 1)
        {
            result += timeSpan.Minutes + "分";
        }
        if (timeSpan.Seconds >= 1)
        {
            result += timeSpan.Seconds + "秒";
        }
        if (timeSpan.Milliseconds >= 1)
        {
            result += timeSpan.Milliseconds + "毫秒";
        }
        return result;
    }

    /// <summary>
    /// 时间按天转换字符串
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static string FormatDateTimeToFriendlyString(this DateTime date)
    {
        string result = string.Empty;

        var strDate = date.ToString("yyyy-MM-dd");
        if (DateTime.Now.ToString("yyyy-MM-dd") == strDate)
        {
            result = "今天";
        }
        else if (DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") == strDate)
        {
            result = "明天";
        }
        else if (DateTime.Now.AddDays(2).ToString("yyyy-MM-dd") == strDate)
        {
            result = "后天";
        }
        else if (DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") == strDate)
        {
            result = "昨天";
        }
        else if (DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd") == strDate)
        {
            result = "前天";
        }
        else
        {
            result = strDate;
        }
        return result;
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
            if (string.IsNullOrWhiteSpace(thisValue))
            {
                return DateTime.MinValue;
            }
            if (thisValue.Contains("-") || thisValue.Contains("/"))
            {
                return DateTime.Parse(thisValue);
            }
            else
            {
                int length = thisValue.Length;
                switch (length)
                {
                    case 4:
                        return DateTime.ParseExact(thisValue, "yyyy", System.Globalization.CultureInfo.CurrentCulture);

                    case 6:
                        return DateTime.ParseExact(thisValue, "yyyyMM", System.Globalization.CultureInfo.CurrentCulture);

                    case 8:
                        return DateTime.ParseExact(thisValue, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);

                    case 10:
                        return DateTime.ParseExact(thisValue, "yyyyMMddHH", System.Globalization.CultureInfo.CurrentCulture);

                    case 12:
                        return DateTime.ParseExact(thisValue, "yyyyMMddHHmm", System.Globalization.CultureInfo.CurrentCulture);

                    case 14:
                        return DateTime.ParseExact(thisValue, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);

                    default:
                        return DateTime.ParseExact(thisValue, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                }
            }
        }
        catch
        {
            return DateTime.MinValue;
        }
    }
}