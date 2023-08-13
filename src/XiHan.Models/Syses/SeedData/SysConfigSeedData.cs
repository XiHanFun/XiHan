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

using XiHan.Models.Bases;
using XiHan.Models.Bases.Attributes;

namespace XiHan.Models.Syses.SeedDatas;

/// <summary>
/// 系统配置表种子数据
/// </summary>
public class SysConfigSeedData : ISeedDataFilter<SysConfig>
{
    /// <summary>
    /// 种子数据
    /// </summary>
    /// <returns></returns>
    [IgnoreUpdate]
    public IEnumerable<SysConfig> HasData()
    {
        var result = new List<SysConfig>();

        // 站点配置
        var siteConfig = new List<SysConfig>
        {
            new SysConfig{
                BaseId = 1,
                TypeCode="SysSiteConfig",
                Code="Name",
                Name="网站名称",
                Value="曦寒",
                IsOfficial=true,
                SortOrder=1,
                Description="网站访问时显示的名称"
            },
            new SysConfig{
                BaseId = 2,
                TypeCode="SysSiteConfig",
                Code="Description",
                Name="网站描述",
                Value="高效快速 拥抱开源 用心创作 探索未知",
                IsOfficial=true,
                SortOrder=2,
                Description="网站访问时显示的描述"
            },
            new SysConfig{
                BaseId = 3,
                TypeCode="SysSiteConfig",
                Code="KeyWord",
                Name="网站关键字",
                Value="曦寒,元宇宙,个人知识产权,全场景应用软件,XiHan,Metaverse",
                IsOfficial=true,
                SortOrder=3,
                Description="网站访问时显示的关键字"
            },
            new SysConfig{
                BaseId = 4,
                TypeCode="SysSiteConfig",
                Code="Domain",
                Name="网站域名",
                Value="https://xihan.fun",
                IsOfficial=true,
                SortOrder=4,
                Description="网站访问时显示的域名"
            },
            new SysConfig{
                BaseId = 5,
                TypeCode="SysSiteConfig",
                Code="IcpRegistrationNumber",
                Name="ICP备案号",
                Value=string.Empty,
                IsOfficial=true,
                SortOrder=5,
                Description="网站被访问时底部显示的ICP备案号"
            },

            new SysConfig{
                BaseId = 6,
                TypeCode="SysSiteConfig",
                Code="PublicSecurityRegistrationNumber",
                Name="公安备案号",
                Value=string.Empty,
                IsOfficial=true,
                SortOrder=6,
                Description="网站被访问时底部显示的公安备案号"
            },
            new SysConfig{
                BaseId = 7,
                TypeCode="SysSiteConfig",
                Code="UpdateTime",
                Name="升级时间",
                Value=string.Empty,
                IsOfficial=true,
                SortOrder=7,
                Description="网站版本升级时间"
            }
        };
        // 日志配置
        var logConfig = new List<SysConfig>
        {
            new SysConfig{
                BaseId = 11,
                TypeCode="SysLogConfig",
                Code="Exception",
                Name="异常日志配置",
                Value="true",
                IsOfficial=true,
                SortOrder=1,
                Description="站点根据此配置开关异常日志"
            },
            new SysConfig{
                BaseId = 12,
                TypeCode="SysLogConfig",
                Code="Operation",
                Name="操作日志配置",
                Value="true",
                IsOfficial=true,
                SortOrder=2,
                Description="站点根据此配置开关操作日志"
            },
            new SysConfig{
                BaseId = 13,
                TypeCode="SysLogConfig",
                Code="Login",
                Name="登录日志配置",
                Value="true",
                IsOfficial=true,
                SortOrder=3,
                Description="站点根据此配置开关登录日志"
            },
        };

        // 模式配置
        var modeConfig = new List<SysConfig>
        {
             new SysConfig{
                BaseId = 21,
                TypeCode="SysModeConfig",
                Code="IsDemoMode",
                Name="是否演示模式",
                Value="false",
                IsOfficial=true,
                SortOrder=1,
                Description="网站是否为演示模式"
            },
        };

        result.AddRange(siteConfig);
        result.AddRange(logConfig);
        result.AddRange(modeConfig);

        return result;
    }
}