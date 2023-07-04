﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:SysTasks
// long:cf17417c-79fa-4785-b490-feea07bbf6e3
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-11 下午 02:30:34
// ----------------------------------------------------------------​

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Entity;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统计划任务表
/// </summary>
/// <remarks>记录新增，修改，删除信息</remarks>
[SugarTable(TableName = "Sys_Tasks")]
public class SysTasks : BaseDeleteEntity
{
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
    [SugarColumn(Length = 100, IsNullable = true)]
    public string? Description { get; set; }

    #region 任务类型

    /// <summary>
    /// 任务类型
    /// JobTypeEnum
    /// </summary>
    public int JobType { get; set; }

    #region 程序集

    /// <summary>
    /// 程序集名称
    /// </summary>
    [SugarColumn(Length = 200, IsNullable = true)]
    public string? AssemblyName { get; set; }

    /// <summary>
    /// 任务所在类
    /// </summary>
    [SugarColumn(Length = 200, IsNullable = true)]
    public string? ClassName { get; set; }

    /// <summary>
    /// 传入参数
    /// </summary>
    [SugarColumn(Length = 500, IsNullable = true)]
    public string? JobParams { get; set; }

    #endregion

    #region 网络请求

    /// <summary>
    /// 网络请求方式
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public int? RequestMethod { get; set; }

    /// <summary>
    /// Api执行地址
    /// </summary>
    [SugarColumn(Length = 500, IsNullable = true)]
    public string? ApiUrl { get; set; } = string.Empty;

    #endregion

    #region SQL语句

    /// <summary>
    /// SQL语句
    /// </summary>
    [SugarColumn(Length = 2000, IsNullable = true)]
    public string? SqlText { get; set; } = string.Empty;

    #endregion

    #endregion

    #region 触发器类型

    /// <summary>
    /// 触发器类型
    /// TriggerTypeEnum
    /// </summary>
    public int TriggerType { get; set; }

    #region 定时任务

    /// <summary>
    /// 执行间隔时间 单位秒
    /// </summary>
    public int IntervalSecond { get; set; }

    /// <summary>
    /// 执行次数
    /// </summary>
    public int RunTimes { get; set; }

    /// <summary>
    /// 循环执行次数
    /// </summary>
    public int CycleRunTimes { get; set; }

    /// <summary>
    /// 已循环次数
    /// </summary>
    public int CycleHasRunTimes { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public DateTime? BeginTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// 最后运行时间
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public DateTime? LastRunTime { get; set; }

    /// <summary>
    /// 是否启动
    /// </summary>
    public bool IsStart { get; set; }

    #endregion

    #region 时间点或者周期性任务

    /// <summary>
    /// 运行时间表达式
    /// </summary>
    [SugarColumn(Length = 100, IsNullable = true)]
    public string? Cron { get; set; }

    #endregion

    #endregion
}