// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SerializeHelper
// Guid:71a6060f-74fa-4e1d-846d-2ec166b3ed78
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-07 上午 03:00:55
// ----------------------------------------------------------------

using Newtonsoft.Json;
using System.Text;

namespace ZhaiFanhuaBlog.Utils.Serialize;

/// <summary>
/// 序列化帮助类
/// </summary>
public class SerializeHelper
{
    /// <summary>
    /// 序列化
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public static byte[] Serialize(object item)
    {
        var jsonString = JsonConvert.SerializeObject(item);
        return Encoding.UTF8.GetBytes(jsonString);
    }

    /// <summary>
    /// 反序列化
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TEntity? Deserialize<TEntity>(byte[] value)
    {
        if (value == null)
        {
            return default;
        }
        var jsonString = Encoding.UTF8.GetString(value);
        return JsonConvert.DeserializeObject<TEntity>(jsonString);
    }
}