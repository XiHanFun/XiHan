#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:TestResourceMigration
// Guid:ec23382c-4c63-4d3e-b4a4-6c46b3b1808c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-30 上午 02:06:58
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Utils.Console;
using XiHan.Utils.Migration;

namespace XiHan.Test.Common;

/// <summary>
/// 测试资源迁移
/// </summary>
public static class TestResourceMigration
{
    /// <summary>
    /// 资源迁移
    /// </summary>
    public static void ResourceMigration()
    {
        var resourceInfo = new ResourceInfo();
        resourceInfo.Path = @"C:\Users\zhaifanhua\Desktop\technology";
        resourceInfo.OldPrefix = @"https://img.zhaifanhua.com/blogimg";
        resourceInfo.NewPrefix = @"https://cdn.zhaifanhua.com/blog/img";
        $@"============资源迁移开始============".WriteLineInfo();
        var migrationInfos = ResourcesHelper.Migration(resourceInfo);
        foreach (var migrationInfo in migrationInfos)
        {
            if (migrationInfo.IsSucess)
            {
                $@"资源迁移成功，路径【{migrationInfo.Path}】".WriteLineSuccess();
            }
            else
            {
                $@"资源迁移失败，路径【{migrationInfo.Path}】".WriteLineError();
            }
        }
        $@"============资源迁移结束============".WriteLineInfo();
    }
}