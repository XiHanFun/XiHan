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
using XiHan.Models.Bases.Entity;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统配置表
/// </summary>
/// <remarks>记录新增，修改信息</remarks>
[SugarTable(TableName = "Sys_Config")]
public class SysConfig : BaseModifyEntity
{
    /// <summary>
    /// 分组编码
    ///</summary>
    [SugarColumn(Length = 64)]
    public string GroupCode { get; set; } = string.Empty;

    /// <summary>
    /// 配置编码
    ///</summary>
    [SugarColumn(Length = 64)]
    public string ConfigCode { get; set; } = string.Empty;

    /// <summary>
    /// 配置名称
    /// </summary>
    [SugarColumn(Length = 64)]
    public string ConfigName { get; set; } = string.Empty;

    /// <summary>
    /// 配置项值
    /// </summary>
    [SugarColumn(Length = 10)]
    public string ConfigValue { get; set; } = string.Empty;

    /// <summary>
    /// 是否系统内置
    /// </summary>
    public bool IsOfficial { get; set; } = false;

    /// <summary>
    /// 字典描述
    /// </summary>
    [SugarColumn(Length = 200, IsNullable = true)]
    public string? Description { get; set; }
}