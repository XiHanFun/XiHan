#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysFile
// Guid:04d47255-762a-4dda-afe6-ad46a3b35f5f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-11-21 下午 04:28:09
// ----------------------------------------------------------------​

#endregion <<版权版本注释>>

using SqlSugar;
using XiHanCore.Models.Bases.Attributes;
using XiHanCore.Models.Bases.Entities;
using XiHanCore.Models.Syses.Enums;

namespace XiHanCore.Models.Syses;

/// <summary>
/// 系统文件表
/// </summary>
/// <remarks>记录新增，修改，删除信息</remarks>
[SugarTable, SystemTable]
public class SysFile : BaseDeleteEntity
{
    /// <summary>
    /// 文件原名
    ///</summary>
    [SugarColumn(Length = 64)]
    public string RealName { get; set; } = string.Empty;

    /// <summary>
    /// 文件类型
    ///</summary>
    [SugarColumn(Length = 16)]
    public string FileType { get; set; } = string.Empty;

    /// <summary>
    /// 存储名
    /// </summary>
    [SugarColumn(Length = 64)]
    public string StorageName { get; set; } = string.Empty;

    /// <summary>
    /// 存储地址
    /// 例如：/uploads/20221205/{GUID}
    /// </summary>
    [SugarColumn(Length = 256)]
    public string StorageUrl { get; set; } = string.Empty;

    /// <summary>
    /// 文件大小
    ///</summary>
    [SugarColumn(Length = 64)]
    public string FileSize { get; set; } = string.Empty;

    /// <summary>
    /// 文件扩展名
    /// </summary>
    [SugarColumn(Length = 16)]
    public string FileExtension { get; set; } = string.Empty;

    /// <summary>
    /// 存储类型
    /// </summary>
    [SugarColumn]
    public StoredTypeEnum StoredType { get; set; }

    /// <summary>
    /// 存储位置
    /// 例如：/uploads
    /// </summary>
    [SugarColumn(Length = 256)]
    public string StorePath { get; set; } = string.Empty;

    /// <summary>
    /// 访问路径
    /// </summary>
    [SugarColumn(Length = 256)]
    public string AccessUrl { get; set; } = string.Empty;

    /// <summary>
    /// 文件描述
    /// </summary>
    [SugarColumn(Length = 256, IsNullable = true)]
    public string? Description { get; set; }
}