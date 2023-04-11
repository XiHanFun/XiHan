#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:SysTasks
// Guid:cf17417c-79fa-4785-b490-feea07bbf6e3
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-04-11 下午 02:30:34
// ----------------------------------------------------------------​

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Entity;

namespace XiHan.Models.Syses;

/// <summary>
/// 计划任务
/// </summary>
public class SysTasks : BaseDeleteEntity<Guid>
{
    /// <summary>
    /// 程序集名称
    /// </summary>
    [SugarColumn(Length = 100)]
    public string AssemblyName { get; set; } = string.Empty;

    /// <summary>
    /// 任务所在类
    /// </summary>
    [SugarColumn(Length = 100)]
    public string ClassName { get; set; } = string.Empty;

    /// <summary>
    /// 任务名称
    /// </summary>
    [SugarColumn(Length = 20)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 任务分组
    /// </summary>
    [SugarColumn(Length = 20)]
    public string JobGroup { get; set; } = string.Empty;

    /// <summary>
    /// 任务描述
    /// </summary>
    [SugarColumn(Length = 100)]
    public string Remark { get; set; } = string.Empty;

    /// <summary>
    /// 任务类型 1、程序集 2、网络请求 3、SQL语句
    /// </summary>
    public int TaskType { get; set; }

    /// <summary>
    /// 触发器类型 0、Interval 定时任务 1、Cron 时间点或者周期性任务
    /// </summary>
    public int TriggerType { get; set; }

    /// <summary>
    /// 运行时间表达式
    /// </summary>
    [SugarColumn(Length = 100, IsNullable = true)]
    public string Cron { get; set; } = string.Empty;

    /// <summary>
    /// 执行间隔时间 单位秒
    /// </summary>
    public int IntervalSecond { get; set; }

    /// <summary>
    /// 执行次数
    /// </summary>
    public int RunTimes { get; set; }

    /// <summary>
    /// 是否启动
    /// </summary>
    public bool IsStart { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime BeginTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// 最后运行时间
    /// </summary>
    public DateTime? LastRunTime { get; set; }

    /// <summary>
    /// 传入参数
    /// </summary>
    [SugarColumn(Length = 500, IsNullable = true)]
    public string? JobParams { get; set; } = string.Empty;

    /// <summary>
    /// Api执行地址
    /// </summary>
    [SugarColumn(Length = 500, IsNullable = true)]
    public string? ApiUrl { get; set; } = string.Empty;

    /// <summary>
    /// SQL语句
    /// </summary>
    [SugarColumn(Length = 2000, IsNullable = true)]
    public string? SqlText { get; set; } = string.Empty;

    /// <summary>
    /// 网络请求方式
    /// </summary>
    [SugarColumn(Length = 10, IsNullable = true)]
    public string? RequestMethod { get; set; } = string.Empty;
}