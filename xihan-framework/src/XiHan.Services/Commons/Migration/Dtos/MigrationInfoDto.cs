#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:MigrationInfoDto
// Guid:b4cc52d4-6f72-4689-bf02-61f5fffb34c1
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-19 上午 04:23:12
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Services.Commons.Migration.Dtos;

/// <summary>
/// 单个资源迁移信息
/// </summary>
public class MigrationInfoDto
{
    /// <summary>
    /// 资源路径
    /// </summary>
    public string Path { get; set; } = string.Empty;

    /// <summary>
    /// 资源大小
    /// </summary>
    public string ResourceSize { get; set; } = string.Empty;

    /// <summary>
    /// 是否迁移成功
    /// </summary>
    public bool IsSucess { get; set; }
}