#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysConfigSeedData
// Guid:9afb9465-4246-4a2a-987f-25b166729ce0
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-08-04 下午 03:55:25
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Models.Bases.Attributes;
using XiHan.Models.Bases.Interface;

namespace XiHan.Models.Syses.SeedDatas;

/// <summary>
/// 系统配置表种子数据
/// </summary>
public class SysConfigSeedData : ISeedData<SysConfig>
{
    /// <summary>
    /// 种子数据
    /// </summary>
    /// <returns></returns>
    [IgnoreUpdate]
    public IEnumerable<SysConfig> HasData()
    {
        var result = new List<SysConfig>();

        // 模式配置
        var modeConfig = new List<SysConfig>
        {
             new SysConfig{
                BaseId = 0,
                TypeCode="ModeConfig",
                Code="IsDemoMode",
                Name="是否演示模式",
                Value="false",
                IsOfficial=true,
                Description="网站是否为演示模式"
            },
        };
        // 站点配置
        var siteConfig = new List<SysConfig>
        {
            new SysConfig{
                BaseId = 1,
                TypeCode="SiteConfig",
                Code="Name",
                Name="网站名称",
                Value="曦寒",
                IsOfficial=true,
                Description="网站访问时显示的名称"
            },
            new SysConfig{
                BaseId = 2,
                TypeCode="SiteConfig",
                Code="Description",
                Name="网站描述",
                Value="高效快速 拥抱开源 用心创作 探索未知",
                IsOfficial=true,
                Description="网站访问时显示的描述"
            },
            new SysConfig{
                BaseId = 3,
                TypeCode="SiteConfig",
                Code="KeyWord",
                Name="网站关键字",
                Value="曦寒,元宇宙,个人知识产权,全场景应用软件,XiHan,Metaverse",
                IsOfficial=true,
                Description="网站访问时显示的关键字"
            },
            new SysConfig{
                BaseId = 4,
                TypeCode="SiteConfig",
                Code="Domain",
                Name="网站域名",
                Value="https://xihan.fun",
                IsOfficial=true,
                Description="网站访问时显示的域名"
            },
            new SysConfig{
                BaseId = 5,
                TypeCode="SiteConfig",
                Code="IcpRegistrationNumber",
                Name="ICP备案号",
                Value="",
                IsOfficial=true,
                Description="网站被访问时底部显示的ICP备案号"
            },

            new SysConfig{
                BaseId = 6,
                TypeCode="SiteConfig",
                Code="PublicSecurityRegistrationNumber",
                Name="公安备案号",
                Value="",
                IsOfficial=true,
                Description="网站被访问时底部显示的公安备案号"
            },
            new SysConfig{
                BaseId = 7,
                TypeCode="SiteConfig",
                Code="UpdateTime",
                Name="升级时间",
                Value="",
                IsOfficial=true,
                Description="网站版本升级时间"
            }
        };
        // 日志配置
        var logConfig = new List<SysConfig>
        {
            new SysConfig{
                BaseId = 11,
                TypeCode="LogConfig",
                Code="Login",
                Name="登录日志配置",
                Value="true",
                IsOfficial=true,
                Description="站点根据此配置开关登录日志"
            },
            new SysConfig{
                BaseId = 12,
                TypeCode="LogConfig",
                Code="Operation",
                Name="操作日志配置",
                Value="true",
                IsOfficial=true,
                Description="站点根据此配置开关操作日志"
            },
            new SysConfig{
                BaseId = 13,
                TypeCode="LogConfig",
                Code="Exception",
                Name="异常日志配置",
                Value="true",
                IsOfficial=true,
                Description="站点根据此配置开关异常日志"
            },
        };

        result.AddRange(modeConfig);
        result.AddRange(siteConfig);
        result.AddRange(logConfig);

        return result;
    }
}