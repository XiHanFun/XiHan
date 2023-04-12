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

namespace XiHan.Extensions.Common.Converters;

/// <summary>
/// DateTimeJsonConverter
/// </summary>
public class DateTimeJsonConverter : JsonConverter<DateTime>
{
    private readonly string _DateFormatString;

    /// <summary>
    /// 构造函数
    /// </summary>
    public DateTimeJsonConverter()
    {
        _DateFormatString = "yyyy-MM-dd HH:mm:ss";
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dateFormatString"></param>
    public DateTimeJsonConverter(string dateFormatString)
    {
        _DateFormatString = dateFormatString;
    }

    /// <summary>
    /// 请求，将时间字符串转换为DateTime类型
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            if (DateTime.TryParse(reader.GetString(), out var date))
                return date;
        }
        return reader.GetDateTime();
    }

    /// <summary>
    /// 响应，将DateTime类型转换成指定格式的时间
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_DateFormatString));
    }
}