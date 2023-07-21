#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ISnowWorker
// Guid:e2469767-fafc-4b86-92f9-4710ae360b0f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/7/12 23:33:43
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

namespace XiHan.Utils.IdGenerator.Contract;

/// <summary>
/// ISnowWorker
/// </summary>
internal interface ISnowWorker
{
    long NextId();
}