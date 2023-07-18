#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ISysTasksLogService
// Guid:d8291498-8a92-40a9-a37b-4dd187725363
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/7/19 1:14:54
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Models.Syses;
using XiHan.Services.Bases;

namespace XiHan.Services.Syses.Tasks;

/// <summary>
/// ISysTasksLogService
/// </summary>
public interface ISysTasksLogService : IBaseService<SysTasksLog>
{
    /// <summary>
    /// 新增任务日志
    /// </summary>
    /// <param name="tasksLog"></param>
    /// <returns></returns>
    Task<SysTasksLog> CreateTasksLog(SysTasksLog tasksLog);
}