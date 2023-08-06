#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysOrganization
// Guid:a4988c3d-1b3c-4d1d-a9bb-37ed5754e5f1
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-25 上午 10:36:59
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Entity;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统机构表
/// </summary>
/// <remarks>记录新增，修改，删除信息</remarks>
[SugarTable(TableName = "Sys_Organization")]
public class SysOrganization : BaseDeleteEntity
{
    /// <summary>
    /// 父级机构
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public long? ParentId { get; set; }

    /// <summary>
    /// 机构编码
    /// </summary>
    [SugarColumn(Length = 64)]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 机构名称
    /// </summary>
    [SugarColumn(Length = 64)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 负责人
    /// </summary>
    [SugarColumn(IsNullable = true, Length = 64)]
    public string Leader { get; set; } = string.Empty;

    /// <summary>
    /// 手机号码
    /// </summary>
    [SugarColumn(IsNullable = true, Length = 32)]
    public string? Phone { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    [SugarColumn(Length = 64)]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 机构排序
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 机构描述
    /// </summary>
    [SugarColumn(Length = 256, IsNullable = true)]
    public string? Description { get; set; }
}