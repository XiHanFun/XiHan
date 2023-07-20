#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ServerInfoRDto
// Guid:66b00579-d3de-4482-9f7b-e755fb2ec2bd
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-20 下午 12:11:50
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Infos;

namespace XiHan.Services.Syses.Servers.Dtos;

/// <summary>
/// ServerInfoRDto
/// </summary>
public class ServerInfoRDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public ServerInfoRDto()
    {
        SystemInfo = new SystemInfoHelper();
        EnvironmentInfo = new EnvironmentInfoHelper();
        ApplicationInfo = new ApplicationInfoHelper();
    }

    /// <summary>
    /// 系统信息
    /// </summary>
    public SystemInfoHelper SystemInfo { get; set; }

    /// <summary>
    /// 环境信息
    /// </summary>
    public EnvironmentInfoHelper EnvironmentInfo { get; set; }

    /// <summary>
    /// 应用信息
    /// </summary>
    public ApplicationInfoHelper ApplicationInfo { get; set; }
}