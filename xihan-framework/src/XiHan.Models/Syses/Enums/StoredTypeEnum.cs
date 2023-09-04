#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:StoredTypeEnum
// Guid:9a9c7869-6f2b-4744-ab3f-c6f8a31f8b60
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-20 下午 03:26:17
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace XiHan.Models.Syses.Enums;

/// <summary>
/// 存储类别
/// </summary>
public enum StoredTypeEnum
{
    /// <summary>
    /// 本地
    /// </summary>
    [Description("本地")] Local = 1,

    /// <summary>
    /// 远程
    /// </summary>
    [Description("远程")] Remote = 2,

    /// <summary>
    /// 阿里云
    /// </summary>
    [Description("阿里云")] AlibabaCloudOss = 3,

    /// <summary>
    /// 腾讯云
    /// </summary>
    [Description("腾讯云")] TencentCloudCos = 4,

    /// <summary>
    /// 七牛云
    /// </summary>
    [Description("七牛云")] QiniuCloudKodo = 5,

    /// <summary>
    /// 又拍云
    /// </summary>
    [Description("又拍云")] YoupaiCloudUss = 6,

    /// <summary>
    /// 华为云
    /// </summary>
    [Description("华为云")] HuaweiCloudObs = 7
}