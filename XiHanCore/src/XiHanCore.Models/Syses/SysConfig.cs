#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysConfig
// Guid:826c51d5-2ef4-43cb-baf3-fc08fd843c19
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 06:35:58
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHanCore.Models.Bases.Attributes;
using XiHanCore.Models.Bases.Entities;

namespace XiHanCore.Models.Syses;

/// <summary>
/// 系统配置表
/// </summary>
/// <remarks>记录新增，修改信息</remarks>
[SugarTable, SystemTable]
public class SysConfig : BaseModifyEntity
{
    /// <summary>
    /// 分类编码
    ///</summary>
    [SugarColumn(Length = 64)]
    public string TypeCode { get; set; } = string.Empty;

    /// <summary>
    /// 配置编码
    ///</summary>
    [SugarColumn(Length = 64)]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 配置名称
    /// </summary>
    [SugarColumn(Length = 64)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 配置项值
    /// </summary>
    [SugarColumn(Length = 512)]
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// 是否系统内置
    /// </summary>
    [SugarColumn]
    public bool IsOfficial { get; set; } = false;

    /// <summary>
    /// 配置排序
    /// </summary>
    [SugarColumn]
    public int SortOrder { get; set; }

    /// <summary>
    /// 字典描述
    /// </summary>
    [SugarColumn(Length = 256, IsNullable = true)]
    public string? Description { get; set; }
}