#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:StoredTypeEnum
// Guid:39768536-06f6-41b1-ac7e-3e2932c456b8
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-12-24 上午 01:06:02
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace XiHan.Infrastructure.Enums;

/// <summary>
/// 文件存储类别
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