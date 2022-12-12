#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:TimeFormatHelper
// Guid:4598f6e0-78b7-46d3-9eb5-834f6699d7c6
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 03:36:28
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace ZhaiFanhuaBlog.Utils.Formats;

/// <summary>
/// 时间格式化帮助类
/// </summary>
public static class TimeFormatHelper
{
    /// <summary>
    /// 时间转换字符串
    /// </summary>
    /// <param name="dateTimeBefore"></param>
    /// <param name="dateTimeAfter"></param>
    /// <returns></returns>
    public static string DateTimeToString(DateTime dateTimeBefore, DateTime dateTimeAfter)
    {
        if (dateTimeBefore < dateTimeAfter)
        {
            TimeSpan timeSpan = dateTimeAfter - dateTimeBefore;
            return TimeSpanToString(timeSpan);
        }
        else
        {
            throw new Exception("开始日期必须小于结束日期");
        }
    }

    /// <summary>
    /// 毫秒转换字符串
    /// </summary>
    /// <param name="milliseconds"></param>
    /// <returns></returns>
    public static string MillisecondsToString(long milliseconds)
    {
        TimeSpan timeSpan = TimeSpan.FromMilliseconds(milliseconds);
        return TimeSpanToString(timeSpan);
    }

    /// <summary>
    /// 时刻转换字符串
    /// </summary>
    /// <param name="ticks"></param>
    /// <returns></returns>
    public static string TimeTicksToString(long ticks)
    {
        TimeSpan timeSpan = TimeSpan.FromTicks(ticks);
        return TimeSpanToString(timeSpan);
    }

    /// <summary>
    /// 时间跨度转换字符串
    /// </summary>
    /// <param name="timeSpan"></param>
    /// <returns></returns>
    public static string TimeSpanToString(TimeSpan timeSpan)
    {
        string result = string.Empty;
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
    public static string FriendlyDate(DateTime? date)
    {
        if (!date.HasValue)
        {
            return string.Empty;
        }

        string strDate = date.Value.ToString("yyyy-MM-dd");
        string vDate = string.Empty;
        if (DateTime.Now.ToString("yyyy-MM-dd") == strDate)
        {
            vDate = "今天";
        }
        else if (DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") == strDate)
        {
            vDate = "明天";
        }
        else if (DateTime.Now.AddDays(2).ToString("yyyy-MM-dd") == strDate)
        {
            vDate = "后天";
        }
        else if (DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") == strDate)
        {
            vDate = "昨天";
        }
        else if (DateTime.Now.AddDays(2).ToString("yyyy-MM-dd") == strDate)
        {
            vDate = "前天";
        }
        else
        {
            vDate = strDate;
        }
        return vDate;
    }
}