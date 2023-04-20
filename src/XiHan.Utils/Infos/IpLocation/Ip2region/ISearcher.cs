using System.Net;

namespace XiHan.Utils.Infos.IpLocation.Ip2region;

/// <summary>
/// ��ѯ�ӿ�
/// </summary>
/// <remarks>powerd by https://github.com/lionsoul2014/ip2region</remarks>
public interface ISearcher
{
    /// <summary>
    /// ��ѯ
    /// </summary>
    /// <param name="ipStr"></param>
    /// <returns></returns>
    string? Search(string ipStr);

    /// <summary>
    /// ��ѯ
    /// </summary>
    /// <param name="ipAddress"></param>
    /// <returns></returns>
    string? Search(IPAddress ipAddress);

    /// <summary>
    /// ��ѯ
    /// </summary>
    /// <param name="ipAddress"></param>
    /// <returns></returns>
    string? Search(uint ipAddress);

    /// <summary>
    /// IO����
    /// </summary>
    int IoCount { get; }
}