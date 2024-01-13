#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ResourceInfoDto
// Guid:2e40e341-0aed-463b-ac69-48ec33188896
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-06-03 下午 04:25:33
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHanCore.Services.Commons.Migration.Dtos;

/// <summary>
/// 资源信息
/// </summary>
public class ResourceInfoDto
{
    /// <summary>
    /// 资源路径
    /// </summary>
    public string Path { get; set; } = string.Empty;

    /// <summary>
    /// 旧资源前缀
    /// </summary>
    public string OldPrefix { get; set; } = string.Empty;

    /// <summary>
    /// 新资源前缀
    /// </summary>
    public string NewPrefix { get; set; } = string.Empty;
}