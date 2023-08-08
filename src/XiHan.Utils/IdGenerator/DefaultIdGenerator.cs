#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:DefaultIdGenerator
// Guid:81809d4c-fb87-447f-8bd8-a8e46841b6af
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/7/12 23:28:47
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

/*
 * 版权属于：yitter(yitter@126.com)
 * 开源地址：https://github.com/yitter/idgenerator
 * 版权协议：MIT
 * 版权说明：只要保留本版权，你可以免费使用、修改、分发本代码。
 * 免责条款：任何因为本代码产生的系统、法律、政治、宗教问题，均与版权所有者无关。
 *
 */

using XiHan.Utils.IdGenerator.Contract;
using XiHan.Utils.IdGenerator.Core;

namespace XiHan.Utils.IdGenerator;

/// <summary>
/// 默认实现
/// </summary>
public class DefaultIdGenerator : IIdGenerator
{
    private ISnowWorker SnowWorker { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="options"></param>
    /// <exception cref="ArgumentException"></exception>
    public DefaultIdGenerator(IdGeneratorOptions options)
    {
        if (options == null)
        {
            throw new ArgumentException("options error.");
        }

        // 1.BaseTime
        if (options.BaseTime < DateTime.Now.AddYears(-50) || options.BaseTime > DateTime.Now)
        {
            throw new ArgumentException("BaseTime error.");
        }

        // 2.WorkerIdBitLength
        var maxLength = options.TimestampType == 0 ? 22 : 31; // (秒级时间戳时放大到31位)
        if (options.WorkerIdBitLength <= 0)
        {
            throw new ArgumentException("WorkerIdBitLength error.(range:[1, 21])");
        }

        if (options.DataCenterIdBitLength + options.WorkerIdBitLength + options.SeqBitLength > maxLength)
        {
            throw new ArgumentException("error：DataCenterIdBitLength + WorkerIdBitLength + SeqBitLength <= " + maxLength);
        }

        // 3.WorkerId & DataCenterId
        var maxWorkerIdNumber = (1 << options.WorkerIdBitLength) - 1;
        if (maxWorkerIdNumber == 0)
        {
            maxWorkerIdNumber = 63;
        }

        if (options.WorkerId < 0 || options.WorkerId > maxWorkerIdNumber)
        {
            throw new ArgumentException("WorkerId error. (range:[0, " + maxWorkerIdNumber + "]");
        }

        var maxDataCenterIdNumber = (1 << options.DataCenterIdBitLength) - 1;
        if (options.DataCenterId < 0 || options.DataCenterId > maxDataCenterIdNumber)
        {
            throw new ArgumentException("DataCenterId error. (range:[0, " + maxDataCenterIdNumber + "]");
        }

        // 4.SeqBitLength
        if (options.SeqBitLength < 2 || options.SeqBitLength > 21)
        {
            throw new ArgumentException("SeqBitLength error. (range:[2, 21])");
        }

        // 5.MaxSeqNumber
        var maxSeqNumber = (1 << options.SeqBitLength) - 1;
        if (maxSeqNumber == 0)
        {
            maxSeqNumber = 63;
        }

        if (options.MaxSeqNumber < 0 || options.MaxSeqNumber > maxSeqNumber)
        {
            throw new ArgumentException("MaxSeqNumber error. (range:[1, " + maxSeqNumber + "]");
        }

        // 6.MinSeqNumber
        if (options.MinSeqNumber < 5 || options.MinSeqNumber > maxSeqNumber)
        {
            throw new ArgumentException("MinSeqNumber error. (range:[5, " + maxSeqNumber + "]");
        }

        // 7.TopOverCostCount
        if (options.TopOverCostCount < 0 || options.TopOverCostCount > 10000)
        {
            throw new ArgumentException("TopOverCostCount error. (range:[0, 10000]");
        }

        switch (options.Method)
        {
            case 2:
                SnowWorker = new SnowWorkerM2(options);
                break;

            default:
                SnowWorker = options is { DataCenterIdBitLength: 0, TimestampType: 0 } ? new SnowWorkerM1(options) : new SnowWorkerM3(options);

                break;
        }

        if (options.Method != 2)
        {
            Thread.Sleep(500);
        }
    }

    /// <summary>
    /// 下一个Id
    /// </summary>
    /// <returns></returns>
    public long NewLong()
    {
        return SnowWorker.NextId();
    }
}