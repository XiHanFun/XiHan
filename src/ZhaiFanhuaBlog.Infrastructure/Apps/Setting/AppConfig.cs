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

namespace ZhaiFanhuaBlog.Infrastructure.Apps.Setting;

/// <summary>
/// AppConfig
/// </summary>
public static class AppConfig
{
    /// <summary>
    /// 获取值
    /// </summary>
    /// <typeparam name="TREntity"></typeparam>
    /// <param name="entity"></param>
    /// <param name="fullName"></param>
    /// <returns></returns>
    public static TREntity Get<TREntity>(this TREntity entity, [CallerArgumentExpression("entity")] string fullName = "")
    {
        return AppConfigManager.Get<TREntity>(fullName);
    }

    /// <summary>
    /// 获取对象
    /// </summary>
    /// <typeparam name="TREntity"></typeparam>
    /// <param name="entity"></param>
    /// <param name="fullName"></param>
    /// <returns></returns>
    public static TREntity GetSection<TREntity>(this TREntity entity, [CallerArgumentExpression("entity")] string fullName = "")
    {
        return AppConfigManager.GetSection<TREntity>(fullName);
    }

    /// <summary>
    /// 赋值
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TREntity"></typeparam>
    /// <param name="entity"></param>
    /// <param name="value"></param>
    /// <param name="fullName"></param>
    public static void Set<TEntity, TREntity>(this TREntity entity, TREntity value, [CallerArgumentExpression("entity")] string fullName = "")
    {
        AppConfigManager.Set<TEntity, TREntity>(fullName, value);
    }
}