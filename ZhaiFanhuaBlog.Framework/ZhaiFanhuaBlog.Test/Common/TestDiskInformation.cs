// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:TestDiskInformation
// Guid:0979736c-d1d0-4cb2-ac75-950643d97cb4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-30 上午 02:11:14
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Utils.DirFile;
using ZhaiFanhuaBlog.Utils.Formats;

namespace ZhaiFanhuaBlog.Test.Common;

/// <summary>
/// 测试磁盘信息
/// </summary>
public static class TestDiskInformation
{
    public static void DiskInformation()
    {
        Console.WriteLine($@"【C盘】磁盘大小：{FileSizeFormatHelper.ParseFileSizeBytes(DirFileHelper.GetHardDiskSpace(@"C:\"))}；");
        Console.WriteLine($@"【C盘】磁盘空余大小：{FileSizeFormatHelper.ParseFileSizeBytes(DirFileHelper.GetHardDiskFreeSpace(@"C:\"))}；");
        Console.WriteLine($@"【C盘】磁盘空闲占比：{DirFileHelper.ProportionOfHardDiskFreeSpace(@"C:\")}；");
        Console.WriteLine($@"【D:\DataMine\Repository】目录大小：{FileSizeFormatHelper.ParseFileSizeBytes(DirFileHelper.GetDirectorySize(@"D:\DataMine\Repository"))}；");
        Console.WriteLine($@"【D:\DataMine\Repository\ZhaiFanhuaBlog.Framework\README.md】文件大小：{FileSizeFormatHelper.ParseFileSizeBytes(DirFileHelper.GetFileSize(@"D:\DataMine\Repository\ZhaiFanhuaBlog.Framework\README.md"))}；");

        string[]? directories = DirFileHelper.GetDirectories(@"D:\");
        Console.WriteLine($@"【D:\】目录：");
        foreach (string directory in directories)
        {
            Console.WriteLine(directory);
        }
    }
}