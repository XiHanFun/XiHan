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
/// 友情链接表
/// </summary>
public class RootFriendlyLink : BaseEntity
{
    /// <summary>
    /// 用户名
    /// </summary>
    public Guid AccountID { get; set; }

    /// <summary>
    /// 友链名称
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(100)")]
    public string? Name { get; set; } = null;

    /// <summary>
    /// 头像路径
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(200)")]
    public string AvatarPath { get; set; } = "/Images/RootFriendlyLink/Avatar/defult.png";

    /// <summary>
    /// 友链地址
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(200)")]
    public string? Url { get; set; } = null;

    /// <summary>
    /// 友链描述
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(100)")]
    public string? Description { get; set; } = null;
}