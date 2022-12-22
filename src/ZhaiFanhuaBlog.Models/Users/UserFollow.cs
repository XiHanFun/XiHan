#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserFollow
// Guid:196d9961-eb5f-4e8d-807d-a29b87a0a4f9
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:05:37
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Users;

/// <summary>
/// 用户关注表
/// </summary>
public class UserFollow : BaseEntity
{
    /// <summary>
    /// 关注用户
    /// </summary>
    public Guid FollowedAccountId { get; set; }

    /// <summary>
    /// 备注名称
    /// </summary>
    [SugarColumn(Length = 20, IsNullable = true)]
    public string? RemarkName { get; set; }
}