#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ResourcesService
// Guid:11b53a79-9ca9-4044-92bc-24061ec75715
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-06-03 下午 04:20:58
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Text;
using XiHan.Services.Commons.Migration.Dtos;
using XiHan.Utils.Extensions;
using XiHan.Utils.Files;

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
    public static async Task<List<MigrationInfoDto>> Migration(ResourceInfoDto resourceInfo)
    {
        ArgumentNullException.ThrowIfNull(resourceInfo);

        List<MigrationInfoDto> list = [];
        IEnumerable<string> paths = FileHelper.GetFiles(resourceInfo.Path);
        foreach (var path in paths)
        {
            MigrationInfoDto migrationInfo = new()
            {
                // 路径
                Path = path
            };
            var content = await File.ReadAllTextAsync(path, Encoding.UTF8);
            // 替换资源
            content = content.FormatReplaceStr(resourceInfo.OldPrefix, resourceInfo.NewPrefix);
            // 刷新重写
            FileHelper.CleanFile(content);
            FileHelper.WriteText(path, content, Encoding.UTF8);
            // 迁移成功
            migrationInfo.IsSucess = true;
            list.Add(migrationInfo);
        }

        return await Task.FromResult(list);
    }
}