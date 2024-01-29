#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysSms
// Guid:67146d85-d705-41c4-9d5a-b31f63d85de5
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/1/30 0:30:08
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Domain.Bases.Attributes;
using XiHan.Domain.Bases.Entities;

namespace XiHan.Domain.Syses;

/// <summary>
/// 系统短信配置表
/// </summary>
/// <remarks>记录新增，修改信息</remarks>
[SugarTable, SystemTable]
public class SysSms : BaseModifiedEntity
{
    /// <summary>
    /// 配置标题
    /// </summary>
    [SugarColumn(Length = 64)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 是否可用
    /// </summary>
    [SugarColumn]
    public bool IsEnabled { get; set; }

    /// <summary>
    /// 配置模板
    /// </summary>
    [SugarColumn]
    public string Template { get; set; } = string.Empty;
}