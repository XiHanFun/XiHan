#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:IntJsonConverter
// Guid:b7dc3b41-c151-4ed0-a5ee-92325a1e2be7
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-25 下午 11:04:00
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Text.Json;
using System.Text.Json.Serialization;
using XiHan.Utils.Extensions;

namespace XiHan.Utils.Serializes.Converters;

/// <summary>
/// IntJsonConverter
/// </summary>
public class IntJsonConverter : JsonConverter<int>
{
    /// <summary>
    /// 读
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType == JsonTokenType.Number ? reader.GetInt32() : reader.GetString().ParseToInt();
    }

    /// <summary>
    /// 写
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}