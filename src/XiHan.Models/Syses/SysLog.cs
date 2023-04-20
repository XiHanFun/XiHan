#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SysLog
// long:0b64e03b-c87c-4c6e-9262-7e4f5e507ce2
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:37:47
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Entity;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统日志表
/// </summary>
[SugarTable(TableName = "Sys_Log")]
public class SysLog : BaseDeleteEntity
{
    /// <summary>
    /// 日志级别
    /// </summary>
    [SugarColumn(Length = 10, IsNullable = true)]
    public string? LogLevel { get; set; }

    /// <summary>
    /// 触发线程
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public int? LogThread { get; set; }

    /// <summary>
    /// 出错文件
    /// </summary>
    [SugarColumn(Length = 200, IsNullable = true)]
    public string? LogFile { get; set; }

    /// <summary>
    /// 出错行号
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public int LogLine { get; set; }

    /// <summary>
    /// 请求类名
    /// </summary>
    [SugarColumn(Length = 200, IsNullable = true)]
    public string? LogClass { get; set; }

    /// <summary>
    /// 事件对象
    /// </summary>
    [SugarColumn(Length = 200, IsNullable = true)]
    public string? LogEvent { get; set; }

    /// <summary>
    /// 消息描述
    /// </summary>
    [SugarColumn(Length = 500, IsNullable = true)]
    public string? LogMessage { get; set; }

    /// <summary>
    /// 错误详情
    /// </summary>
    [SugarColumn(Length = 4000, IsNullable = true)]
    public string? LogException { get; set; }
}