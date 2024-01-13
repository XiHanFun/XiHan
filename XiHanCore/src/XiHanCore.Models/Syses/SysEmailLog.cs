#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysLogEmail
// Guid:51802bf7-e47d-4b0d-a570-71168a2d0d0f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/12/31 6:28:23
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHanCore.Models.Bases.Attributes;
using XiHanCore.Models.Bases.Entities;

namespace XiHanCore.Models.Syses;

/// <summary>
/// 系统邮件日志表
/// </summary>
/// <remarks>记录新增信息</remarks>
[SugarTable, SystemTable]
public class SysEmailLog : BaseDeleteEntity
{
    /// <summary>
    /// 发送邮箱
    /// </summary>
    [SugarColumn(Length = 64)]
    public string FromMail { get; set; } = string.Empty;

    /// <summary>
    /// 发送主题
    /// </summary>
    [SugarColumn]
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// 发送内容
    /// </summary>
    [SugarColumn]
    public string Body { get; set; } = string.Empty;

    /// <summary>
    /// 是否网页形式
    /// </summary>
    [SugarColumn]
    public bool IsBodyHtml { get; set; } = true;

    /// <summary>
    /// 接收者邮箱
    /// </summary>
    [SugarColumn]
    public string ToMail { get; set; } = string.Empty;

    /// <summary>
    /// 抄送给邮箱
    /// </summary>
    [SugarColumn]
    public string CcMail { get; set; } = string.Empty;

    /// <summary>
    /// 密送给邮箱
    /// </summary>
    [SugarColumn]
    public string BccMail { get; set; } = string.Empty;

    /// <summary>
    /// 附件
    /// </summary>
    [SugarColumn]
    public string AttachmentsPath { get; set; } = string.Empty;

    /// <summary>
    /// 是否已发送(1已发送 0未发送)
    /// </summary>
    [SugarColumn]
    public bool IsSend { get; set; }

    /// <summary>
    /// 是否执行成功(1正常 0失败)
    /// </summary>
    [SugarColumn]
    public bool IsSuccess { get; set; }
}