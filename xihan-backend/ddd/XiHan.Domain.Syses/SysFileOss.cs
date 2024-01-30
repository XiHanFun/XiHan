#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysFileOSS
// Guid:b19f2f8c-3940-4e6d-b57d-4c63bd8a1759
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-19 上午 03:07:02
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Common.Shared.Attributes;
using XiHan.Domain.Bases.Entities;
using XiHan.Domain.Syses.Enums;

namespace XiHan.Domain.Syses;

/// <summary>
/// 系统文件对象存储配置表
/// </summary>
/// <remarks>记录新增，修改信息</remarks>
[SugarTable, SystemTable]
public class SysFileOss : BaseModifiedEntity
{
    /// <summary>
    /// 存储标题
    /// </summary>
    [SugarColumn(Length = 256)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 存储类型
    /// </summary>
    [SugarColumn]
    public StoredTypeEnum StoredType { get; set; }

    /// <summary>
    /// 机密Id
    /// </summary>
    [SugarColumn(Length = 128)]
    public string SecretId { get; set; } = string.Empty;

    /// <summary>
    /// 机密密匙
    /// </summary>
    [SugarColumn(Length = 128)]
    public string SecretKey { get; set; } = string.Empty;

    /// <summary>
    /// 是否可用
    /// </summary>
    [SugarColumn]
    public bool IsEnabled { get; set; }
}