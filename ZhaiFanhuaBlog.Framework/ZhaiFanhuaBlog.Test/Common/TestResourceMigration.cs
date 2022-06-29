// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:TestResourceMigration
// Guid:ec23382c-4c63-4d3e-b4a4-6c46b3b1808c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-30 上午 02:06:58
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Utils.Console;
using ZhaiFanhuaBlog.Utils.Migration;

namespace ZhaiFanhuaBlog.Test.Common;

/// <summary>
/// 测试资源迁移
/// </summary>
public static class TestResourceMigration
{
    public static void ResourceMigration()
    {
        ResourceInfo resourceInfo = new ResourceInfo();
        resourceInfo.Path = @"C:\Users\zhaifanhua\Desktop\technology";
        resourceInfo.OldPrefix = @"https://img.zhaifanhua.com/blogimg";
        resourceInfo.NewPrefix = @"https://cdn.zhaifanhua.com/blog/img";
        ConsoleHelper.WriteInfoLine($@"============资源迁移开始============");
        List<MigrationInfo> migrationInfos = CDNResourcesHelper.Migration(resourceInfo);
        foreach (MigrationInfo migrationInfo in migrationInfos)
        {
            if (migrationInfo.IsSucess)
            {
                ConsoleHelper.WriteSuccessLine($@"资源迁移成功，路径【{migrationInfo.Path}】");
            }
            else
            {
                ConsoleHelper.WriteErrorLine($@"资源迁移失败，路径【{migrationInfo.Path}】");
            }
        }
        ConsoleHelper.WriteInfoLine($@"============资源迁移结束============");
    }
}