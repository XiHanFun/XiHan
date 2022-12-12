#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:AppConfig
// Guid:6d94d4d9-7ab6-4feb-95b2-d055bcc13494
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-12-10 上午 02:00:44
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Runtime.CompilerServices;

namespace ZhaiFanhuaBlog.Infrastructure.AppSetting;

/// <summary>
/// AppConfig
/// </summary>
public static class AppConfig
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="REntity"></typeparam>
    /// <param name="entity"></param>
    /// <param name="fullName"></param>
    /// <returns></returns>
    public static REntity Get<REntity>(this REntity entity, [CallerArgumentExpression("entity")] string fullName = "")
    {
        return AppConfigManager.Get<REntity>(fullName);
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="REntity"></typeparam>
    /// <param name="entity"></param>
    /// <param name="fullName"></param>
    /// <returns></returns>
    public static REntity GetSection<REntity>(this REntity entity, [CallerArgumentExpression("entity")] string fullName = "")
    {
        return AppConfigManager.GetSection<REntity>(fullName);
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="REntity"></typeparam>
    /// <param name="entity"></param>
    /// <param name="value"></param>
    /// <param name="fullName"></param>
    public static void Set<TEntity, REntity>(this REntity entity, REntity value, [CallerArgumentExpression("entity")] string fullName = "")
    {
        AppConfigManager.Set<TEntity, REntity>(fullName, value);
    }
}