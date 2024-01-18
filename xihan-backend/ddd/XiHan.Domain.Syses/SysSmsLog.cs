#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysSmsLog
// Guid:b1af5dd4-d8f3-4b42-a187-59ce4dad43f7
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/12/31 6:29:39
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Domain.Bases.Attributes;
using XiHan.Domain.Bases.Entities;

namespace XiHan.Domain.Syses;

/// <summary>
/// 系统短信日志表
/// </summary>
/// <remarks>记录新增信息</remarks>
[SugarTable, SystemTable]
public class SysSmsLog : BaseDeletedEntity
{
    /// <summary>
    /// 手机号码
    /// </summary>
    [SugarColumn(Length = 32)]
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// 发送内容
    /// </summary>
    [SugarColumn]
    public string Body { get; set; } = string.Empty;

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