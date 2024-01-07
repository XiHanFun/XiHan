#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SeoTest
// Guid:e413c111-d3fe-4f40-8bf5-629382dae8b6
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-09-26 上午 09:53:58
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.WebCore.Common.Seo;

namespace XiHan.ConsoleTest.Common;

/// <summary>
/// SeoTest
/// </summary>
public static class SeoTest
{
    /// <summary>
    /// 生成 SiteMap
    /// </summary>
    /// <returns></returns>
    public static void SeoSiteMap()
    {
        var result = SeoHelper.GenerateSiteMap();
        var ss = result;
    }
}