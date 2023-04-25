#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:DecimalJsonConverter
// Guid:302db373-1155-429b-b57b-07744a04c1b7
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-25 下午 10:57:32
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Text.Json.Serialization;
using System.Text.Json;
using XiHan.Utils.Objects;

namespace XiHan.Utils.Serializes.Converters;

/// <summary>
/// DecimalJsonConverter
/// </summary>

public class DecimalJsonConverter : JsonConverter<decimal>
{
    /// <summary>
    /// 读
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number)
        {
            return reader.GetDecimal();
        }

        return string.IsNullOrEmpty(reader.GetString()) ? 0L : reader.GetString().ParseToDecimal();
    }

    /// <summary>
    /// 写
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}