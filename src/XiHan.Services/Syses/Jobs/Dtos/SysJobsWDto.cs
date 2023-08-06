#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysJobsWDto
// Guid:06225b1c-7cd5-4fd3-bbd7-70c35419a90d
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-21 下午 05:31:03
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Services.Syses.Jobs.Dtos;

/// <summary>
/// SysJobsWDto
/// </summary>
public class SysJobsWDto
{
    /// <summary>
    /// 任务名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 任务分组
    /// </summary>
    public string? Group { get; set; }

    /// <summary>
    /// 任务类型
    /// JobTypeEnum
    /// </summary>
    public int? JobType { get; set; }

    /// <summary>
    /// 触发器类型
    /// TriggerTypeEnum
    /// </summary>
    public int? TriggerType { get; set; }

    /// <summary>
    /// 是否启动
    /// </summary>
    public bool? IsStart { get; set; }
}