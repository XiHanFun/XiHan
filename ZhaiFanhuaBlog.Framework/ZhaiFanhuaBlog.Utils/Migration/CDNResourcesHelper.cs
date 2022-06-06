// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CDNResourcesHelper
// Guid:11b53a79-9ca9-4044-92bc-24061ec75715
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-03 下午 04:20:58
// ----------------------------------------------------------------

using System.Text;
using ZhaiFanhuaBlog.Utils.DirFile;
using ZhaiFanhuaBlog.Utils.Formats;

namespace ZhaiFanhuaBlog.Utils.Migration;

/// <summary>
/// CDN资源迁移帮助类
/// </summary>
public static class CDNResourcesHelper
{
    /// <summary>
    /// 迁移资源url
    /// </summary>
    /// <param name="resourceInfo"></param>
    /// <returns></returns>
    public static List<MigrationInfo> Migration(ResourceInfo resourceInfo)
    {
        try
        {
            if (resourceInfo == null) throw new ArgumentNullException(nameof(resourceInfo));
            List<MigrationInfo> list = new List<MigrationInfo>();
            string[] paths = DirFileHelper.GetFiles(resourceInfo.Path);
            foreach (string path in paths)
            {
                MigrationInfo migrationInfo = new MigrationInfo();
                // 路径
                migrationInfo.Path = path;
                string content = File.ReadAllText(path, Encoding.UTF8);
                // 替换资源
                content = StringFormatHelper.ReplaceStr(content, resourceInfo.OldPrefix, resourceInfo.NewPrefix);
                // 刷新重写
                DirFileHelper.ClearFile(content);
                DirFileHelper.WriteText(path, content, Encoding.UTF8);
                // 迁移成功
                migrationInfo.IsSucess = true;
                list.Add(migrationInfo);
            }
            return list;
        }
        catch (Exception)
        {
            throw;
        }
    }
}