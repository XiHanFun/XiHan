#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SysOperationLog
// Guid:d7a2c392-4915-4831-9b7c-5cde51f9d618
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2022-11-21 下午 04:52:59
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases.Entity;

namespace ZhaiFanhuaBlog.Models.Syses;

/// <summary>
 /// 站点操作日志表
 /// </summary>
public class SysOperationLog : BaseDeleteEntity<Guid>
{
    /// <summary>
    /// 操作模块
    ///</summary>
    [SugarColumn(ColumnDataType = "nvarchar(20)", IsNullable = true)]
    public string? OperModule { get; set; }

    /// <summary>
    /// 操作类型
    ///</summary>
    [SugarColumn(ColumnDataType = "nvarchar(10)", IsNullable = true)]
    public int? OperType { get; set; }

    /// <summary>
    /// 请求类型 GET、POST等
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(10)", IsNullable = true)]
    public string? RequestType { get; set; }

    /// <summary>
    /// 操作人员
    ///</summary>
    [SugarColumn(ColumnDataType = "nvarchar(20)", IsNullable = true)]
    public string? OperUser { get; set; }

    /// <summary>
    /// 操作Ip
    ///</summary>
    [SugarColumn(ColumnDataType = "nvarchar(20)", IsNullable = true)]
    public string? OperIp { get; set; }

    /// <summary>
    /// 操作地点
    ///</summary>
    [SugarColumn(ColumnDataType = "nvarchar(50)", IsNullable = true)]
    public string? OperLocation { get; set; }

    /// <summary>
    /// 来源页面
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(100)", IsNullable = true)]
    public string? Referrer { get; set; }

    /// <summary>
    /// 操作方法
    ///</summary>
    [SugarColumn(ColumnDataType = "nvarchar(50)", IsNullable = true)]
    public string? OperMethod { get; set; }

    /// <summary>
    /// 请求参数
    ///</summary>
    [SugarColumn(ColumnDataType = "nvarchar(200)", IsNullable = true)]
    public string? RequestParam { get; set; }

    /// <summary>
    /// 请求结果
    ///</summary>
    [SugarColumn(ColumnDataType = "nvarchar(4000)", IsNullable = true)]
    public string? RequestResult { get; set; }
}