// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RootState
// Guid:aa5837c6-b11d-4a89-b1ce-bad5eea067bc
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:52:00
// ----------------------------------------------------------------

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Roots;

/// <summary>
/// 系统状态表
/// </summary>
public class RootState : BaseDeleteEntity<Guid>
{
    /// <summary>
    /// 类型代码（英文表名称）
    /// </summary>
    /// <returns></returns>
    [SugarColumn(ColumnDataType = "nvarchar(20)")]
    public string? TypeKey { get; set; } = null;

    /// <summary>
    /// 类型名称（中文表名称）
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(20)")]
    public string? TypeName { get; set; } = null;

    /// <summary>
    /// 状态代码 正常值(1)
    /// </summary>
    public int StateKey { get; set; }

    /// <summary>
    /// 状态名称
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(20)")]
    public string? StateName { get; set; } = null;
}