#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ZipTest
// Guid:aaac17c3-43c3-4438-a1d4-9e4c2cfcce6d
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-21 上午 10:02:42
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Utils.Files;

namespace XiHan.ConsoleTest.Common;

/// <summary>
/// ZipTest
/// </summary>
public static class ZipTest
{
    public static void Extract()
    {
        var archivePath = "D:\\新建文件夹1.zip";
        var extractPath = "D:\\新建文件夹1";

        try
        {
            // Extract the contents of the archive to the specified directory
            ZipHelper.Extract(archivePath, extractPath);
            Console.WriteLine("提取成功.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    public static void Compress()
    {
        var sourceDirectory = "D:\\新建文件夹2";

        try
        {
            // Compress the specified directory into an archive
            var compressedArchivePath = "D:\\新建文件夹2.zip";
            ZipHelper.Compress(sourceDirectory, compressedArchivePath);
            Console.WriteLine("压缩成功.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}