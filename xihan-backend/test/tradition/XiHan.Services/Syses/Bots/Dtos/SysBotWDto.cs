#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysBotWDto
// Guid:9dd9ea5e-4b58-49d3-80bb-ba69c8c141dd
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/9/4 2:30:36
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Models.Syses.Enums;

namespace XiHan.Services.Syses.Bots.Dtos;

/// <summary>
/// SysBotWDto
/// </summary>
public class SysBotWDto
{
    /// <summary>
    /// 自定义机器人标题
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// 自定义机器人类型
    /// </summary>
    public BotTypeEnum? BotType { get; set; }

    /// <summary>
    /// 是否可用
    /// </summary>
    public bool? IsEnabled { get; set; }
}