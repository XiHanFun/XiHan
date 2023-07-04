#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SysPermission
// long:8b190341-c474-4974-961f-895c2c6a831d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 04:45:01
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Entity;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统权限表
/// </summary>
/// <remarks>记录新增，修改信息</remarks>
[SugarTable(TableName = "Sys_Permission")]
public class SysPermission : BaseModifyEntity
{
    /// <summary>
    /// 父级权限
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public long? ParentId { get; set; }

    /// <summary>
    /// 权限代码
    /// </summary>
    [SugarColumn(Length = 10)]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 权限名称
    /// </summary>
    [SugarColumn(Length = 10)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 权限类型
    /// PermissionTypeEnum
    /// </summary>
    public int PermissionType { get; set; }

    /// <summary>
    /// 权限描述
    /// </summary>
    [SugarColumn(Length = 50, IsNullable = true)]
    public string? Description { get; set; }
}