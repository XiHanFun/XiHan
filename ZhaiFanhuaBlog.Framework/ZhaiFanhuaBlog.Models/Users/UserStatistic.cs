// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserStatistic
// Guid:23d7c499-56e3-4d23-835b-80aab213bb38
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:16:33
// ----------------------------------------------------------------

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Users;

/// <summary>
/// 用户统计表
/// </summary>
[SugarTable("UserStatistic", "用户统计表")]
public class UserStatistic : BaseEntity
{
    /// <summary>
    /// 所属用户
    /// </summary>
    [SugarColumn(ColumnDescription = "所属用户")]
    public Guid AccountId { get; set; }

    /// <summary>
    /// 关注数量
    /// </summary>
    [SugarColumn(ColumnDescription = "关注数量")]
    public int FollowsCount { get; set; } = 0;

    /// <summary>
    /// 粉丝数量
    /// </summary>
    [SugarColumn(ColumnDescription = "粉丝数量")]
    public int FocusCount { get; set; } = 0;

    /// <summary>
    /// 收藏数量
    /// </summary>
    [SugarColumn(ColumnDescription = "收藏数量")]
    public int CollectsCount { get; set; } = 0;

    /// <summary>
    /// 通知数量
    /// </summary>
    [SugarColumn(ColumnDescription = "通知数量")]
    public int NoticesCount { get; set; } = 0;

    /// <summary>
    /// 登录次数
    /// </summary>
    [SugarColumn(ColumnDescription = "登录次数")]
    public int LoginCount { get; set; } = 0;
}