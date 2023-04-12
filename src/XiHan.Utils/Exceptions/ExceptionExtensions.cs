#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:ExceptionExtensions
// Guid:25adaec8-dba5-4b9d-a48f-c508b0810a3a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-13 上午 04:00:09
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Utils.Exceptions;

/// <summary>
/// 异常拓展类(polly拓展)
/// </summary>
public static class ExceptionExtensions
{
    /// <summary>
    /// 熔断策略
    /// </summary>
    public static void GetBreakPolicy<TResult>() where TResult : Exception
    {
    }
}