#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BoolJsonConverter
// Guid:9e1cdf66-5e38-4760-8ecc-1481ef4054f8
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2022-12-05 下午 05:41:03
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Text.Json;
using System.Text.Json.Serialization;

namespace ZhaiFanhuaBlog.Extensions.Common.Controller;

/// <summary>
/// BoolJsonConverter
/// </summary>
public class BoolJsonConverter : JsonConverter<bool>
{
    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.True || reader.TokenType == JsonTokenType.False)
        {
            if (bool.TryParse(reader.GetString(), out bool date))
                return date;
        }
        return reader.GetBoolean();
    }

    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
    {
        writer.WriteBooleanValue(value);
    }
}