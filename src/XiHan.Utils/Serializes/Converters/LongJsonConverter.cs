#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:LongJsonConverter
// Guid:6c399d59-ef12-4354-a5b6-d9af73b04f8b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-25 下午 11:04:51
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Text.Json.Serialization;
using System.Text.Json;
using XiHan.Utils.Objects;

namespace XiHan.Utils.Serializes.Converters;

/// <summary>
/// LongJsonConverter
/// </summary>
public class LongJsonConverter : JsonConverter<long>
{
    /// <summary>
    /// 读
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number)
            return reader.GetInt64();

        return reader.GetString().ParseToLong();
    }

    /// <summary>
    /// 写
    /// long数据在前端会出现丢失精度，故转换为string类型
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}