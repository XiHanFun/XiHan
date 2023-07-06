#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BaseIdDto
// Guid:b66012e2-97fb-4240-9c55-5f8a52a6aa73
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-06-15 上午 01:38:44
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.WebApi.Controllers.Bases;

/// <summary>
/// 主键基类
/// </summary>
public class BaseIdDto
{
    /// <summary>
    /// 主键标识
    /// </summary>
    public long BaseId { get; set; }
}