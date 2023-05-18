#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:IAppCacheService
// Guid:6fff80c4-ea26-4029-9780-f99bbf9ac227
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-26 上午 03:40:20
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Common.Caches;

/// <summary>
/// IAppCacheService
/// </summary>
public interface IAppCacheService
{
    #region  验证缓存项是否存在

    /// <summary>
    /// 验证缓存项是否存在
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <returns></returns>
    bool Exists(string key);

    #endregion

    #region  添加缓存

    /// <summary>
    /// 添加缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <param name="value">缓存Value</param>
    /// <returns></returns>
    bool Add(string key, object value);

    /// <summary>
    /// 添加缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <param name="value">缓存Value</param>
    /// <param name="expiresSliding">滑动过期时长（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
    /// <param name="expiressAbsoulte">绝对过期时长</param>
    /// <returns></returns>
    bool Add(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte);

    /// <summary>
    /// 添加缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <param name="value">缓存Value</param>
    /// <param name="expiresIn">缓存时长</param>
    /// <param name="isSliding">是否滑动过期（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
    /// <returns></returns>
    bool Add(string key, object value, TimeSpan expiresIn, bool isSliding = false);

    /// <summary>
    /// 添加缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <param name="value">缓存Value</param>
    /// <param name="seconds">缓存时长，默认7200秒</param>
    /// <returns></returns>
    bool Add(string key, object value, int seconds = 7200);

    #endregion

    #region  删除缓存

    /// <summary>
    /// 删除缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <returns></returns>
    bool Remove(string key);

    /// <summary>
    /// 批量删除缓存
    /// </summary>
    /// <param name="keys">缓存Key集合</param>
    /// <returns></returns>
    void RemoveAll(IEnumerable<string> keys);

    /// <summary>
    /// 使用通配符找出所有的key然后逐个删除
    /// </summary>
    /// <param name="pattern">通配符</param>
    void RemoveByPattern(string pattern);

    /// <summary>
    /// 删除所有缓存
    /// </summary>
    void RemoveCacheAll();

    #endregion

    #region  获取缓存

    /// <summary>
    /// 获取缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <returns></returns>
    T? GetByKey<T>(string key) where T : class;

    /// <summary>
    /// 获取缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <returns></returns>
    object? GetByKey(string key);

    /// <summary>
    /// 获取缓存集合
    /// </summary>
    /// <param name="keys">缓存Key集合</param>
    /// <returns></returns>
    IDictionary<string, object?> GetAll(IEnumerable<string> keys);

    #endregion

    #region  修改缓存

    /// <summary>
    /// 修改缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <param name="value">新的缓存Value</param>
    /// <returns></returns>
    bool Replace(string key, object value);

    /// <summary>
    /// 修改缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <param name="value">新的缓存Value</param>
    /// <param name="expiresSliding">滑动过期时长（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
    /// <param name="expiressAbsoulte">绝对过期时长</param>
    /// <returns></returns>
    bool Replace(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte);

    /// <summary>
    /// 修改缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <param name="value">新的缓存Value</param>
    /// <param name="expiresIn">缓存时长</param>
    /// <param name="isSliding">是否滑动过期（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
    /// <returns></returns>
    bool Replace(string key, object value, TimeSpan expiresIn, bool isSliding = false);

    #endregion
}