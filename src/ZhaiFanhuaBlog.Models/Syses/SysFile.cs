#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SysFile
// Guid:04d47255-762a-4dda-afe6-ad46a3b35f5f
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2022-11-21 下午 04:28:09
// ----------------------------------------------------------------​

#endregion <<版权版本注释>>

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases.Entity;

namespace ZhaiFanhuaBlog.Models.Syses;

/// <summary>
 /// 站点文件表
 /// </summary>
public class SysFile : BaseDeleteEntity<Guid>
{
    /// <summary>
    /// 文件原名
    ///</summary>
    [SugarColumn(Length = 100)]
    public string RealName { get; set; } = string.Empty;

    /// <summary>
    /// 文件类型
    ///</summary>
    [SugarColumn(Length = 50)]
    public string FileType { get; set; } = string.Empty;

    /// <summary>
    /// 存储名
    /// </summary>
    [SugarColumn(Length = 100)]
    public string StorageName { get; set; } = string.Empty;

    /// <summary>
    /// 存储地址
    /// 例如：/uploads/20221205/{GUID}
    /// </summary>
    [SugarColumn(Length = 200)]
    public string StorageUrl { get; set; } = string.Empty;

    /// <summary>
    /// 文件大小
    ///</summary>
    [SugarColumn(Length = 50)]
    public string FileSize { get; set; } = string.Empty;

    /// <summary>
    /// 文件扩展名
    /// </summary>
    [SugarColumn(Length = 20)]
    public string FileExt { get; set; } = string.Empty;

    /// <summary>
    /// 存储类型
    /// </summary>
    [SugarColumn(Length = 50)]
    public string StoreType { get; set; } = string.Empty;

    /// <summary>
    /// 存储位置
    /// 例如：/uploads
    /// </summary>
    [SugarColumn(Length = 50)]
    public string StorePath { get; set; } = string.Empty;

    /// <summary>
    /// 访问路径
    /// </summary>
    [SugarColumn(Length = 200)]
    public string AccessUrl { get; set; } = string.Empty;

    /// <summary>
    /// 字典描述
    /// </summary>
    [SugarColumn(Length = 50, IsNullable = true)]
    public string? Remark { get; set; }
}