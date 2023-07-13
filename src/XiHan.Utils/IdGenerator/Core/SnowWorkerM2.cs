#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SnowWorkerM2
// Guid:2029a393-8db6-48b7-bc72-d3b47f3d86ee
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/7/12 23:31:35
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

namespace XiHan.Utils.IdGenerator.Core;

/// <summary>
/// 常规雪花算法
/// </summary>
internal class SnowWorkerM2 : SnowWorkerM1
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="options"></param>
    public SnowWorkerM2(IdGeneratorOptions options) : base(options)
    {
    }

    public override long NextId()
    {
        lock (_SyncLock)
        {
            long currentTimeTick = GetCurrentTimeTick();

            if (_LastTimeTick == currentTimeTick)
            {
                if (_CurrentSeqNumber++ > MaxSeqNumber)
                {
                    _CurrentSeqNumber = MinSeqNumber;
                    currentTimeTick = GetNextTimeTick();
                }
            }
            else
            {
                _CurrentSeqNumber = MinSeqNumber;
            }

            if (currentTimeTick < _LastTimeTick)
            {
                throw new Exception(string.Format("Time error for {0} milliseconds", _LastTimeTick - currentTimeTick));
            }

            _LastTimeTick = currentTimeTick;
            var result = ((currentTimeTick << _TimestampShift) + ((long)WorkerId << SeqBitLength) + (uint)_CurrentSeqNumber);

            return result;
        }
    }
}