#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:HttpRequestMethodEnum
// Guid:0082df6d-561a-454f-a354-83a2e3b6c337
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/1/18 20:47:47
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace XiHan.Common.Shared.Https.Enums;

/// <summary>
/// 网络请求方式枚举
/// </summary>
public enum HttpRequestMethodEnum
{
    /// <summary>
    /// 请求/响应指定资源的通信选项信息
    /// </summary>
    [Description("请求/响应指定资源的通信选项信息")]
    Options = 1,

    /// <summary>
    /// 请求满足条件头域的实体
    /// </summary>
    [Description("请求满足条件头域的实体")]
    Get = 2,

    /// <summary>
    /// 获取请求实体的元信息
    /// </summary>
    [Description("获取请求实体的元信息")]
    Head = 3,

    /// <summary>
    /// 请求源服务器接受请求中的实体作为请求资源的一个新的从属物
    /// </summary>
    [Description("请求源服务器接受请求中的实体作为请求资源的一个新的从属物")]
    Post = 4,

    /// <summary>
    /// 请求的实体头域用于资源的创建或修改
    /// </summary>
    [Description("请求的实体头域用于资源的创建或修改")]
    Put = 5,

    /// <summary>
    /// 请求源服务器删除请求指定的资源
    /// </summary>
    [Description("请求源服务器删除请求指定的资源")]
    Delete = 6,

    /// <summary>
    /// 更新资源的部分内容
    /// </summary>
    [Description("更新资源的部分内容")]
    Patch = 7,

    /// <summary>
    /// 动态切换到隧道的代理服务器
    /// </summary>
    [Description("动态切换到隧道的代理服务器")]
    Connect = 8,

    /// <summary>
    /// 激发一个远程的应用层的请求消息回路
    /// </summary>
    [Description("激发一个远程的应用层的请求消息回路")]
    Trace = 9
}