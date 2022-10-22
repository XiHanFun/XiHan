// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:AutoMapperConfig
// Guid:26358f65-1415-4378-9f76-72131179a837
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-29 上午 12:15:37
// ----------------------------------------------------------------

using AutoMapper;

namespace ZhaiFanhuaBlog.Extensions.Common.AutoMapper;

/// <summary>
/// 静态全局 AutoMapper 配置文件
/// </summary>
public class AutoMapperConfig
{
    /// <summary>
    /// 注册配置
    /// </summary>
    /// <returns></returns>
    public static MapperConfiguration RegisterMappings()
    {
        return new MapperConfiguration(config =>
        {
            config.AddProfile(new AutoMapperProfile());
        });
    }
}