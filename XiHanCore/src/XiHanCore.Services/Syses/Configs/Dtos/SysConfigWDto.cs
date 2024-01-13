#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysConfigWDto
// Guid:69e682b3-287a-44c6-94a7-a0662f14f8bf
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/7 6:10:14
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHanCore.Services.Syses.Configs.Dtos;

/// <summary>
/// SysConfigWDto
/// </summary>
public class SysConfigWDto
{
    /// <summary>
    /// 分类编码
    ///</summary>
    public string? TypeCode { get; set; }

    /// <summary>
    /// 配置编码
    ///</summary>
    public string? Code { get; set; }

    /// <summary>
    /// 配置名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 是否系统内置
    /// </summary>
    public bool? IsOfficial { get; set; }
}