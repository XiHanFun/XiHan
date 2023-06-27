#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BooleanJsonConverter
// Guid:9e1cdf66-5e38-4760-8ecc-1481ef4054f8
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-12-05 下午 05:41:03
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Text.Json;
using System.Text.Json.Serialization;
using XiHan.Utils.Extensions;

namespace XiHan.Utils.Serializes.Converters;

/// <summary>
/// BooleanJsonConverter
/// </summary>
public class BooleanJsonConverter : JsonConverter<bool>
{
    /// <summary>
    /// 读
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType is JsonTokenType.True or JsonTokenType.False
            ? reader.GetBoolean()
            : reader.GetString().ParseToBool();
    }

    /// <summary>
    /// 写
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
    {
        writer.WriteBooleanValue(value);
    }
}