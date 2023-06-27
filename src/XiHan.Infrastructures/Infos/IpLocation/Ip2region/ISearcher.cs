using System.Net;

namespace XiHan.Infrastructures.Infos.IpLocation.Ip2region;

/// <summary>
/// ISearcher
/// </summary>
/// <remarks>powered by https://github.com/lionsoul2014/ip2region</remarks>
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
    /// IO总数
    /// </summary>
    int IoCount { get; }
}