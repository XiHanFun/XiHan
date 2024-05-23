#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:PageExtend
// Guid:1ed47ffe-cfae-4800-a089-dd0327d00777
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 7:40:23
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Linq.Expressions;
using XiHan.Communication.Https.Entities;
using XiHan.Communication.Https.Enums;
using XiHan.Utils.Extensions.System;

namespace XiHan.Communication.Https.Extends;

/// <summary>
/// 分页扩展
/// </summary>
public static class PageExtend
{
    #region 条件扩展

    /// <summary>
    /// 扩展选择条件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="selectConditions"></param>
    /// <returns></returns>
    public static IQueryable<T> ToSelect<T>(this IQueryable<T> query, IEnumerable<SelectCondition> selectConditions)
    {
        var parser = new ExpressionParser<T>();
        var filter = parser.ParserConditions(selectConditions);
        return query.Where(filter);
    }

    /// <summary>
    /// 扩展排序条件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="orderConditions"></param>
    /// <returns></returns>
    public static IQueryable<T> ToOrder<T>(this IQueryable<T> query, IEnumerable<OrderCondition> orderConditions)
    {
        foreach (var orderCondition in orderConditions)
        {
            var parameter = Expression.Parameter(typeof(T));
            Expression propertySelector = parameter;

            //// 参数化字段
            //if (orderinfo.ParentFields != null && orderinfo.ParentFields.Count > 0)
            //{
            //    foreach (var parent in orderinfo.ParentFields)
            //    {
            //        propertySelector = Expression.Property(propertySelector, parent);
            //    }
            //    propertySelector = Expression.Property(propertySelector, orderinfo.Field);
            //}
            //else
            //{
            //    propertySelector = Expression.Property(propertySelector, orderinfo.Field);
            //}

            // 组合排序表达式
            if (propertySelector.Type == typeof(long))
            {
                var orderby = Expression.Lambda<Func<T, long>>(propertySelector, parameter);
                if (orderCondition.Condition == OrderConditionEnum.Desc)
                    query = query.OrderByDescending(orderby);
                else
                    query = query.OrderBy(orderby);
            }
            else if (propertySelector.Type == typeof(int))
            {
                var orderby = Expression.Lambda<Func<T, int>>(propertySelector, parameter);
                if (orderCondition.Condition == OrderConditionEnum.Desc)
                    query = query.OrderByDescending(orderby);
                else
                    query = query.OrderBy(orderby);
            }
            else if (propertySelector.Type == typeof(DateTime))
            {
                var orderby = Expression.Lambda<Func<T, DateTime>>(propertySelector, parameter);
                if (orderCondition.Condition == OrderConditionEnum.Desc)
                    query = query.OrderByDescending(orderby);
                else
                    query = query.OrderBy(orderby);
            }
            else if (propertySelector.Type == typeof(TimeSpan))
            {
                var orderby = Expression.Lambda<Func<T, TimeSpan>>(propertySelector, parameter);
                if (orderCondition.Condition == OrderConditionEnum.Desc)
                    query = query.OrderByDescending(orderby);
                else
                    query = query.OrderBy(orderby);
            }
            else
            {
                var orderby = Expression.Lambda<Func<T, object>>(propertySelector, parameter);
                if (orderCondition.Condition == OrderConditionEnum.Desc)
                    query = query.OrderByDescending(orderby);
                else
                    query = query.OrderBy(orderby);
            }
        }
        return query;
    }

    /// <summary>
    /// 扩展查询条件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="selectConditions"></param>
    /// <param name="orderConditions"></param>
    /// <returns></returns>
    public static IQueryable<T> ToQuery<T>(this IQueryable<T> query, IEnumerable<SelectCondition> selectConditions, IEnumerable<OrderCondition> orderConditions)
    {
        return query.ToSelect(selectConditions).ToOrder(orderConditions);
    }

    #endregion

    #region 数据分页

    /// <summary>
    /// 数据分页
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="page"></param>
    /// <param name="defaultFirstIndex"></param>
    /// <returns></returns>
    public static IQueryable<T> ToPage<T>(this IQueryable<T> query, PageInfo page, int defaultFirstIndex = 1)
        where T : class, new()
    {
        return query.Skip((page.CurrentIndex - defaultFirstIndex) * page.PageSize).Take(page.PageSize);
    }

    /// <summary>
    /// 数据选择分页
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="page"></param>
    /// <param name="selectConditions"></param>
    /// <returns></returns>
    public static IQueryable<T> ToPage<T>(this IQueryable<T> query, PageInfo page, IEnumerable<SelectCondition> selectConditions)
        where T : class, new()
    {
        return query.ToSelect(selectConditions).ToPage(page);
    }

    /// <summary>
    /// 数据排序分页
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="page"></param>
    /// <param name="orderConditions"></param>
    /// <returns></returns>
    public static IQueryable<T> ToPage<T>(this IQueryable<T> query, PageInfo page, IEnumerable<OrderCondition> orderConditions)
        where T : class, new()
    {
        return query.ToOrder(orderConditions).ToPage(page);
    }

    /// <summary>
    /// 数据查询分页
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="page"></param>
    /// <param name="selectConditions"></param>
    /// <param name="orderConditions"></param>
    /// <returns></returns>
    public static IQueryable<T> ToPage<T>(this IQueryable<T> query, PageInfo page, IEnumerable<SelectCondition> selectConditions, IEnumerable<OrderCondition> orderConditions)
        where T : class, new()
    {
        return query.ToQuery(selectConditions, orderConditions).ToPage(page);
    }

    #endregion

    #region 分页信息

    /// <summary>
    /// 响应分页信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="page"></param>
    /// <returns></returns>
    public static PageResponse ToPageResponse<T>(this IQueryable<T> query, PageInfo page)
        where T : class, new()
    {
        var datas = query.ToPage(page).ToList();
        return new PageResponse(page, datas.Count);
    }

    /// <summary>
    /// 响应分页信息和数据结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="page"></param>
    /// <returns></returns>
    public static PageResponse<T> ToPageResponseData<T>(this IQueryable<T> query, PageInfo page)
        where T : class, new()
    {
        var datas = query.ToPage(page).ToList();
        return new PageResponse<T>(page, datas);
    }

    /// <summary>
    /// 响应分页信息和全部数据结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static PageResponse<T> ToPageResponseAllData<T>(this IQueryable<T> query)
        where T : class, new()
    {
        var datas = query.ToList();
        var page = new PageInfo(currentIndex: 1, pageSize: datas.Count);
        return new PageResponse<T>(page, datas);
    }

    #endregion

    #region 包装标准结果

    /// <summary>
    /// 包装标准结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="requestBody"></param>
    /// <returns></returns>
    public static ResponseBody ToResponse<T>(this RequestBody requestBody)
        where T : class, new()
    {
        var query = new List<T>().AsQueryable();

        requestBody.IsQueryAll ??= requestBody.IsQueryAll.ParseToBool();
        requestBody.IsOnlyPage ??= requestBody.IsOnlyPage.ParseToBool();
        requestBody.PageInfo ??= new PageInfo();

        // 是否查询所有数据
        if (requestBody.IsQueryAll == true)
        {
            return ResponseBody.Success(query.ToPageResponseAllData());
        }

        // 查询数据条件
        if (requestBody.SelectConditions is not null && requestBody.OrderConditions is not null)
        {
            query = query.ToPage(requestBody.PageInfo, requestBody.SelectConditions, requestBody.OrderConditions);
        }
        else if (requestBody.SelectConditions is not null)
        {
            query = query.ToPage(requestBody.PageInfo, requestBody.SelectConditions);
        }
        else if (requestBody.OrderConditions is not null)
        {
            query = query.ToPage(requestBody.PageInfo, requestBody.OrderConditions);
        }

        // 是否只返回分页信息
        if (requestBody.IsOnlyPage == true)
        {
            return ResponseBody.Success(query.ToPageResponseData(requestBody.PageInfo));
        }
        else
        {
            return ResponseBody.Success(query.ToPageResponse(requestBody.PageInfo));
        }
    }

    #endregion
}