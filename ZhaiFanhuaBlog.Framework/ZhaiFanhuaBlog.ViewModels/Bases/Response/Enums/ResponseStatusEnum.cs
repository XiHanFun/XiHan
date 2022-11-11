#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ResponseStatusEnum
// Guid:61eebc1f-97fa-4966-8d17-d69dabf079f2
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-17 下午 09:28:34
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace ZhaiFanhuaBlog.ViewModels.Response.Enum;

/// <summary>
/// 通用结果状态
/// </summary>
public enum ResponseStatusEnum
{
    /// <summary>
    /// 失败
    /// </summary>
    Fail = 0,

    /// <summary>
    /// 成功
    /// </summary>
    Success = 1,

    /// <summary>
    /// 确认继续
    /// </summary>
    ConfirmIsContinue = 2,

    /// <summary>
    /// 错误
    /// </summary>
    Error = 3
}