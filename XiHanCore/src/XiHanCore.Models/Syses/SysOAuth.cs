#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysOAuth
// Guid:264eff54-6625-456e-96e6-b305db3682a5
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-19 上午 02:25:36
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHanCore.Models.Bases.Attributes;
using XiHanCore.Models.Bases.Entities;
using XiHanCore.Models.Syses.Enums;

namespace XiHanCore.Models.Syses;

/// <summary>
/// 系统三方开放授权协议表
/// </summary>
/// <remarks>记录新增，修改信息</remarks>
[SugarTable, SystemTable]
public class SysOAuth : BaseModifyEntity
{
    /// <summary>
    /// 三方开放授权协议类型
    /// </summary>
    [SugarColumn]
    public OAuthTypeEnum OAuthType { get; set; }

    /// <summary>
    /// 客户端ID
    /// </summary>
    [SugarColumn(Length = 128)]
    public string ClientId { get; set; } = string.Empty;

    /// <summary>
    /// 客户端机密
    /// </summary>
    [SugarColumn(Length = 128)]
    public string Secret { get; set; } = string.Empty;

    /// <summary>
    /// 授权范围
    /// </summary>
    [SugarColumn(Length = 256)]
    public string Scope { get; set; } = string.Empty;

    /// <summary>
    /// 重定向地址
    /// </summary>
    [SugarColumn(Length = 256)]
    public string RedirectUri { get; set; } = string.Empty;

    /// <summary>
    /// 是否可用
    /// </summary>
    [SugarColumn]
    public bool IsEnabled { get; set; }
}