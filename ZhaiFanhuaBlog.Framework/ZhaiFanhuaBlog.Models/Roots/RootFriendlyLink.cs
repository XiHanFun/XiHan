// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RootFriendlyLink
// Guid:6580a424-f371-43eb-aafb-5ed17facb1aa
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:49:36
// ----------------------------------------------------------------

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Roots;

/// <summary>
/// 系统友情链接表
/// </summary>
[SugarTable("RootFriendlyLink", "系统友情链接表")]
public class RootFriendlyLink : BaseEntity
{
    /// <summary>
    /// 用户名
    /// </summary>
    [SugarColumn(ColumnDescription = "用户名")]
    public Guid AccountId { get; set; }

    /// <summary>
    /// 友链名称
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(100)", ColumnDescription = "友链名称")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 头像路径
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(200)", ColumnDescription = "头像路径")]
    public string AvatarPath { get; set; } = "/Images/RootFriendlyLink/Avatar/defult.png";

    /// <summary>
    /// 友链地址
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(200)", ColumnDescription = "友链地址")]
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// 友链描述
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(100)", ColumnDescription = "友链描述")]
    public string? Description { get; set; }
}