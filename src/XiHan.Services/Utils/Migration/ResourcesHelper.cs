#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CDNResourcesHelper
// Guid:11b53a79-9ca9-4044-92bc-24061ec75715
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-03 下午 04:20:58
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Text;
using XiHan.Utils.Formats;
using XiHan.Utils.Info.BaseInfos;

namespace XiHan.Services.Utils.Migration;

/// <summary>
/// 资源迁移帮助类
/// </summary>
public static class ResourcesHelper
{
    /// <summary>
    /// 迁移资源url
    /// </summary>
    /// <param name="resourceInfo"></param>
    /// <returns></returns>
    public static List<MigrationInfo> Migration(ResourceInfo resourceInfo)
    {
        if (resourceInfo == null) throw new ArgumentNullException(nameof(resourceInfo));

        List<MigrationInfo> list = new();
        string[] paths = DiskHelper.GetFiles(resourceInfo.Path);
        foreach (var path in paths)
        {
            MigrationInfo migrationInfo = new()
            {
                // 路径
                Path = path
            };
            var content = File.ReadAllText(path, Encoding.UTF8);
            // 替换资源
            content = content.ReplaceStr(resourceInfo.OldPrefix, resourceInfo.NewPrefix);
            // 刷新重写
            DiskHelper.ClearFile(content);
            DiskHelper.WriteText(path, content, Encoding.UTF8);
            // 迁移成功
            migrationInfo.IsSucess = true;
            list.Add(migrationInfo);
        }
        return list;
    }
}