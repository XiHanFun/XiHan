#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysJobsService
// Guid:52fd0cf5-e077-4447-b808-bc02e504124d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/7/19 1:20:53
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Apps.Services;
using XiHan.Models.Syses;
using XiHan.Services.Bases;

namespace XiHan.Services.Syses.Jobs.Logic;

/// <summary>
/// 系统任务服务
/// </summary>
[AppService(ServiceType = typeof(ISysJobsService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysJobsService : BaseService<SysJobs>, ISysJobsService
{
}