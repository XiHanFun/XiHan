#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:SysEmail
// Guid:89af545a-053f-4e16-83b7-126f7fbe7f45
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-19 上午 02:58:04
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Entity;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统邮件配置
/// </summary>
/// <remarks>记录创建，修改信息</remarks>
[SugarTable(TableName = "Sys_Email")]
public class SysEmail : BaseModifyEntity
{
    /// <summary>
    /// 主机服务器
    /// </summary>
    [SugarColumn(Length = 20)]
    public string Host { get; set; } = string.Empty;

    /// <summary>
    /// 端口
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// 是否SSL加密
    /// </summary>
    public bool UseSsl { get; set; }

    /// <summary>
    /// 发自名称
    /// </summary>
    [SugarColumn(Length = 20)]
    public string FromUserName { get; set; } = string.Empty;

    /// <summary>
    /// 发自密码
    /// </summary>
    [SugarColumn(Length = 64)]
    public string FromPassword { get; set; } = string.Empty;

    /// <summary>
    /// 发自地址
    /// </summary>
    [SugarColumn(Length = 50)]
    public string FromAddress { get; set; } = string.Empty;
}