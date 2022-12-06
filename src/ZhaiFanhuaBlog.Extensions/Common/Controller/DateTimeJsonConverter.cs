#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:DateTimeJsonConverter
// Guid:fded905f-17ef-4373-afbc-f2716e06f072
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2022-12-05 下午 05:33:59
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Text.Json;
using System.Text.Json.Serialization;

namespace ZhaiFanhuaBlog.Extensions.Common.Controller;

/// <summary>
/// DateTimeJsonConverter
/// </summary>
public class DateTimeJsonConverter : JsonConverter<DateTime>
{
    private readonly string _dateFormatString;

    public DateTimeJsonConverter()
    {
        _dateFormatString = "yyyy-MM-dd HH:mm:ss";
    }

    public DateTimeJsonConverter(string dateFormatString)
    {
        _dateFormatString = dateFormatString;
    }

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            if (DateTime.TryParse(reader.GetString(), out DateTime date))
                return date;
        }
        return reader.GetDateTime();
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_dateFormatString));
    }
}