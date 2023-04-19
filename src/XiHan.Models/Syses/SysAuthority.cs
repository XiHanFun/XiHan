#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SysAuthority
// long:8b190341-c474-4974-961f-895c2c6a831d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 04:45:01
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using System.ComponentModel;
using XiHan.Models.Bases.Entity;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统权限表
/// </summary>
[SugarTable(TableName = "Sys_Authority")]
public class SysAuthority : BaseDeleteEntity
{
    /// <summary>
    /// 父级权限
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public long? ParentId { get; set; }

    /// <summary>
    /// 权限名称
    /// </summary>
    [SugarColumn(Length = 10)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 权限类型
    /// AuthorityTypeEnum
    /// </summary>
    public int AuthorityType { get; set; }

    /// <summary>
    /// 权限描述
    /// </summary>
    [SugarColumn(Length = 50, IsNullable = true)]
    public string? Remark { get; set; }
}

/// <summary>
/// 权限类型
/// </summary>
public enum AuthorityTypeEnum
{
    /// <summary>
    /// 页面权限(菜单级)
    /// </summary>
    [Description("页面权限(菜单级)")]
    Page = 1,

    /// <summary>
    /// 操作权限(按钮级)
    /// </summary>
    [Description("操作权限(按钮级)")]
    Operation = 2,

    /// <summary>
    /// 数据权限(访问响应级）
    /// </summary>
    [Description("数据权限(访问响应级)")]
    Data = 3,
}