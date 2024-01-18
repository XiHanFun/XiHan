#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysUserStatistic
// Guid:23d7c499-56e3-4d23-835b-80aab213bb38
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 06:16:33
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Domain.Bases.Attributes;
using XiHan.Domain.Bases.Entities;

namespace XiHan.Domain.Syses;

/// <summary>
/// 用户统计表
/// </summary>
/// <remarks>记录新增信息</remarks>
[SugarTable, SystemTable]
public class SysUserStatistic : BaseCreatedEntity
{
    /// <summary>
    /// 所属用户
    /// </summary>
    [SugarColumn]
    public long UserId { get; set; }

    /// <summary>
    /// 关注数量
    /// </summary>
    [SugarColumn]
    public int FollowsCount { get; set; }

    /// <summary>
    /// 粉丝数量
    /// </summary>
    [SugarColumn]
    public int FocusCount { get; set; }

    /// <summary>
    /// 收藏数量
    /// </summary>
    [SugarColumn]
    public int CollectsCount { get; set; }

    /// <summary>
    /// 通知数量
    /// </summary>
    [SugarColumn]
    public int NoticesCount { get; set; }

    /// <summary>
    /// 登录次数
    /// </summary>
    [SugarColumn]
    public int LoginCount { get; set; }
}