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
using System.ComponentModel;
using XiHan.Models.Bases.Entity;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统文件对象存储配置
/// Operation Support Systems
/// </summary>
[SugarTable(TableName = "Sys_OSS")]
public class SysFileOSS : BaseDeleteEntity
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

/// <summary>
/// 存储类别
/// </summary>
public enum StoredTypeEnum
{
    /// <summary>
    /// 本地
    /// </summary>
    [Description("本地")]
    Local = 1,

    /// <summary>
    /// 远程
    /// </summary>
    [Description("远程")]
    Remote = 2,

    /// <summary>
    /// 阿里云
    /// </summary>
    [Description("阿里云")]
    AlibabaCloudOss = 3,

    /// <summary>
    /// 腾讯云
    /// </summary>
    [Description("腾讯云")]
    TencentCloudCos = 4,

    /// <summary>
    /// 七牛云
    /// </summary>
    [Description("七牛云")]
    QiniuCloudKodo = 5,

    /// <summary>
    /// 又拍云
    /// </summary>
    [Description("又拍云")]
    YoupaiCloudUss = 6,

    /// <summary>
    /// 华为云
    /// </summary>
    [Description("华为云")]
    HuaweiCloudObs = 7,
}