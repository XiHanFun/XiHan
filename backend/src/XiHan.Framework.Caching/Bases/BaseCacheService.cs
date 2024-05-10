#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BaseCacheService
// Guid:c06dba51-db4f-463f-b81b-176346d51631
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/29 4:20:00
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.Caching.Memory;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;
using XiHan.Framework.Core.System.Text.Json.Serialization;

namespace XiHan.Framework.Caching.Bases;

/// <summary>
/// 内存缓存操作
/// </summary>
public class BaseCacheService : IBaseCacheService
{
    /// <summary>
    /// 默认缓存配置
    /// </summary>
    private readonly IMemoryCache _cache;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="memoryCache"></param>
    public BaseCacheService(IMemoryCache memoryCache)
    {
        _cache = memoryCache;
    }

    #region 验证缓存项是否存在

    /// <summary>
    /// 验证缓存项是否存在,TryGetValue 来检测 Key 是否存在
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <returns></returns>
    public bool Exists(string key)
    {
        return key == null ? throw new ArgumentNullException(nameof(key)) : _cache.TryGetValue(key, out _);
    }

    #endregion

    #region 设置缓存

    /// <summary>
    /// 设置永久缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <param name="value">缓存Value</param>
    /// <returns></returns>
    public bool Set(string key, object value)
    {
        ArgumentNullException.ThrowIfNull(key);

        ArgumentNullException.ThrowIfNull(value);

        _ = _cache.Set(key, value.SerializeTo());
        return Exists(key);
    }

    /// <summary>
    /// 设置缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <param name="value">缓存Value</param>
    /// <param name="expiresSliding">滑动过期时长(如果在过期时间内有操作，则以当前时间点延长过期时间)</param>
    /// <param name="expiresAbsolute">绝对过期时长</param>
    /// <returns></returns>
    public bool Set(string key, object value, TimeSpan expiresSliding, TimeSpan expiresAbsolute)
    {
        ArgumentNullException.ThrowIfNull(key);

        ArgumentNullException.ThrowIfNull(value);

        _ = _cache.Set(key, value.SerializeTo(), new MemoryCacheEntryOptions()
            .SetSlidingExpiration(expiresSliding)
            .SetAbsoluteExpiration(expiresAbsolute));
        return Exists(key);
    }

    /// <summary>
    /// 设置缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <param name="value">缓存Value</param>
    /// <param name="expiresIn">缓存时长</param>
    /// <param name="isSliding">是否滑动过期(如果在过期时间内有操作，则以当前时间点延长过期时间)</param>
    /// <returns></returns>
    public bool Set(string key, object value, TimeSpan expiresIn, bool isSliding = false)
    {
        ArgumentNullException.ThrowIfNull(key);

        ArgumentNullException.ThrowIfNull(value);

        _ = _cache.Set(key, value.SerializeTo(), isSliding
            ? new MemoryCacheEntryOptions().SetSlidingExpiration(expiresIn)
            : new MemoryCacheEntryOptions().SetAbsoluteExpiration(expiresIn));
        return Exists(key);
    }

    /// <summary>
    /// 用键和值将某个缓存项插入缓存中，并指定基于时间的过期详细信息
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <param name="value">缓存Value</param>
    /// <param name="seconds">缓存时长，秒</param>
    public bool SetWithSeconds(string key, object value, int seconds)
    {
        ArgumentNullException.ThrowIfNull(key);

        ArgumentNullException.ThrowIfNull(value);

        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(seconds);

        _ = _cache.Set(key, value.SerializeTo(), DateTime.Now.AddSeconds(seconds));
        return Exists(key);
    }

    /// <summary>
    /// 用键和值将某个缓存项插入缓存中，并指定基于时间的过期详细信息
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <param name="value">缓存Value</param>
    /// <param name="minutes">缓存时长，分钟</param>
    public bool SetWithMinutes(string key, object value, int minutes)
    {
        ArgumentNullException.ThrowIfNull(key);

        ArgumentNullException.ThrowIfNull(value);

        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(minutes);

        _ = _cache.Set(key, value.SerializeTo(), DateTime.Now.AddMinutes(minutes));
        return Exists(key);
    }

    #endregion

    #region 删除缓存

    /// <summary>
    /// 删除缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <returns></returns>
    public bool Remove(string key)
    {
        ArgumentNullException.ThrowIfNull(key);

        _cache.Remove(key);
        return !Exists(key);
    }

    /// <summary>
    /// 批量删除缓存
    /// </summary>
    /// <param name="keys">缓存Key集合</param>
    /// <returns></returns>
    public void Remove(IEnumerable<string> keys)
    {
        ArgumentNullException.ThrowIfNull(keys);

        keys.ToList().ForEach(_cache.Remove);
    }

    /// <summary>
    /// 删除匹配到的缓存
    /// </summary>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public void RemoveByPattern(string pattern)
    {
        IEnumerable<string> l = GetMatch(pattern);
        foreach (var s in l) _ = Remove(s);
    }

    /// <summary>
    /// 删除所有缓存
    /// </summary>
    public void CleanAll()
    {
        List<string> l = GetKeys();
        foreach (var s in l) _ = Remove(s);
    }

    #endregion

    #region 修改缓存

    /// <summary>
    /// 修改缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <param name="value">新的缓存Value</param>
    /// <returns></returns>
    public bool Update(string key, object value)
    {
        return key == null
            ? throw new ArgumentNullException(nameof(key))
            : value == null
                ? throw new ArgumentNullException(nameof(value))
                : !Exists(key)
                    ? Set(key, value.SerializeTo())
                    : Remove(key) && Set(key, value.SerializeTo());
    }

    /// <summary>
    /// 修改缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <param name="value">新的缓存Value</param>
    /// <param name="expiresSliding">滑动过期时长(如果在过期时间内有操作，则以当前时间点延长过期时间)</param>
    /// <param name="expiresAbsolute">绝对过期时长</param>
    /// <returns></returns>
    public bool Update(string key, object value, TimeSpan expiresSliding, TimeSpan expiresAbsolute)
    {
        return key == null
            ? throw new ArgumentNullException(nameof(key))
            : value == null
                ? throw new ArgumentNullException(nameof(value))
                : !Exists(key)
                    ? Set(key, value.SerializeTo(), expiresSliding, expiresAbsolute)
                    : Remove(key) && Set(key, value.SerializeTo(), expiresSliding, expiresAbsolute);
    }

    /// <summary>
    /// 修改缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <param name="value">新的缓存Value</param>
    /// <param name="expiresIn">缓存时长</param>
    /// <param name="isSliding">是否滑动过期(如果在过期时间内有操作，则以当前时间点延长过期时间)</param>
    /// <returns></returns>
    public bool Update(string key, object value, TimeSpan expiresIn, bool isSliding = false)
    {
        return key == null
            ? throw new ArgumentNullException(nameof(key))
            : value == null
                ? throw new ArgumentNullException(nameof(value))
                : !Exists(key)
                    ? Set(key, value.SerializeTo(), expiresIn, isSliding)
                    : Remove(key) && Set(key, value.SerializeTo(), expiresIn, isSliding);
    }

    #endregion

    #region 获取缓存

    /// <summary>
    /// 获取缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <returns></returns>
    public T? Get<T>(string key) where T : class
    {
        return key == null ? throw new ArgumentNullException(nameof(key)) : _cache.Get(key) as T;
    }

    /// <summary>
    /// 获取缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <returns></returns>
    public object Get(string key)
    {
        return key == null ? throw new ArgumentNullException(nameof(key)) : _cache.Get(key) ?? new object();
    }

    /// <summary>
    /// 获取缓存集合
    /// </summary>
    /// <param name="keys">缓存Key集合</param>
    /// <returns></returns>
    public IDictionary<string, object?> Get(IEnumerable<string> keys)
    {
        ArgumentNullException.ThrowIfNull(keys);

        Dictionary<string, object?> dict = [];
        keys.ToList().ForEach(item => dict.Add(item, _cache.Get(item)));
        return dict;
    }

    /// <summary>
    /// 搜索匹配缓存
    ///</summary>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public IEnumerable<string> GetMatch(string pattern)
    {
        List<string> cacheKeys = GetKeys();
        List<string> l = cacheKeys.Where(k => Regex.IsMatch(k, pattern)).ToList();
        return l.AsReadOnly();
    }

    /// <summary>
    /// 获取所有缓存键
    /// </summary>
    /// <returns></returns>
    public List<string> GetKeys()
    {
        List<string> keys = [];

        var entries = _cache.GetType().GetField("_entries", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(_cache);
        if (entries is not IDictionary cacheItems) return keys;

        keys.AddRange(from DictionaryEntry cacheItem in cacheItems select cacheItem.Key.ToString()!);
        return keys;
    }

    #endregion
}