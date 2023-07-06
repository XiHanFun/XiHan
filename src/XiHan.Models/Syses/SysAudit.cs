#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysAudit
// Guid:80aaad90-961a-4c97-aca3-12f12d26e056
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 06:44:51
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Entity;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统审核表
/// </summary>
/// <remarks>记录新增，修改，删除信息</remarks>
[SugarTable(TableName = "Sys_Audit")]
public class SysAudit : BaseDeleteEntity
{
    /// <summary>
    /// 审核分类
    /// </summary>
    public long CategoryId { get; set; }

    /// <summary>
    /// 审核内容
    /// </summary>
    [SugarColumn(Length = 2000)]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 审核结果
    /// </summary>
    [SugarColumn(Length = 500, IsNullable = true)]
    public string? Result { get; set; }
}