#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SysDictType
// Guid:9f923080-701d-4eec-9171-2112f128fdaf
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-10-24 上午 11:10:00
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases.Entity;

namespace ZhaiFanhuaBlog.Models.Syses;

/// <summary>
/// 站点字典类型表
/// </summary>
public class SysDictType : BaseDeleteEntity<Guid>
{
    /// <summary>
    /// 字典名称
    /// </summary>
    [SugarColumn(Length = 10)]
    public string DictName { get; set; } = string.Empty;

    /// <summary>
    /// 字典类型
    ///</summary>
    [SugarColumn(Length = 50)]
    public string DictType { get; set; } = string.Empty;

    /// <summary>
    /// 字典状态
    /// </summary>
    public bool Status { get; set; } = true;

    /// <summary>
    /// 是否系统内置
    /// </summary>
    public bool IsOfficial { get; set; }

    /// <summary>
    /// 字典描述
    /// </summary>
    [SugarColumn(Length = 50, IsNullable = true)]
    public string? Remark { get; set; }
}