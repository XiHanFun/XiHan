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

using XiHan.Models.Bases.Interface;
using XiHan.Models.Syses;

namespace XiHan.Models.SeedDatas;

/// <summary>
/// 系统配置表种子数据
/// </summary>
public class SysConfigSeedData : ISeedData<SysConfig>
{
    /// <summary>
    /// 种子数据
    /// </summary>
    /// <returns></returns>
    public IEnumerable<SysConfig> HasData()
    {
        var result = new List<SysConfig>();

        var siteConfig = new List<SysConfig>
        {
            new SysConfig{
                BaseId = 1,
                GroupCode="SiteConfig",
                ConfigCode="Name",
                ConfigName="网站名称",
                ConfigValue="曦寒",
                IsOfficial=true,
                Description="网站访问时显示的名称"
            },
            new SysConfig{
                BaseId = 2,
                GroupCode="SiteConfig",
                ConfigCode="Description",
                ConfigName="网站描述",
                ConfigValue="高效快速 拥抱开源 用心创作 探索未知",
                IsOfficial=true,
                Description="网站访问时显示的描述"
            },
            new SysConfig{
                BaseId = 3,
                GroupCode="SiteConfig",
                ConfigCode="KeyWord",
                ConfigName="网站关键字",
                ConfigValue="曦寒,元宇宙,个人知识产权,全场景应用软件,XiHan,Metaverse",
                IsOfficial=true,
                Description="网站访问时显示的关键字"
            },
            new SysConfig{
                BaseId = 4,
                GroupCode="SiteConfig",
                ConfigCode="Domain",
                ConfigName="网站域名",
                ConfigValue="https://xihan.fun",
                IsOfficial=true,
                Description="网站访问时显示的域名"
            },
            new SysConfig{
                BaseId = 5,
                GroupCode="SiteConfig",
                ConfigCode="IcpRegistrationNumber",
                ConfigName="ICP备案号",
                ConfigValue="",
                IsOfficial=true,
                Description="网站被访问时底部显示的ICP备案号"
            },

            new SysConfig{
                BaseId = 6,
                GroupCode="SiteConfig",
                ConfigCode="PublicSecurityRegistrationNumber",
                ConfigName="公安备案号",
                ConfigValue="",
                IsOfficial=true,
                Description="网站被访问时底部显示的公安备案号"
            },
            new SysConfig{
                BaseId = 7,
                GroupCode="SiteConfig",
                ConfigCode="UpdateTime",
                ConfigName="升级时间",
                ConfigValue="",
                IsOfficial=true,
                Description="网站版本升级时间"
            },
        };

        var logConfig = new List<SysConfig>
        {
            new SysConfig{
                BaseId = 11,
                GroupCode="LogConfig",
                ConfigCode="Login",
                ConfigName="登录日志配置",
                ConfigValue="true",
                IsOfficial=true,
                Description="站点根据此配置开关登录日志"
            },
            new SysConfig{
                BaseId = 12,
                GroupCode="LogConfig",
                ConfigCode="Operation",
                ConfigName="操作日志配置",
                ConfigValue="true",
                IsOfficial=true,
                Description="站点根据此配置开关操作日志"
            },
            new SysConfig{
                BaseId = 13,
                GroupCode="LogConfig",
                ConfigCode="Exception",
                ConfigName="异常日志配置",
                ConfigValue="true",
                IsOfficial=true,
                Description="站点根据此配置开关异常日志"
            },
        };

        result.AddRange(siteConfig);
        result.AddRange(logConfig);

        return result;
    }
}