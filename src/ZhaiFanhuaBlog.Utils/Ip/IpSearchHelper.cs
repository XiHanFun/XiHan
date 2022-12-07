#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IpSearchHelper
// Guid:c778b891-3c13-44f6-bcb4-01affc426934
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-12-07 下午 09:59:15
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using IP2Region.Net.XDB;
using ZhaiFanhuaBlog.Utils.Info;

namespace ZhaiFanhuaBlog.Utils.Ip;

/// <summary>
/// IpSearchHelper
/// </summary>
public static class IpSearchHelper
{
    /// <summary>
    /// 单一实例
    /// </summary>
    private static Searcher? _SearcherInstance = null;

    /// <summary>
    /// 锁
    /// </summary>
    private static readonly object _Lock = new();

    /// <summary>
    /// 访问器
    /// </summary>
    public static Searcher GetSearcher
    {
        get
        {
            lock (_Lock)
            {
                _SearcherInstance ??= new(CachePolicy.File, ApplicationInfoHelper.CurrentDirectory + @"ConfigData/ip2region.xdb");
            }
            return _SearcherInstance;
        }
    }

    /// <summary>
    /// 查询Ip
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    public static IpAddressModel? Search(string ip)
    {
        // 中国|0|浙江省|杭州市|电信
        string? modelStr = GetSearcher.Search(ip);
        var addressArray = modelStr?.Split('|');
        IpAddressModel model = new()
        {
            // Ip
            Ip = ip,
            // 国家 中国
            Country = addressArray?[0],
            // 省份/自治区/直辖市 贵州
            State = addressArray?[2],
            // 地级市 安顺
            PrefectureLevelCity = addressArray?[3],
            // 区/县 西秀区
            DistrictOrCounty = string.Empty,
            // 运营商 联通
            Operator = addressArray?[4],
            // 邮政编码 561000
            PostalCode = null,
            // 地区区号 0851
            AreaCode = null,
        };
        return model;
    }
}