#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysRobot
// Guid:e034c85d-9537-4580-ad6b-5974c27915e1
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-19 上午 02:46:34
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Entity;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统机器人配置表
/// </summary>
/// <remarks>记录新增，修改信息</remarks>
[SugarTable(TableName = "Sys_Robot")]
public class SysRobot : BaseModifyEntity
{
    /// <summary>
    /// 自定义机器人类型
    /// RobotTypeEnum
    /// </summary>
    public int RobotType { get; set; }

    /// <summary>
    /// 网络挂钩地址
    /// </summary>
    [SugarColumn(Length = 256)]
    public string WebHookUrl { get; set; } = string.Empty;

    /// <summary>
    /// 访问令牌
    /// 钉钉、飞书为 AccessToken，企业微信为 Key
    /// </summary>
    [SugarColumn(Length = 128)]
    public string AccessTokenOrKey { get; set; } = string.Empty;

    /// <summary>
    /// 机密
    /// 钉钉、飞书用
    /// </summary>
    [SugarColumn(Length = 128, IsNullable = true)]
    public string? Secret { get; set; } = string.Empty;

    /// <summary>
    /// 上传地址
    /// 企业微信用
    /// </summary>
    [SugarColumn(Length = 256, IsNullable = true)]
    public string? UploadUrl { get; set; } = string.Empty;

    /// <summary>
    /// 是否可用
    /// </summary>
    public bool IsEnabled { get; set; }
}