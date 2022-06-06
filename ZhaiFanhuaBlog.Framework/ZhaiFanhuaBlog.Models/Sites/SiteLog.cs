// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SiteLog
// Guid:0b64e03b-c87c-4c6e-9262-7e4f5e507ce2
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:37:47
// ----------------------------------------------------------------

using SqlSugar;
using System.Net;
using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Sites;

/// <summary>
/// 网站日志表
/// </summary>
public class SiteLog : BaseDeleteEntity<Guid>
{
    /// <summary>
    /// 日志级别
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(10)", IsNullable = true)]
    public string? LogLevel { get; set; } = null;

    /// <summary>
    /// 触发线程
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public int LogThread { get; set; }

    /// <summary>
    /// 出错文件
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(200)", IsNullable = true)]
    public string? LogFile { get; set; } = null;

    /// <summary>
    /// 出错行号
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public int LogLine { get; set; }

    /// <summary>
    /// 请求类名
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(200)", IsNullable = true)]
    public string? LogType { get; set; } = null;

    /// <summary>
    /// 事件对象
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(200)", IsNullable = true)]
    public string? Logger { get; set; } = null;

    /// <summary>
    /// 消息描述
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(500)", IsNullable = true)]
    public string? Message { get; set; } = null;

    /// <summary>
    /// 错误详情
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(4000)", IsNullable = true)]
    public string? Exception { get; set; } = null;

    /// <summary>
    /// 来源Ip
    /// </summary>
    [SugarColumn(ColumnDataType = "varbinary(16)", IsNullable = true)]
    public IPAddress? SourceIp { get; set; }

    /// <summary>
    /// 请求类型 GET、POST等
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(10)", IsNullable = true)]
    public string? RequestType { get; set; } = null;

    /// <summary>
    /// 来源页面
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(100)", IsNullable = true)]
    public string? Referrer { get; set; } = null;

    /// <summary>
    /// 代理信息
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(100)", IsNullable = true)]
    public string? Agent { get; set; } = null;
}