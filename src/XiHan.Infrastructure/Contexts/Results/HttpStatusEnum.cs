#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:HttpStatusEnum
// Guid:98283a15-6e72-4d21-ba71-f3bf6ed9a896
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-04-20 下午 03:20:21
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace XiHan.Infrastructure.Contexts.Results;

/// <summary>
/// 通用状态标识
/// </summary>
public enum HttpStatusEnum
{
    /// <summary>
    /// 成功
    /// </summary>
    [Description("成功")]
    Success = 1,

    /// <summary>
    /// 确认继续
    /// </summary>
    [Description("确认继续")]
    ConfirmIsContinue = 2,

    /// <summary>
    /// 错误
    /// </summary>
    [Description("错误")]
    Error = 3,

    /// <summary>
    /// 失败
    /// </summary>
    [Description("失败")]
    Fail = 4
}