#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:MapsterAdaptConifg
// Guid:3cf9595b-d406-4d9d-a22d-74704ba51308
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-17 上午 02:38:04
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Mapster;

namespace XiHan.WebCore.Common.Mapster;

/// <summary>
/// MapsterAdaptConifg
/// </summary>
public static class MapsterAdaptConfig
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