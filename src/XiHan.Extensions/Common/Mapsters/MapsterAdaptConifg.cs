#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:MapsterAdaptConifg
// Guid:3cf9595b-d406-4d9d-a22d-74704ba51308
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-17 上午 02:38:04
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Mapster;

namespace XiHan.Extensions.Common.Mapsters;

/// <summary>
/// MapsterAdaptConifg
/// </summary>
public static class MapsterAdaptConifg
{
    /// <summary>
    /// 初始化配置映射关系
    /// </summary>
    /// <returns></returns>
    public static TypeAdapterConfig InitMapperConfig()
    {
        TypeAdapterConfig config = new();

        return config;
    }
}