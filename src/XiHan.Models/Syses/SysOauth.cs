#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:SysOauth
// Guid:264eff54-6625-456e-96e6-b305db3682a5
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-19 上午 02:25:36
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using System.ComponentModel;
using XiHan.Models.Bases.Entity;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统开放授权协议表
/// </summary>
[SugarTable(TableName = "Sys_Oauth")]
public class SysOauth : BaseDeleteEntity
{
    /// <summary>
    /// 开放授权协议类型
    /// OauthTypeEnum
    /// </summary>
    [SugarColumn(Length = 10)]
    public string OauthType { get; set; } = string.Empty;

    /// <summary>
    /// 客户端ID
    /// </summary>
    [SugarColumn(Length = 100)]
    public string ClientId { get; set; } = string.Empty;

    /// <summary>
    /// 客户端机密
    /// </summary>
    [SugarColumn(Length = 100)]
    public string ClientSecret { get; set; } = string.Empty;

    /// <summary>
    /// 授权范围
    /// </summary>
    [SugarColumn(Length = 100)]
    public string Scope { get; set; } = string.Empty;

    /// <summary>
    /// 重定向地址
    /// </summary>
    [SugarColumn(Length = 100)]
    public string RedirectUri { get; set; } = string.Empty;

    /// <summary>
    /// 是否可用
    /// </summary>
    public bool IsEnabled { get; set; }
}