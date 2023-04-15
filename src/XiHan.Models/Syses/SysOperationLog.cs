#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SysOperationLog
// Guid:d7a2c392-4915-4831-9b7c-5cde51f9d618
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-21 下午 04:52:59
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Entity;

namespace XiHan.Models.Syses;

/// <summary>
 /// 站点操作日志表
 /// </summary>
[SugarTable(TableName = "SysOperationLog")]
public class SysOperationLog : BaseDeleteEntity<Guid>
{
    /// <summary>
    /// 操作模块
    ///</summary>
    [SugarColumn(Length = 20, IsNullable = true)]
    public string? Module { get; set; }

    /// <summary>
    /// 操作类型
    ///</summary>
    [SugarColumn(IsNullable = true)]
    public int? Type { get; set; }

    /// <summary>
    /// 请求类型 GET、POST等
    /// </summary>
    [SugarColumn(Length = 10, IsNullable = true)]
    public string? RequestType { get; set; }

    /// <summary>
    /// 操作人员
    ///</summary>
    [SugarColumn(Length = 20, IsNullable = true)]
    public string? User { get; set; }

    /// <summary>
    /// 操作Ip
    ///</summary>
    [SugarColumn(Length = 20, IsNullable = true)]
    public string? Ip { get; set; }

    /// <summary>
    /// 操作地点
    ///</summary>
    [SugarColumn(Length = 50, IsNullable = true)]
    public string? Location { get; set; }

    /// <summary>
    /// 来源页面
    /// </summary>
    [SugarColumn(Length = 100, IsNullable = true)]
    public string? Referrer { get; set; }

    /// <summary>
    /// 操作方法
    ///</summary>
    [SugarColumn(Length = 50, IsNullable = true)]
    public string? Method { get; set; }

    /// <summary>
    /// 请求参数
    ///</summary>
    [SugarColumn(Length = 200, IsNullable = true)]
    public string? RequestParam { get; set; }

    /// <summary>
    /// 请求结果
    ///</summary>
    [SugarColumn(Length = 4000, IsNullable = true)]
    public string? RequestResult { get; set; }
}