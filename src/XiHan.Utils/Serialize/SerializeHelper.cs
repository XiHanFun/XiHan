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
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XiHan.Utils.Serialize;

/// <summary>
/// 序列化帮助类
/// </summary>
public static class SerializeHelper
{
    /// <summary>
    /// 公共参数
    /// </summary>
    public static readonly JsonSerializerOptions JsonSerializerOptionsInstance = new()
    {
        // 序列化格式
        WriteIndented = true,
        // 忽略循环引用
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        // 数字类型
        NumberHandling = JsonNumberHandling.Strict,
        // 允许额外符号
        AllowTrailingCommas = true,
        // 注释处理，允许在 JSON 输入中使用注释并忽略它们
        ReadCommentHandling = JsonCommentHandling.Skip,
        // 属性名称不使用不区分大小写的比较
        PropertyNameCaseInsensitive = false,
        // 数据格式首字母小写 JsonNamingPolicy.CamelCase驼峰样式，null则为不改变大小写
        PropertyNamingPolicy = null,
        // 获取或设置要在转义字符串时使用的编码器，不转义字符
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    /// <summary>
    /// 序列化为Json
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public static string SerializeToJson(this object item)
    {
        return JsonSerializer.Serialize(item, JsonSerializerOptionsInstance);
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
        return Encoding.UTF8.GetString(value).DeserializeToObject<TEntity>();
    }
}