#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SiteFile
// Guid:04d47255-762a-4dda-afe6-ad46a3b35f5f
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2022-11-21 下午 04:28:09
// ----------------------------------------------------------------​

#endregion <<版权版本注释>>

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Sites;

/// <summary>
 /// 文件表
 /// </summary>
public class SiteFile : BaseDeleteEntity<Guid>
{
    /// <summary>
    /// 文件类型
    ///</summary>
    [SugarColumn(IsNullable = true)]
    public string? FileType { get; set; }

    /// <summary>
    /// 文件大小
    ///</summary>
    [SugarColumn(IsNullable = true)]
    public decimal? FileSize { get; set; }

    /// <summary>
    /// 文件名
    ///</summary>
    [SugarColumn(IsNullable = true)]
    public string? FileName { get; set; }

    /// <summary>
    /// 文件路径
    ///</summary>
    [SugarColumn(IsNullable = true)]
    public string? FilePath { get; set; }

    /// <summary>
    /// 排序字段
    ///</summary>
    [SugarColumn(IsNullable = true)]
    public int? SortOrder { get; set; }

    /// <summary>
    /// 字典描述
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(50)", IsNullable = true)]
    public string? Description { get; set; }
}