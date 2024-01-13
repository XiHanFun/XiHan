#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SeoHelper
// Guid:86b1f94a-cf9b-4c2a-857a-8bb0d8702c19
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-09-26 上午 09:37:52
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;
using System.Xml.Serialization;
using XiHanCore.Utils.Extensions;

namespace XiHanCore.WebCore.Common.Seo;

/// <summary>
/// 搜索引擎优化帮助类
/// </summary>
public static class SeoHelper
{
    /// <summary>
    /// 生成 SiteMap 文件
    /// </summary>
    public static string GenerateSiteMap()
    {
        // 创建 sitemap 对象
        var urlset = new UrlSet();

        // 添加 sitemap 条目
        urlset.Url.Add(new Url()
        {
            Loc = "https://example.com/page1.html",
            Lastmod = DateTime.UtcNow,
            Changefreq = ChangefreqEnum.Always.GetEnumDescriptionByKey(),
            Priority = 0.8
        });

        // 使用 XmlSerializer 将 C# 对象序列化为 XML 格式的字符串
        var serializer = new XmlSerializer(typeof(UrlSet));
        var stringWriter = new StringWriter();
        serializer.Serialize(stringWriter, urlset);
        return stringWriter.ToString();
    }
}

/// <summary>
/// UrlSet
/// </summary>
[XmlType(TypeName = "urlset")]
public class UrlSet
{
    /// <summary>
    /// 命名空间
    /// </summary>
    [XmlElement("url")]
    public List<Url> Url { get; set; } = [];
}

/// <summary>
/// 具体某个链接
/// </summary>
[XmlType(TypeName = "url")]
public class Url
{
    /// <summary>
    /// 页面永久链接地址
    /// </summary>
    [XmlElement("loc")]
    public string Loc { get; set; } = string.Empty;

    /// <summary>
    /// 最后更新时间
    /// </summary>
    [XmlElement("lastmod")]
    public DateTime Lastmod { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// 更新频繁程度
    /// </summary>
    [XmlElement("changefreq")]
    public string Changefreq { get; set; } = string.Empty;

    /// <summary>
    /// 优先级，0-1
    /// </summary>
    [XmlElement("priority")]
    public double Priority { get; set; } = 1.00;
}

/// <summary>
/// 更改频率
/// </summary>
public enum ChangefreqEnum
{
    /// <summary>
    /// 经常
    /// </summary>
    [Description("always")] Always,

    /// <summary>
    /// 每时
    /// </summary>
    [Description("hourly")] Hourly,

    /// <summary>
    /// 每天
    /// </summary>
    [Description("daily")] Daily,

    /// <summary>
    /// 每周
    /// </summary>
    [Description("weekly")] Weekly,

    /// <summary>
    /// 每月
    /// </summary>
    [Description("monthly")] Monthly,

    /// <summary>
    /// 每年
    /// </summary>
    [Description("yearly")] Yearly,
}