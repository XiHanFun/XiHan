#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:RequestBody
// Guid:135323af-2f5d-4919-95ca-62ed4cc04a4b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 6:33:42
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructure.Core.Apps.RequestOrResponse.Entities;

namespace XiHan.Infrastructure.Core.Apps.RequestOrResponse;

/// <summary>
/// 通用请求体
/// </summary>
public class RequestBody
{
    /// <summary>
    /// 是否查询所有数据
    /// 是则忽略分页信息，返回所有数据并绑定默认分页信息
    /// </summary>
    public bool? IsQueryAll { get; set; }

    /// <summary>
    /// 是否只返回分页信息
    /// 是则只返回分页信息，否则返回分页信息及结果数据
    /// </summary>
    public bool? IsOnlyPage { get; set; }

    /// <summary>
    /// 分页信息
    /// </summary>
    public PageInfo? PageInfo { get; set; }

    /// <summary>
    /// 选择条件集合
    /// </summary>
    public List<SelectCondition>? SelectConditions { get; set; }

    /// <summary>
    /// 排序条件集合
    /// </summary>
    public List<OrderCondition>? OrderConditions { get; set; }
}

/// <summary>
/// 通用请求体
/// </summary>
/// <typeparam name="T"></typeparam>
public class RequestBody<T> : RequestBody
{
    /// <summary>
    /// 数据传输对象
    /// </summary>
    public T? Dto { get; set; }
}