#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:SysFileOSS
// Guid:b19f2f8c-3940-4e6d-b57d-4c63bd8a1759
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-19 上午 03:07:02
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Entity;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统文件对象存储配置
/// </summary>
/// <remarks>记录创建，修改信息</remarks>
[SugarTable(TableName = "Sys_File_Oss")]
public class SysFileOss : BaseModifyEntity
{
    /// <summary>
    /// 存储类型
    /// StoredTypeEnum
    /// </summary>
    [SugarColumn(Length = 20)]
    public string StoredType { get; set; } = string.Empty;

    /// <summary>
    /// 机密Id
    /// </summary>
    [SugarColumn(Length = 100)]
    public string SecretId { get; set; } = string.Empty;

    /// <summary>
    /// 机密密匙
    /// </summary>
    [SugarColumn(Length = 100)]
    public string SecretKey { get; set; } = string.Empty;

    /// <summary>
    /// 是否可用
    /// </summary>
    public bool IsEnabled { get; set; }
}