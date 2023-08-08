#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IdHelper
// Guid:3944bbce-a2d1-4ddf-9880-122330a2739f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/7/12 23:27:33
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

namespace XiHan.Utils.IdGenerator;

/// <summary>
/// 这是一个调用的例子，默认情况下，单机集成者可以直接使用 NextId()。
/// </summary>
/// <remarks>
/// 第1步，全局 初始化(应用程序启动时执行一次)：
/// <code>
/// // 创建 IdGeneratorOptions 对象，可在构造函数中输入 WorkerId：
/// var options = new IdGeneratorOptions((ushort)Thread.CurrentThread.ManagedThreadId);
/// // 默认值6，限定 WorkerId 最大值为2^6-1，即默认最多支持64个节点。
/// // options.WorkerIdBitLength = 10;
/// // 默认值6，限制每毫秒生成的ID个数。若生成速度超过5万个/秒，建议加大 SeqBitLength 到 10。
/// // options.SeqBitLength = 6;
/// // 如果要兼容老系统的雪花算法，此处应设置为老系统的BaseTime。
/// // options.BaseTime = Your_Base_Time;
/// // ...... 其它参数参考 IdGeneratorOptions 定义。
///
/// // 保存参数(务必调用，否则参数设置不生效)：
/// IdHelper.SetIdGenerator(options);
///
/// // 以上过程只需全局一次，且应在生成ID之前完成。
/// </code>
/// 第2步，生成ID：
/// <code>
/// // 初始化后，在任何需要生成ID的地方，调用以下方法：
/// var newId = IdHelper.NextId();
/// </code>
/// </remarks>
public class IdHelper
{
    /// <summary>
    /// Id生成实例
    /// </summary>
    public static IIdGenerator? IdGenInstance { get; private set; } = null;

    /// <summary>
    /// 设置参数，建议程序初始化时执行一次
    /// </summary>
    /// <param name="options"></param>
    public static void SetIdGenerator(IdGeneratorOptions options)
    {
        IdGenInstance = new DefaultIdGenerator(options);
    }

    /// <summary>
    /// 生成新的Id
    /// 调用本方法前，请确保调用了 SetIdGenerator 方法做初始化。
    /// </summary>
    /// <returns></returns>
    public static long NextId()
    {
        if (IdGenInstance == null) throw new ArgumentException("Please initialize Yitter.IdGeneratorOptions first.");

        return IdGenInstance.NewLong();
    }
}