#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysJobLog
// Guid:238e4849-c954-4aad-955f-db05adadc267
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-19 下午 11:51:33
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Attributes;
using XiHan.Models.Bases.Entities;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统计划任务日志表
/// </summary>
/// <remarks>记录新增信息</remarks>
[SystemTable]
[SugarTable("Sys_Job_Log")]
public class SysJobLog : BaseDeleteEntity
{
    /// <summary>
    /// 所属任务
    /// </summary>
    public long JobId { get; set; }

    /// <summary>
    /// 任务名称
    /// </summary>
    [SugarColumn(Length = 32)]
    public string JobName { get; set; } = string.Empty;

    /// <summary>
    /// 任务分组
    /// </summary>
    [SugarColumn(Length = 32)]
    public string JobGroup { get; set; } = string.Empty;

    /// <summary>
    /// 执行信息
    /// </summary>
    [SugarColumn(Length = 256)]
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// 是否执行成功(1正常 0失败)
    /// </summary>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// 异常信息
    /// </summary>
    [SugarColumn(Length = 512, IsNullable = true)]
    public string? Exception { get; set; } = string.Empty;

    /// <summary>
    /// 调用目标字符串
    /// </summary>
    [SugarColumn(Length = 512, IsNullable = true)]
    public string? InvokeTarget { get; set; }

    /// <summary>
    /// 执行用时，毫秒
    /// </summary>
    public double Elapsed { get; set; }
}