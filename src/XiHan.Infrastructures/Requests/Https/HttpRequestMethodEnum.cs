#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:HttpRequestMethodEnum
// Guid:64c3fc29-d6a2-4c9b-a211-8cb6308f2642
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-06-14 下午 10:12:05
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace XiHan.Infrastructures.Requests.Https;

/// <summary>
/// 网络请求方式
/// </summary>
public enum HttpRequestMethodEnum
{
    /// <summary>
    /// 获取数据
    /// </summary>
    [Description("获取数据")]
    Get,

    /// <summary>
    /// 提交数据
    /// </summary>
    [Description("提交数据")]
    Post,

    /// <summary>
    /// 删除数据
    /// </summary>
    [Description("删除数据")]
    Delete,

    /// <summary>
    /// 更新数据
    /// </summary>
    [Description("更新数据")]
    Put,
}