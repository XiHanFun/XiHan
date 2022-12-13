using System.Net;

namespace ZhaiFanhuaBlog.Utils.Ip.Ip2region;

/// <summary>
/// 查询接口
/// powerd by https://github.com/lionsoul2014/ip2region
/// </summary>
public interface ISearcher
{
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="ipStr"></param>
    /// <returns></returns>
    string? Search(string ipStr);

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="ipAddress"></param>
    /// <returns></returns>
    string? Search(IPAddress ipAddress);

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="ipAddress"></param>
    /// <returns></returns>
    string? Search(uint ipAddress);

    /// <summary>
    /// IO数量
    /// </summary>
    int IoCount { get; }
}