#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysPost
// Guid:e17ddd10-c5c5-4885-b080-82f9e291eddd
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-25 下午 05:46:31
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Attributes;
using XiHan.Models.Bases.Entities;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统岗位表
/// </summary>
/// <remarks>记录新增，修改，删除信息</remarks>
[SugarTable, SystemTable]
public class SysPost : BaseDeleteEntity
{
    /// <summary>
    /// 岗位编码
    /// </summary>
    [SugarColumn(Length = 64)]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 岗位名称
    /// </summary>
    [SugarColumn(Length = 64)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 岗位排序
    /// </summary>
    [SugarColumn]
    public int SortOrder { get; set; }

    /// <summary>
    /// 岗位描述
    /// </summary>
    [SugarColumn(Length = 256, IsNullable = true)]
    public string? Description { get; set; }
}