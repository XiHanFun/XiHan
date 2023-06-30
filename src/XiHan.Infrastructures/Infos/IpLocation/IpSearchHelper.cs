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

using XiHan.Infrastructures.Apps.HttpContexts;
using XiHan.Infrastructures.Infos.IpLocation.Ip2region;
using XiHan.Utils.Extensions;

namespace XiHan.Infrastructures.Infos.IpLocation;

/// <summary>
/// Ip 查询帮助类
/// </summary>
public static class IpSearchHelper
{
    /// <summary>
    /// 锁
    /// </summary>
    private static readonly object Lock = new();

    /// <summary>
    /// 数据库位置
    /// </summary>
    public static string IpDbPath { get; set; } = string.Empty;

    /// <summary>
    /// 单一实例
    /// </summary>
    private static Searcher? _searcherInstance;

    /// <summary>
    /// 访问器
    /// </summary>
    private static Searcher GetSearcher
    {
        get
        {
            lock (Lock)
            {
                _searcherInstance ??= new Searcher(CachePolicyEnum.VectorIndex, IpDbPath);
            }

            return _searcherInstance;
        }
    }

    /// <summary>
    /// 查询 Ip
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    public static UserAddressInfo? Search(string ip)
    {
        try
        {
            // 中国|0|浙江省|杭州市|电信
            var modelStr = GetSearcher.Search(ip);
            if (modelStr.IsEmptyOrNull()) return null;
            string[] addressArray = modelStr.Replace('0', '-').Split('|');
            UserAddressInfo addressInfo = new()
            {
                // 国家 中国
                Country = addressArray[0],
                // 省份/自治区/直辖市 浙江省
                State = addressArray[2],
                // 地级市 安顺
                PrefectureLevelCity = addressArray[3],
                // 区/县 西秀区
                DistrictOrCounty = null,
                // 运营商 联通
                Operator = addressArray[4],
                // 邮政编码 561000
                PostalCode = null,
                // 地区区号 0851
                AreaCode = null
            };
            return addressInfo;
        }
        catch (Exception ex)
        {
            ex.ThrowAndConsoleError("Ip地址信息查询出错");
            return null;
        }
    }
}