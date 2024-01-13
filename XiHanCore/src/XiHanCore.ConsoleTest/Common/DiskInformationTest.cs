#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:DiskInformationTest
// Guid:0979736c-d1d0-4cb2-ac75-950643d97cb4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-06-30 上午 02:11:14
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHanCore.Utils.Extensions;
using XiHanCore.Utils.Files;
using XiHanCore.Utils.HardwareInfos;

namespace XiHanCore.ConsoleTest.Common;

/// <summary>
/// 磁盘信息测试
/// </summary>
public static class DiskInformationTest
{
    /// <summary>
    /// 磁盘信息测
    /// </summary>
    public static void DiskInformation()
    {
        Console.WriteLine(
            $@"【C盘】磁盘大小：{FormatExtension.FormatFileSizeToString(DiskHelper.GetHardDiskTotalSpace(@"C:\"))}；");
        Console.WriteLine(
            $@"【C盘】磁盘空余大小：{FormatExtension.FormatFileSizeToString(DiskHelper.GetHardDiskFreeSpace(@"C:\"))}；");
        Console.WriteLine($@"【C盘】磁盘空闲占比：{DiskHelper.ProportionOfHardDiskFreeSpace(@"C:\")}；");
        Console.WriteLine(
            $@"【D:\DataMine\Repository】目录大小：{FormatExtension.FormatFileSizeToString(FileHelper.GetDirectorySize(@"D:\DataMine\Repository"))}；");
        Console.WriteLine(
            $@"【D:\DataMine\Repository\XiHanCore.Framework\README.md】文件大小：{FormatExtension.FormatFileSizeToString(FileHelper.GetFileSize(@"D:\DataMine\Repository\XiHanCore.Framework\README.md"))}；");

        IEnumerable<string> directories = FileHelper.GetDirectories(@"D:\");
        Console.WriteLine($@"【D:\】目录：");
        foreach (var directory in directories) Console.WriteLine(directory);
    }
}