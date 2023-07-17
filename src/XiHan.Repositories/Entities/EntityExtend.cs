#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:EntityExtension
// Guid:4f70ee05-245b-435b-b83a-94f448dcf796
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-06-13 下午 08:56:48
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Apps.HttpContexts;

namespace XiHan.Repositories.Entities;

/// <summary>
/// 实体拓展
/// </summary>
public static class EntityExtend
{
    /// <summary>
    /// 转化为新增实体
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static TSource ToCreated<TSource>(this TSource source)
    {
        var propertyInfo = new PropertyInfo
        {
            HandleId = "CreatedId",
            HandleBy = "CreatedBy",
            HandleTime = "CreatedTime"
        };
        return source.CommonTo(propertyInfo);
    }

    /// <summary>
    /// 转化为更新实体
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static TSource ToModified<TSource>(this TSource source)
    {
        var propertyInfo = new PropertyInfo
        {
            HandleId = "ModifiedId",
            HandleBy = "ModifiedBy",
            HandleTime = "ModifiedTime"
        };
        return source.CommonTo(propertyInfo);
    }

    /// <summary>
    /// 转化为删除实体
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static TSource ToDeleted<TSource>(this TSource source)
    {
        var propertyInfo = new PropertyInfo
        {
            IsHandle = "IsDeleted",
            HandleId = "DeletedId",
            HandleBy = "DeletedBy",
            HandleTime = "DeletedTime"
        };
        return source.CommonTo(propertyInfo);
    }

    /// <summary>
    /// 转化为审核实体
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static TSource ToAudited<TSource>(this TSource source)
    {
        var propertyInfo = new PropertyInfo
        {
            HandleId = "AuditedId",
            HandleBy = "AuditedBy",
            HandleTime = "AuditedTime"
        };
        return source.CommonTo(propertyInfo);
    }

    #region 私有方法

    /// <summary>
    /// 转化为公共状态实体
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="propertyInfo"></param>
    /// <returns></returns>
    private static TSource CommonTo<TSource>(this TSource source, PropertyInfo propertyInfo)
    {
        var types = source?.GetType();
        if (types == null) return source;

        if (propertyInfo.IsHandle != null && types.GetProperty(propertyInfo.IsHandle) != null)
            types.GetProperty(propertyInfo.IsHandle)?.SetValue(source, true, null);
        if (propertyInfo.HandleTime != null && types.GetProperty(propertyInfo.HandleTime) != null)
            types.GetProperty(propertyInfo.HandleTime)?.SetValue(source, DateTime.Now, null);

        var context = AppHttpContextManager.Current;
        if (propertyInfo.HandleId != null && types.GetProperty(propertyInfo.HandleId) != null && context != null)
            types.GetProperty(propertyInfo.HandleId)?.SetValue(source, context.GetUserId(), null);
        if (propertyInfo.HandleBy != null && types.GetProperty(propertyInfo.HandleBy) != null && context != null)
            types.GetProperty(propertyInfo.HandleBy)?.SetValue(source, context.GetUserName(), null);
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
        public string? IsHandle { get; init; }

        /// <summary>
        /// 处理人主键
        /// </summary>
        public string? HandleId { get; init; }

        /// <summary>
        /// 处理人名称
        /// </summary>
        public string? HandleBy { get; init; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public string? HandleTime { get; init; }
    }

    #endregion
}