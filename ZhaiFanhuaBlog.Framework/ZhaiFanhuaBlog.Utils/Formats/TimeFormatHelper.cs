// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:TimeFormatHelper
// Guid:4598f6e0-78b7-46d3-9eb5-834f6699d7c6
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 03:36:28
// ----------------------------------------------------------------

namespace ZhaiFanhuaBlog.Utils.Formats;

/// <summary>
/// 时间格式化帮助类
/// </summary>
public static class TimeFormatHelper
{
    /// <summary>
    /// 时间转换
    /// </summary>
    /// <param name="dateTimeBefore"></param>
    /// <param name="dateTimeAfter"></param>
    /// <returns></returns>
    public static string FormatDateTimeToString(DateTime dateTimeBefore, DateTime dateTimeAfter)
    {
        long ticks = (dateTimeAfter.Ticks - dateTimeBefore.Ticks) / 10000;
        return FormatTimeTicksToString(ticks);
    }

    /// <summary>
    /// 时刻转换
    /// </summary>
    /// <param name="ticks"></param>
    /// <returns></returns>
    public static string FormatTimeTicksToString(long ticks)
    {
        TimeSpan timeSpan = TimeSpan.FromMilliseconds(ticks);
        string result = string.Empty;
        if (timeSpan.Days > 0)
            result = timeSpan.Days + "天";
        if (timeSpan.Hours > 0)
            result += timeSpan.Hours + "时";
        if (timeSpan.Minutes > 0)
            result += timeSpan.Minutes + "分";
        if (timeSpan.Seconds > 0)
            result += timeSpan.Seconds + "秒";
        if (timeSpan.Milliseconds > 0)
            result += timeSpan.Milliseconds + "毫秒";
        return result;
    }
}