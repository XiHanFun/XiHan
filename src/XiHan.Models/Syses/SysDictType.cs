#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysDictType
// Guid:9f923080-701d-4eec-9171-2112f128fdaf
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-10-24 上午 11:10:00
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Entity;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统字典表
/// </summary>
/// <remarks>记录新增，修改信息</remarks>
[SugarTable(TableName = "Sys_Dict_Type")]
public class SysDictType : BaseModifyEntity
{
    /// <summary>
    /// 字典编码
    ///</summary>
    [SugarColumn(Length = 64)]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 字典名称
    /// </summary>
    [SugarColumn(Length = 64)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsEnable { get; set; } = true;

    /// <summary>
    /// 是否系统内置
    /// </summary>
    public bool IsOfficial { get; set; } = false;

    /// <summary>
    /// 字典描述
    /// </summary>
    [SugarColumn(Length = 256, IsNullable = true)]
    public string? Description { get; set; }
}