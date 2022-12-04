#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserNotice
// Guid:ee3e1bfb-ce5a-4642-84c8-4bef82afb198
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:13:44
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases.Entity;

namespace ZhaiFanhuaBlog.Models.Users;

/// <summary>
/// 用户通知表
/// </summary>
public class UserNotice : BaseEntity
{
    /// <summary>
    /// 通知标题
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(200)")]
    public string NoticeTitle { get; set; } = string.Empty;

    /// <summary>
    /// 通知内容
    /// </summary>
    [SugarColumn(ColumnDataType = "text")]
    public string NoticeContent { get; set; } = string.Empty;
}