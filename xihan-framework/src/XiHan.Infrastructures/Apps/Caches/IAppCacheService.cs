#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IAppCacheService
// Guid:6fff80c4-ea26-4029-9780-f99bbf9ac227
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-26 上午 03:40:20
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Infrastructures.Apps.Caches;

/// <summary>
/// IAppCacheService
/// </summary>
public interface IAppCacheService
{
    #region 验证缓存项是否存在

    /// <summary>
    /// 验证缓存项是否存在,TryGetValue 来检测 Key 是否存在
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <returns></returns>
    bool Exists(string key);

    #endregion

    #region 设置缓存

    /// <summary>
    /// 设置永久缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <param name="value">缓存Value</param>
    /// <returns></returns>
    bool Set(string key, object value);

    /// <summary>
    /// 设置缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <param name="value">缓存Value</param>
    /// <param name="expiresSliding">滑动过期时长(如果在过期时间内有操作，则以当前时间点延长过期时间)</param>
    /// <param name="expiresAbsolute">绝对过期时长</param>
    /// <returns></returns>
    bool Set(string key, object value, TimeSpan expiresSliding, TimeSpan expiresAbsolute);

    /// <summary>
    /// 设置缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <param name="value">缓存Value</param>
    /// <param name="expiresIn">缓存时长</param>
    /// <param name="isSliding">是否滑动过期(如果在过期时间内有操作，则以当前时间点延长过期时间)</param>
    /// <returns></returns>
    bool Set(string key, object value, TimeSpan expiresIn, bool isSliding = false);

    /// <summary>
    /// 用键和值将某个缓存项插入缓存中，并指定基于时间的过期详细信息
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <param name="value">缓存Value</param>
    /// <param name="seconds">缓存时长，秒</param>
    bool SetWithSeconds(string key, object value, int seconds);

    /// <summary>
    /// 用键和值将某个缓存项插入缓存中，并指定基于时间的过期详细信息
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <param name="value">缓存Value</param>
    /// <param name="minutes">缓存时长，分钟</param>
    bool SetWithMinutes(string key, object value, int minutes);

    #endregion

    #region 删除缓存

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
    void Remove(IEnumerable<string> keys);

    /// <summary>
    /// 删除匹配到的缓存
    /// </summary>
    /// <param name="pattern"></param>
    /// <returns></returns>
    void RemoveByPattern(string pattern);

    /// <summary>
    /// 删除所有缓存
    /// </summary>
    void CleanAll();

    #endregion

    #region 修改缓存

    /// <summary>
    /// 修改缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <param name="value">新的缓存Value</param>
    /// <returns></returns>
    bool Update(string key, object value);

    /// <summary>
    /// 修改缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <param name="value">新的缓存Value</param>
    /// <param name="expiresSliding">滑动过期时长(如果在过期时间内有操作，则以当前时间点延长过期时间)</param>
    /// <param name="expiresAbsolute">绝对过期时长</param>
    /// <returns></returns>
    bool Update(string key, object value, TimeSpan expiresSliding, TimeSpan expiresAbsolute);

    /// <summary>
    /// 修改缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <param name="value">新的缓存Value</param>
    /// <param name="expiresIn">缓存时长</param>
    /// <param name="isSliding">是否滑动过期(如果在过期时间内有操作，则以当前时间点延长过期时间)</param>
    /// <returns></returns>
    bool Update(string key, object value, TimeSpan expiresIn, bool isSliding = false);

    #endregion

    #region 获取缓存

    /// <summary>
    /// 获取缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <returns></returns>
    T? Get<T>(string key) where T : class;

    /// <summary>
    /// 获取缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <returns></returns>
    object Get(string key);

    /// <summary>
    /// 获取缓存集合
    /// </summary>
    /// <param name="keys">缓存Key集合</param>
    /// <returns></returns>
    IDictionary<string, object?> Get(IEnumerable<string> keys);

    /// <summary>
    /// 搜索匹配缓存
    ///</summary>
    /// <param name="pattern"></param>
    /// <returns></returns>
    IEnumerable<string> GetMatch(string pattern);

    /// <summary>
    /// 获取所有缓存键
    /// </summary>
    /// <returns></returns>
    List<string> GetKeys();

    #endregion
}