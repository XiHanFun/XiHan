// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:TestChinaDate
// Guid:59adf8de-21a2-455a-9f25-c865fceb8231
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-30 上午 02:14:37
// ----------------------------------------------------------------

using System.Text;
using ZhaiFanhuaBlog.Utils.Date;
using ZhaiFanhuaBlog.Utils.Formats;

namespace ZhaiFanhuaBlog.Test.Common;

/// <summary>
/// 测试农历
/// </summary>
[TestClass]
public static class TestChinaDate
{
    /// <summary>
    /// 农历
    /// </summary>
    [TestMethod]
    public static void ChinaDate()
    {
        //DateTime dateTime1 = DateTime.Now.AddDays(-1000).AddHours(-5).AddMilliseconds(-15);
        //while (true)
        //{
        //    Random random = new Random();
        //    Task.Delay(random.Next(1, 3000)).Wait();
        //    DateTime dateTime2 = DateTime.Now;
        //    Console.WriteLine(TimeFormatHelper.FormatDateTimeToString(dateTime1, dateTime2));
        //}

        ChineseCalendar c = new ChineseCalendar(new DateTime(2022, 10, 1));
        StringBuilder dayInfo = new StringBuilder();
        dayInfo.Append("阳历：" + c.DateString + "\r\n");
        dayInfo.Append("农历：" + c.ChineseDateString + "\r\n");
        dayInfo.Append("星期：" + c.WeekDayStr + "\r\n");
        dayInfo.Append("时辰：" + c.ChineseHour + "\r\n");
        dayInfo.Append("属相：" + c.AnimalString + "\r\n");
        dayInfo.Append("节气：" + c.ChineseTwentyFourDay + "\r\n");
        dayInfo.Append("前一个节气：" + c.ChineseTwentyFourPrevDay + "\r\n");
        dayInfo.Append("下一个节气：" + c.ChineseTwentyFourNextDay + "\r\n");
        dayInfo.Append("阳历节日：" + c.DateHoliday + "\r\n");
        dayInfo.Append("农历节日：" + c.newCalendarHoliday + "\r\n");
        dayInfo.Append("按周计算节日：" + c.WeekDayHoliday + "\r\n");
        dayInfo.Append("干支：" + c.GanZhiDateString + "\r\n");
        dayInfo.Append("星宿：" + c.ChineseConstellation + "\r\n");
        dayInfo.Append("星座：" + c.Constellation + "\r\n");
        Console.WriteLine(dayInfo.ToString());
    }
}