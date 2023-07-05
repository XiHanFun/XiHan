#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:AppCacheService
// Guid:5f71897c-e758-47c5-b87b-f949a269f899
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-26 上午 03:01:05
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.Caching.Memory;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;

namespace XiHan.Infrastructures.Apps.Caches;

/// <summary>
/// 内存缓存操作
/// </summary>
public class AppCacheService : IAppCacheService
{
    /// <summary>
    /// 默认缓存配置
    /// </summary>
    private readonly IMemoryCache _cache = new MemoryCache(new MemoryCacheOptions
    {
        // 最大缓存个数限制(这属于一个说明性属性，而且单位也不是缓存数目，而是缓存真正占用的空间大小，当所有缓存大小超过这个值的时候进行一次缓存压缩)
        SizeLimit = 60,
        // 过期扫描频率(默认为1分钟，可以理解为每过多久移除一次过期的缓存项)
        ExpirationScanFrequency = TimeSpan.FromMinutes(5)
    });

    #region 验证缓存项是否存在

    /// <summary>
    /// 验证缓存项是否存在,TryGetValue 来检测 Key 是否存在
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <returns></returns>
    public bool Exists(string key)
    {
        if (key == null) throw new ArgumentNullException(nameof(key));

        return _cache.TryGetValue(key, out _);
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
        if (key == null) throw new ArgumentNullException(nameof(key));
        if (value == null) throw new ArgumentNullException(nameof(value));

        _cache.Set(key, value);
        return Exists(key);
    }

    /// <summary>
    /// 设置缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <param name="value">缓存Value</param>
    /// <param name="expiresSliding">滑动过期时长（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
    /// <param name="expiresAbsolute">绝对过期时长</param>
    /// <returns></returns>
    public bool Set(string key, object value, TimeSpan expiresSliding, TimeSpan expiresAbsolute)
    {
        if (key == null) throw new ArgumentNullException(nameof(key));
        if (value == null) throw new ArgumentNullException(nameof(value));

        _cache.Set(key, value, new MemoryCacheEntryOptions()
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
    /// <param name="isSliding">是否滑动过期（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
    /// <returns></returns>
    public bool Set(string key, object value, TimeSpan expiresIn, bool isSliding = false)
    {
        if (key == null) throw new ArgumentNullException(nameof(key));
        if (value == null) throw new ArgumentNullException(nameof(value));

        _cache.Set(key, value, isSliding
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
        if (key == null) throw new ArgumentNullException(nameof(key));
        if (value == null) throw new ArgumentNullException(nameof(value));
        if (seconds <= 0) throw new ArgumentOutOfRangeException(nameof(seconds));

        _cache.Set(key, value, DateTime.Now.AddSeconds(seconds));
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
        if (key == null) throw new ArgumentNullException(nameof(key));
        if (value == null) throw new ArgumentNullException(nameof(value));
        if (minutes <= 0) throw new ArgumentOutOfRangeException(nameof(minutes));

        _cache.Set(key, value, DateTime.Now.AddMinutes(minutes));
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
        if (key == null) throw new ArgumentNullException(nameof(key));

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
        if (keys == null) throw new ArgumentNullException(nameof(keys));

        keys.ToList().ForEach(item => _cache.Remove(item));
    }

    /// <summary>
    /// 删除匹配到的缓存
    /// </summary>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public void RemoveByPattern(string pattern)
    {
        var l = GetMatch(pattern);
        foreach (var s in l) Remove(s);
    }

    /// <summary>
    /// 删除所有缓存
    /// </summary>
    public void ClearAll()
    {
        var l = GetKeys();
        foreach (var s in l) Remove(s);
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
        if (key == null) throw new ArgumentNullException(nameof(key));
        if (value == null) throw new ArgumentNullException(nameof(value));

        if (!Exists(key)) return Set(key, value);
        return Remove(key) && Set(key, value);
    }

    /// <summary>
    /// 修改缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <param name="value">新的缓存Value</param>
    /// <param name="expiresSliding">滑动过期时长（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
    /// <param name="expiresAbsolute">绝对过期时长</param>
    /// <returns></returns>
    public bool Update(string key, object value, TimeSpan expiresSliding, TimeSpan expiresAbsolute)
    {
        if (key == null) throw new ArgumentNullException(nameof(key));
        if (value == null) throw new ArgumentNullException(nameof(value));

        if (!Exists(key)) return Set(key, value, expiresSliding, expiresAbsolute);
        return Remove(key) && Set(key, value, expiresSliding, expiresAbsolute);
    }

    /// <summary>
    /// 修改缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <param name="value">新的缓存Value</param>
    /// <param name="expiresIn">缓存时长</param>
    /// <param name="isSliding">是否滑动过期（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
    /// <returns></returns>
    public bool Update(string key, object value, TimeSpan expiresIn, bool isSliding = false)
    {
        if (key == null) throw new ArgumentNullException(nameof(key));
        if (value == null) throw new ArgumentNullException(nameof(value));

        if (!Exists(key)) return Set(key, value, expiresIn, isSliding);
        return Remove(key) && Set(key, value, expiresIn, isSliding);
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
        if (key == null) throw new ArgumentNullException(nameof(key));

        return _cache.Get(key) as T;
    }

    /// <summary>
    /// 获取缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <returns></returns>
    public object Get(string key)
    {
        if (key == null) throw new ArgumentNullException(nameof(key));

        return _cache.Get(key) ?? new object();
    }

    /// <summary>
    /// 获取缓存集合
    /// </summary>
    /// <param name="keys">缓存Key集合</param>
    /// <returns></returns>
    public IDictionary<string, object?> Get(IEnumerable<string> keys)
    {
        if (keys == null) throw new ArgumentNullException(nameof(keys));

        var dict = new Dictionary<string, object?>();
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
        var cacheKeys = GetKeys();
        var l = cacheKeys.Where(k => Regex.IsMatch(k, pattern)).ToList();
        return l.AsReadOnly();
    }

    /// <summary>
    /// 获取所有缓存键
    /// </summary>
    /// <returns></returns>
    public List<string> GetKeys()
    {
        var keys = new List<string>();

        var entries = _cache.GetType().GetField("_entries", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(_cache);
        var cacheItems = entries as IDictionary;
        if (cacheItems == null) return keys;
        keys.AddRange(from DictionaryEntry cacheItem in cacheItems select cacheItem.Key.ToString()!);
        return keys;
    }

    #endregion
}