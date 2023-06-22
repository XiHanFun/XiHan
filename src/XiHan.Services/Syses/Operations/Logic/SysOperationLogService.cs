#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:SysOperationLogService
// Guid:8bab505b-cf3a-4778-ae9c-df04b00f66a0
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-06-21 下午 05:43:16
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Apps.Services;
using XiHan.Models.Syses;
using XiHan.Services.Bases;

namespace XiHan.Services.Syses.Operations.Logic;

/// <summary>
/// SysOperationLogService
/// </summary>
[AppService(ServiceType = typeof(ISysOperationLogService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysOperationLogService : BaseService<SysOperationLog>, ISysOperationLogService
{
}