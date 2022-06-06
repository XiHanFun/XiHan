// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UtilsTest
// Guid:ea1e5a45-d67d-48b4-847c-1b061674a519
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-04-10 下午 10:49:22
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Utils.Console;
using ZhaiFanhuaBlog.Utils.DirFile;
using ZhaiFanhuaBlog.Utils.Encryptions;
using ZhaiFanhuaBlog.Utils.Formats;
using ZhaiFanhuaBlog.Utils.Migration;

namespace ZhaiFanhuaBlog.Test.Common;

/// <summary>
/// CommonTest
/// </summary>
public class UtilsTest
{
    public UtilsTest()
    {
        //Console.WriteLine($@"【C盘】磁盘大小：{FileSizeFormatHelper.ParseFileSizeBytes(DirFileHelper.GetHardDiskSpace(@"C:\"))}；");
        //Console.WriteLine($@"【C盘】磁盘大小：{FileSizeFormatHelper.ParseFileSizeBytes(DirFileHelper.GetHardDiskFreeSpace(@"C:\"))}；");
        //Console.WriteLine($@"【C盘】磁盘空闲占比：{DirFileHelper.ProportionOfHardDiskFreeSpace(@"C:\")}；");
        //Console.WriteLine($@"【D:\Repository\ZhaiFanhuaBlog】目录大小：{FileSizeFormatHelper.ParseFileSizeBytes(DirFileHelper.GetDirectorySize(@"D:\Repository\ZhaiFanhuaBlog"))}；");
        //Console.WriteLine($@"【D:\Repository\ZhaiFanhuaBlog\README.md】文件大小：{FileSizeFormatHelper.ParseFileSizeBytes(DirFileHelper.GetFileSize(@"D:\Repository\ZhaiFanhuaBlog\README.md"))}；");

        //Console.WriteLine($@"MD5：{MD5Helper.EncryptMD5(@"")}；");

        //ResourceInfo resourceInfo = new ResourceInfo();
        //resourceInfo.Path = @"C:\Users\zhaifanhua\Desktop\technology";
        //resourceInfo.OldPrefix = @"https://img.zhaifanhua.com/blogimg";
        //resourceInfo.NewPrefix = @"https://cdn.zhaifanhua.com/blog/img";
        //ConsoleHelper.WriteInfoLine($@"============资源迁移开始============");
        //List<MigrationInfo> migrationInfos = CDNResourcesHelper.Migration(resourceInfo);
        //foreach (MigrationInfo migrationInfo in migrationInfos)
        //{
        //    if (migrationInfo.IsSucess)
        //    {
        //        ConsoleHelper.WriteSuccessLine($@"资源迁移成功，路径【{migrationInfo.Path}】");
        //    }
        //    else
        //    {
        //        ConsoleHelper.WriteErrorLine($@"资源迁移失败，路径【{migrationInfo.Path}】");
        //    }
        //}
        //ConsoleHelper.WriteInfoLine($@"============资源迁移结束============");
    }
}