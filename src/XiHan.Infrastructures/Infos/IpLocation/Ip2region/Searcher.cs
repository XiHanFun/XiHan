using System.Net;
using System.Text;

namespace XiHan.Infrastructures.Infos.IpLocation.Ip2region;

/// <summary>
/// 查询器
/// </summary>
/// <remarks>powered by https://github.com/lionsoul2014/ip2region</remarks>
public class Searcher : ISearcher
{
    private const int HeaderInfoLength = 256;
    private const int VectorIndexRows = 256;
    private const int VectorIndexCols = 256;
    private const int VectorIndexSize = 8;
    private const int SegmentIndexSize = 14;

    private readonly byte[]? _vectorIndex;
    private readonly byte[]? _contentBuff;
    private readonly FileStream _contentStream;
    private readonly CachePolicyEnum _cachePolicy;

    /// <summary>
    /// IO数量
    /// </summary>
    public int IoCount { get; private set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="cachePolicy"></param>
    /// <param name="dbPath"></param>
    public Searcher(CachePolicyEnum cachePolicy = CachePolicyEnum.Content, string? dbPath = null)
    {
        if (string.IsNullOrEmpty(dbPath))
            dbPath = Path.Combine(AppContext.BaseDirectory, "IpDatabases", "ip2region.xdb");

        _contentStream = File.OpenRead(dbPath);
        _cachePolicy = cachePolicy;

        switch (_cachePolicy)
        {
            case CachePolicyEnum.Content:
                using (var stream = new MemoryStream())
                {
                    _contentStream.CopyTo(stream);
                    _contentBuff = stream.ToArray();
                }

                break;

            case CachePolicyEnum.VectorIndex:
                var vectorLength = VectorIndexRows * VectorIndexCols * VectorIndexSize;
                _vectorIndex = new byte[vectorLength];
                Read(HeaderInfoLength, _vectorIndex);
                break;

            default:
                using (var stream = new MemoryStream())
                {
                    _contentStream.CopyTo(stream);
                    _contentBuff = stream.ToArray();
                }

                break;
        }
    }

    /// <summary>
    /// 释放器
    /// </summary>
    ~Searcher()
    {
        _contentStream.Close();
        _contentStream.Dispose();
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="ipStr"></param>
    /// <returns></returns>
    public string Search(string ipStr)
    {
        var ip = Util.IpAddressToUInt32(ipStr);
        return Search(ip);
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="ipAddress"></param>
    /// <returns></returns>
    public string Search(IPAddress ipAddress)
    {
        var ip = Util.IpAddressToUInt32(ipAddress);
        return Search(ip);
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    public string Search(uint ip)
    {
        var result = string.Empty;
        var il0 = (ip >> 24) & 0xFF;
        var il1 = (ip >> 16) & 0xFF;
        var idx = il0 * VectorIndexCols * VectorIndexSize + il1 * VectorIndexSize;
        uint sPtr;
        uint ePtr;
        switch (_cachePolicy)
        {
            case CachePolicyEnum.VectorIndex:
                sPtr = BitConverter.ToUInt32(_vectorIndex.AsSpan()[(int)idx..]);
                ePtr = BitConverter.ToUInt32(_vectorIndex.AsSpan()[((int)idx + 4)..]);
                break;

            case CachePolicyEnum.Content:
                sPtr = BitConverter.ToUInt32(_contentBuff.AsSpan()[(HeaderInfoLength + (int)idx)..]);
                ePtr = BitConverter.ToUInt32(_contentBuff.AsSpan()[(HeaderInfoLength + (int)idx + 4)..]);
                break;

            case CachePolicyEnum.File:
                var buff = new byte[VectorIndexSize];
                Read((int)(idx + HeaderInfoLength), buff);
                sPtr = BitConverter.ToUInt32(buff);
                ePtr = BitConverter.ToUInt32(buff.AsSpan()[4..]);
                break;

            default:
                sPtr = BitConverter.ToUInt32(_contentBuff.AsSpan()[(HeaderInfoLength + (int)idx)..]);
                ePtr = BitConverter.ToUInt32(_contentBuff.AsSpan()[(HeaderInfoLength + (int)idx + 4)..]);
                break;
        }

        var dataLen = 0;
        uint dataPtr = 0;
        var l = 0;
        var h = (int)((ePtr - sPtr) / SegmentIndexSize);
        var buffer = new byte[SegmentIndexSize];

        while (l <= h)
        {
            var mid = Util.GetMidIp(l, h);
            var pos = sPtr + mid * SegmentIndexSize;

            Read((int)pos, buffer);
            var sip = BitConverter.ToUInt32(buffer);

            if (ip < sip)
            {
                h = mid - 1;
            }
            else
            {
                var eip = BitConverter.ToUInt32(buffer.AsSpan()[4..]);
                if (ip > eip)
                {
                    l = mid + 1;
                }
                else
                {
                    dataLen = BitConverter.ToUInt16(buffer.AsSpan()[8..]);
                    dataPtr = BitConverter.ToUInt32(buffer.AsSpan()[10..]);
                    break;
                }
            }
        }

        if (dataLen == 0) return result;

        var regionBuff = new byte[dataLen];
        Read((int)dataPtr, regionBuff);
        result = Encoding.UTF8.GetString(regionBuff);
        return result;
    }

    /// <summary>
    /// 读
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="buff"></param>
    /// <exception cref="IOException"></exception>
    private void Read(int offset, byte[] buff)
    {
        switch (_cachePolicy)
        {
            case CachePolicyEnum.Content:
                _contentBuff.AsSpan()[offset..(offset + buff.Length)].CopyTo(buff);
                break;

            default:
                _contentStream.Seek(offset, SeekOrigin.Begin);
                IoCount++;

                var rLen = _contentStream.Read(buff);
                if (rLen != buff.Length) throw new IOException($"incomplete read: read bytes should be {buff.Length}");

                break;
        }
    }
}