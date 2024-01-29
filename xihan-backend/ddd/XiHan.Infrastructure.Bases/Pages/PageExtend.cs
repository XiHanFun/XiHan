#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:PageExtend
// Guid:a345ade2-5c23-474d-b6b5-ea29490d57b0
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-09-02 上午 01:03:21
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Linq.Expressions;
using XiHan.Infrastructure.Bases.Pages.Dtos;
using XiHan.Infrastructure.Bases.Pages.Enums;
using XiHan.Infrastructure.Bases.Pages.Expressions;
using XiHan.Infrastructure.Bases.Responses;

namespace XiHan.Infrastructure.Bases.Pages;

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
    /// <param name="conditions"></param>
    /// <returns></returns>
    public static IQueryable<T> ToSelect<T>(this IQueryable<T> query, IEnumerable<SelectConditionDto> selectConditions)
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
    public static IQueryable<T> ToOrder<T>(this IQueryable<T> query, IEnumerable<OrderConditionDto> orderConditions)
    {
        foreach (var orderinfo in orderConditions)
        {
            var parameter = Expression.Parameter(typeof(T));
            Expression propertySelector = parameter;

            // 参数化字段
            if (orderinfo.ParentFields != null && orderinfo.ParentFields.Count > 0)
            {
                foreach (var parent in orderinfo.ParentFields)
                {
                    propertySelector = Expression.Property(propertySelector, parent);
                }
                propertySelector = Expression.Property(propertySelector, orderinfo.Field);
            }
            else
            {
                propertySelector = Expression.Property(propertySelector, orderinfo.Field);
            }

            // 组合排序表达式
            if (propertySelector.Type == typeof(long))
            {
                var orderby = Expression.Lambda<Func<T, long>>(propertySelector, parameter);
                if (orderinfo.OrderCondition == OrderConditionEnum.Desc)
                    query = query.OrderByDescending(orderby);
                else
                    query = query.OrderBy(orderby);
            }
            else if (propertySelector.Type == typeof(int))
            {
                var orderby = Expression.Lambda<Func<T, int>>(propertySelector, parameter);
                if (orderinfo.OrderCondition == OrderConditionEnum.Desc)
                    query = query.OrderByDescending(orderby);
                else
                    query = query.OrderBy(orderby);
            }
            else if (propertySelector.Type == typeof(DateTime))
            {
                var orderby = Expression.Lambda<Func<T, DateTime>>(propertySelector, parameter);
                if (orderinfo.OrderCondition == OrderConditionEnum.Desc)
                    query = query.OrderByDescending(orderby);
                else
                    query = query.OrderBy(orderby);
            }
            else if (propertySelector.Type == typeof(TimeSpan))
            {
                var orderby = Expression.Lambda<Func<T, TimeSpan>>(propertySelector, parameter);
                if (orderinfo.OrderCondition == OrderConditionEnum.Desc)
                    query = query.OrderByDescending(orderby);
                else
                    query = query.OrderBy(orderby);
            }
            else
            {
                var orderby = Expression.Lambda<Func<T, object>>(propertySelector, parameter);
                if (orderinfo.OrderCondition == OrderConditionEnum.Desc)
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
    public static IQueryable<T> ToQuery<T>(this IQueryable<T> query, IEnumerable<SelectConditionDto> selectConditions, IEnumerable<OrderConditionDto> orderConditions)
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
    public static IQueryable<T> ToPage<T>(this IQueryable<T> query, PageInfoDto page, int defaultFirstIndex = 1) where T : class, new()
    {
        return query.Skip((page.CurrentIndex - defaultFirstIndex) * page.PageSize).Take(page.PageSize);
    }

    #endregion

    #region 空数据分页结果

    /// <summary>
    /// 空数据分页结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="page"></param>
    /// <returns></returns>
    public static PageResponseDto ToPageResponse<T>(this IQueryable<T> query, PageInfoDto page) where T : class, new()
    {
        return new PageResponseDto(page, query.Count());
    }

    #endregion

    #region 有数据分页结果

    /// <summary>
    /// 有数据分页结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="page"></param>
    /// <param name="defaultFirstIndex"></param>
    /// <returns></returns>
    public static PageResponseDataDto<T> ToPageResponseData<T>(this IQueryable<T> query, PageInfoDto page) where T : class, new()
    {
        return new PageResponseDataDto<T>(page, query.Count(), query.ToPage(page));
    }

    #endregion

    #region 全部数据分页结果

    /// <summary>
    /// 全部数据分页结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static PageResponseDataDto<T> ToPageResponseAllData<T>(this IQueryable<T> query) where T : class, new()
    {
        return new PageResponseDataDto<T>(new PageResponseDto(new PageInfoDto
        {
            CurrentIndex = 1,
            PageSize = query.Count()
        }, query.Count()), query);
    }

    #endregion

    /// <summary>
    /// 包装标准结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="pageQuery"></param>
    /// <returns></returns>
    public static ResponseResult ToResponse<T>(this PageQueryDto pageQuery) where T : class, new()
    {
        var query = new List<T>().AsQueryable();

        if (pageQuery.IsQueryAll)
        {
            return ResponseResult.Success(query.ToPageResponseAllData());
        }
        else
        {
            var result = query.ToQuery(pageQuery.SelectConditions, pageQuery.OrderConditions);

            if (result.ToList().Count != 0)
            {
                return ResponseResult.Success(result.ToPage(pageQuery.PageInfo).ToPageResponseData(pageQuery.PageInfo));
            }
            else
            {
                return ResponseResult.Success(result.ToPage(pageQuery.PageInfo).ToPageResponse(pageQuery.PageInfo));
            }
        }
    }
}