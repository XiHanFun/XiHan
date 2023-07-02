#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SysFriendlyLink
// long:6580a424-f371-43eb-aafb-5ed17facb1aa
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:49:36
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统友情链接表
/// </summary>
/// <remarks>记录新增，修改，删除，审核，状态信息</remarks>
[SugarTable(TableName = "Sys_Friendly_Link")]
public class SysFriendlyLink : BaseEntity
{
    /// <summary>
    /// 友链名称
    /// </summary>
    [SugarColumn(Length = 100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 头像路径
    /// </summary>
    [SugarColumn(Length = 200)]
    public string AvatarPath { get; set; } = "/Images/SysFriendlyLink/Avatar/default.png";

    /// <summary>
    /// 友链地址
    /// </summary>
    [SugarColumn(Length = 200)]
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// 友链描述
    /// </summary>
    [SugarColumn(Length = 100)]
    public string? Description { get; set; }
}