#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:SysDictDataService
// Guid:3f7ca12f-9a86-4e07-b304-1a0f951a133c
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-06-12 下午 04:32:05
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Apps.Services;
using XiHan.Models.Syses;
using XiHan.Services.Bases;

namespace XiHan.Services.Syses.Dicts.Logic;

/// <summary>
/// 系统字典数据服务
/// </summary>
[AppService(ServiceType = typeof(ISysDictDataService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysDictDataService : BaseService<SysDictData>, ISysDictDataService
{
}