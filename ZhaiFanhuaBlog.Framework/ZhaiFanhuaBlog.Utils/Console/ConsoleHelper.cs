// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ConsoleHelper
// Guid:824ca05d-f5be-49a9-96f9-8a6502e5b064
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-30 上午 12:12:17
// ----------------------------------------------------------------

namespace ZhaiFanhuaBlog.Utils.Console;

/// <summary>
/// 控制台输出帮助类
/// </summary>
public static class ConsoleHelper
{
    private static readonly object _ObjLock = new();

    /// <summary>
    /// 在控制台输出
    /// </summary>
    /// <param name="inputStr">打印文本</param>
    /// <param name="frontColor">前置颜色</param>
    public static void WriteColorLine(string inputStr, ConsoleColor frontColor)
    {
        lock (_ObjLock)
        {
            ConsoleColor currentForeColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = frontColor;
            System.Console.WriteLine(inputStr);
            System.Console.ForegroundColor = currentForeColor;
        }
    }

    /// <summary>
    /// 错误信息
    /// </summary>
    /// <param name="inputStr"></param>
    /// <param name="frontColor"></param>
    public static void WriteLineError(this string inputStr, ConsoleColor frontColor = ConsoleColor.Red) => WriteColorLine(inputStr, frontColor);

    /// <summary>
    /// 警告信息
    /// </summary>
    /// <param name="inputStr"></param>
    /// <param name="frontColor"></param>
    public static void WriteLineWarning(this string inputStr, ConsoleColor frontColor = ConsoleColor.Yellow) => WriteColorLine(inputStr, frontColor);

    /// <summary>
    /// 处理信息
    /// </summary>
    /// <param name="inputStr"></param>
    /// <param name="frontColor"></param>
    public static void WriteLineHandle(this string inputStr, ConsoleColor frontColor = ConsoleColor.Cyan) => WriteColorLine(inputStr, frontColor);

    /// <summary>
    /// 正常信息
    /// </summary>
    /// <param name="inputStr"></param>
    /// <param name="frontColor"></param>
    public static void WriteLineInfo(this string inputStr, ConsoleColor frontColor = ConsoleColor.White) => WriteColorLine(inputStr, frontColor);

    /// <summary>
    /// 成功信息
    /// </summary>
    /// <param name="inputStr"></param>
    /// <param name="frontColor"></param>
    public static void WriteLineSuccess(this string inputStr, ConsoleColor frontColor = ConsoleColor.Green) => WriteColorLine(inputStr, frontColor);
}