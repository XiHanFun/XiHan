#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:EntityExtension
// Guid:4f70ee05-245b-435b-b83a-94f448dcf796
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-06-13 下午 08:56:48
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Http;
using XiHan.Utils.Extensions;

namespace XiHan.Infrastructures.Extensions;

/// <summary>
/// 实体拓展
/// </summary>
public static class EntityExtension
{
    /// <summary>
    /// 转化为新增实体
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public static TSource ToCreated<TSource>(this TSource source, HttpContext? context = null)
    {
        var propertyInfo = new PropertyInfo
        {
            HandleBy = "CreatedBy",
            HandleTime = "CreatedTime",
        };
        return source.CommonTo(propertyInfo, context);
    }

    /// <summary>
    /// 转化为更新实体
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public static TSource ToModified<TSource>(this TSource source, HttpContext? context = null)
    {
        var propertyInfo = new PropertyInfo
        {
            HandleBy = "ModifiedBy",
            HandleTime = "ModifiedTime",
        };
        return source.CommonTo(propertyInfo, context);
    }

    /// <summary>
    /// 转化为删除实体
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public static TSource ToDeleted<TSource>(this TSource source, HttpContext? context = null)
    {
        var propertyInfo = new PropertyInfo
        {
            IsHandle = "IsDeleted",
            HandleBy = "DeletedBy",
            HandleTime = "DeletedTime",
        };
        return source.CommonTo(propertyInfo, context);
    }

    /// <summary>
    /// 转化为审核实体
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public static TSource ToAudited<TSource>(this TSource source, HttpContext? context = null)
    {
        var propertyInfo = new PropertyInfo
        {
            HandleBy = "AuditedBy",
            HandleTime = "AuditedTime",
        };
        return source.CommonTo(propertyInfo, context);
    }

    #region 私有方法

    /// <summary>
    /// 转化为公共状态实体
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="propertyInfo"></param>
    /// <param name="context"></param>
    /// <param name=""></param>
    /// <returns></returns>
    private static TSource CommonTo<TSource>(this TSource source, PropertyInfo propertyInfo, HttpContext? context = null)
    {
        var types = source?.GetType();
        if (types == null) return source;

        if (propertyInfo.IsHandle.IsNotEmptyOrNull())
        {
            if (types.GetProperty(propertyInfo.IsHandle!) != null)
            {
                types.GetProperty(propertyInfo.IsHandle!)?.SetValue(source, true, null);
            }
        }
        if (propertyInfo.HandleBy.IsNotEmptyOrNull())
        {
            if (types.GetProperty(propertyInfo.HandleBy!) != null && context != null)
            {
                types.GetProperty(propertyInfo.HandleBy!)?.SetValue(source, context.GetUserId(), null);
            }
        }
        if (propertyInfo.HandleTime.IsNotEmptyOrNull())
        {
            if (types.GetProperty(propertyInfo.HandleTime!) != null)
            {
                types.GetProperty(propertyInfo.HandleTime!)?.SetValue(source, DateTime.Now, null);
            }
        }
        return source;
    }

    /// <summary>
    /// 属性信息
    /// </summary>
    private class PropertyInfo
    {
        /// <summary>
        /// 是否处理
        /// </summary>
        public string? IsHandle { get; set; }

        /// <summary>
        /// 处理人
        /// </summary>
        public string? HandleBy { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public string? HandleTime { get; set; }
    }

    #endregion
}