#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SerializeHelper
// Guid:71a6060f-74fa-4e1d-846d-2ec166b3ed78
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-07 上午 03:00:55
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Text;
using System.Text.Json;

namespace ZhaiFanhuaBlog.Utils.Serialize;

/// <summary>
/// 序列化帮助类
/// </summary>
public static class SerializeHelper
{
    /// <summary>
    /// 序列化为Json
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public static string SerializeToJson(this object item)
    {
        return JsonSerializer.Serialize(item);
    }

    /// <summary>
    /// 序列化为Byte
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public static byte[] SerializeToBate(this object item)
    {
        return Encoding.UTF8.GetBytes(item.SerializeToJson());
    }

    /// <summary>
    /// Json反序列化为对象
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="jsonString"></param>
    /// <returns></returns>
    public static TEntity? DeserializeToObject<TEntity>(this string jsonString)
    {
        return JsonSerializer.Deserialize<TEntity>(jsonString);
    }

    /// <summary>
    /// Byte反序列化为对象
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TEntity? Deserialize<TEntity>(this byte[] value)
    {
        if (value == null)
        {
            return default;
        }
        return Encoding.UTF8.GetString(value).DeserializeToObject<TEntity>();
    }
}