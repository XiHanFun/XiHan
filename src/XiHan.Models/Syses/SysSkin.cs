#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SysSkin
// long:76cb14e8-a1a1-4f0b-a2b1-246d39879ade
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:40:24
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Entity;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统皮肤表
/// </summary>
/// <remarks>记录新增，修改信息</remarks>
[SugarTable(TableName = "Sys_Skin")]
public class SysSkin : BaseModifyEntity
{
    /// <summary>
    /// 皮肤名称
    /// </summary>
    [SugarColumn(Length = 20)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 皮肤路径
    /// </summary>
    [SugarColumn(Length = 200)]
    public string Path { get; set; } = string.Empty;
}