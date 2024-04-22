#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IExceptionWithSelfLogging
// Guid:8148af5b-5073-426a-bfe6-64621c68ea14
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/23 1:18:25
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.Logging;

namespace XiHan.Core.Microsoft.Extensions.Logging;

/// <summary>
/// 自身日志扩展
/// </summary>
public interface IExceptionWithSelfLogging
{
    /// <summary>
    /// 记录日志
    /// </summary>
    /// <param name="logger"></param>
    void Log(ILogger logger);
}