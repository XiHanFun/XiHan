using System.Net;

namespace ZhaiFanhuaBlog.Utils.Ip.Ip2region;

/// <summary>
/// ��ѯ�ӿ�
/// powerd by https://github.com/lionsoul2014/ip2region
/// </summary>
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