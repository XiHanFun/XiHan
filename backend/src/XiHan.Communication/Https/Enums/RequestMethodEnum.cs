#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:RequestMethodEnum
// Guid:308cddcd-8c7b-458f-9c89-df5cd71ac279
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 6:37:23
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace XiHan.Communication.Https.Enums;

/// <summary>
/// 网络请求方式
/// </summary>
public enum RequestMethodEnum
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