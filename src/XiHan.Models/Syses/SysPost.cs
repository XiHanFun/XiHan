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
using XiHan.Models.Bases.Entity;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统岗位表
/// </summary>
/// <remarks>记录新增，修改，删除信息</remarks>
[SugarTable(TableName = "Sys_Post")]
public class SysPost : BaseDeleteEntity
{
    /// <summary>
    /// 岗位编码
    /// </summary>
    [SugarColumn(Length = 50)]
    public string PostCode { get; set; } = string.Empty;

    /// <summary>
    /// 岗位名称
    /// </summary>
    [SugarColumn(Length = 20)]
    public string PostName { get; set; } = string.Empty;

    /// <summary>
    /// 岗位排序
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 岗位描述
    /// </summary>
    [SugarColumn(Length = 100, IsNullable = true)]
    public string? Description { get; set; }
}