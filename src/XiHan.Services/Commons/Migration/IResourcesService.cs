#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:IResourcesService
// Guid:94e703fd-cb10-4210-a074-0492bf8cdd09
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-19 上午 04:25:47
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Services.Commons.Migration.Dtos;

namespace XiHan.Services.Commons.Migration;

/// <summary>
/// IResourcesService
/// </summary>
public interface IResourcesService
{
    /// <summary>
    /// 迁移资源url
    /// </summary>
    /// <param name="resourceInfo"></param>
    /// <returns></returns>
    Task<List<MigrationInfoDto>> Migration(ResourceInfoDto resourceInfo);
}