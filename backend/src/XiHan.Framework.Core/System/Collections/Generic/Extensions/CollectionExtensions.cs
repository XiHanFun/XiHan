﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:CollectionExtensions
// Guid:898d6533-96e8-445d-aed8-5c04d707696f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/22 2:08:11
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using JetBrains.Annotations;
using XiHan.Framework.Core.Verification;

namespace XiHan.Framework.Core.System.Collections.Generic.Extensions;

/// <summary>
/// 集合扩展方法
/// </summary>
public static class CollectionExtensions
{
    /// <summary>
    /// 检查给定的集合对象是否为空或者没有任何项
    /// </summary>
    public static bool IsNullOrEmpty<T>(this ICollection<T>? source)
    {
        return source == null || source.Count <= 0;
    }

    /// <summary>
    /// 如果集合中尚未包含该项，则将其添加到集合中
    /// </summary>
    /// <param name="source">集合对象</param>
    /// <param name="item">要检查并添加的项</param>
    /// <typeparam name="T">集合中项的类型</typeparam>
    /// <returns>如果添加了项，则返回真(True)；如果没有添加（即项已存在）则返回假(False)</returns>
    public static bool AddIfNotContains<T>([NotNull] this ICollection<T> source, T item)
    {
        CheckHelper.NotNull(source, nameof(source));

        if (source.Contains(item))
        {
            return false;
        }

        source.Add(item);
        return true;
    }

    /// <summary>
    /// 向集合中添加尚未包含的项
    /// </summary>
    /// <param name="source">集合对象</param>
    /// <param name="items">要检查并添加的项的集合</param>
    /// <typeparam name="T">集合中项的类型</typeparam>
    /// <returns>返回添加的项的集合</returns>
    public static IEnumerable<T> AddIfNotContains<T>([NotNull] this ICollection<T> source, IEnumerable<T> items)
    {
        CheckHelper.NotNull(source, nameof(source));

        var addedItems = new List<T>();

        foreach (var item in items)
        {
            if (source.Contains(item))
            {
                continue;
            }

            source.Add(item);
            addedItems.Add(item);
        }

        return addedItems;
    }

    /// <summary>
    /// 如果集合中尚未包含满足给定谓词条件的项，则将项添加到集合中
    /// </summary>
    /// <param name="source">集合对象</param>
    /// <param name="predicate">决定项是否已存在于集合中的条件</param>
    /// <param name="itemFactory">返回项的工厂函数</param>
    /// <typeparam name="T">集合中项的类型</typeparam>
    /// <returns>如果添加了项，则返回真(True)；如果没有添加（即项已存在）则返回假(False)</returns>
    public static bool AddIfNotContains<T>([NotNull] this ICollection<T> source, [NotNull] Func<T, bool> predicate, [NotNull] Func<T> itemFactory)
    {
        CheckHelper.NotNull(source, nameof(source));
        CheckHelper.NotNull(predicate, nameof(predicate));
        CheckHelper.NotNull(itemFactory, nameof(itemFactory));

        if (source.Any(predicate))
        {
            return false;
        }

        source.Add(itemFactory());
        return true;
    }

    /// <summary>
    /// 移除集合中所有满足给定谓词条件的项
    /// </summary>
    /// <typeparam name="T">集合中项的类型</typeparam>
    /// <param name="source">集合对象</param>
    /// <param name="predicate">用于移除项的条件</param>
    /// <returns>被移除项的列表</returns>
    public static IList<T> RemoveAll<T>([NotNull] this ICollection<T> source, Func<T, bool> predicate)
    {
        var items = source.Where(predicate).ToList();

        foreach (var item in items)
        {
            source.Remove(item);
        }

        return items;
    }

    /// <summary>
    /// 从集合中移除所有指定的项
    /// </summary>
    /// <typeparam name="T">集合中项的类型</typeparam>
    /// <param name="source">集合对象</param>
    /// <param name="items">要移除的项的集合</param>
    public static void RemoveAll<T>([NotNull] this ICollection<T> source, IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            source.Remove(item);
        }
    }
}