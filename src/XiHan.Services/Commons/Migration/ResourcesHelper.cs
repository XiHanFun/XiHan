#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ResourcesService
// Guid:11b53a79-9ca9-4044-92bc-24061ec75715
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-03 下午 04:20:58
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Text;
using XiHan.Services.Commons.Migration.Dtos;
using XiHan.Utils.Formats;
using XiHan.Infrastructures.Infos.BaseInfos;

namespace XiHan.Services.Commons.Migration;

/// <summary>
/// 资源迁移服务
/// </summary>
public class ResourcesService
{
    /// <summary>
    /// 迁移资源url
    /// </summary>
    /// <param name="resourceInfo"></param>
    /// <returns></returns>
    public async Task<List<MigrationInfoDto>> Migration(ResourceInfoDto resourceInfo)
    {
        if (resourceInfo == null)
        {
            throw new ArgumentNullException(nameof(resourceInfo));
        }

        List<MigrationInfoDto> list = new();
        string[] paths = DiskHelper.GetFiles(resourceInfo.Path);
        foreach (var path in paths)
        {
            MigrationInfoDto migrationInfo = new()
            {
                // 路径
                Path = path
            };
            var content = File.ReadAllText(path, Encoding.UTF8);
            // 替换资源
            content = content.FormatReplaceStr(resourceInfo.OldPrefix, resourceInfo.NewPrefix);
            // 刷新重写
            DiskHelper.ClearFile(content);
            DiskHelper.WriteText(path, content, Encoding.UTF8);
            // 迁移成功
            migrationInfo.IsSucess = true;
            list.Add(migrationInfo);
        }
        return await Task.FromResult(list);
    }
}