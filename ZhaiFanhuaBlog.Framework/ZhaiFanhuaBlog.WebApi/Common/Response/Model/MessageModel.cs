// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:MessageModel
// Guid:4abbac7e-e91a-4ad2-a048-9f4c16a43464
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-02-20 下午 08:35:52
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Utils.Summaries;
using ZhaiFanhuaBlog.WebApi.Common.Response.Enum;

namespace ZhaiFanhuaBlog.WebApi.Common.Response.Model;

/// <summary>
/// 通用数据类
/// </summary>
public class MessageModel
{
    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Success { get; set; } = true;

    /// <summary>
    /// 状态码
    /// </summary>
    public ResultCode Code { get; set; } = ResultCode.OK;

    /// <summary>
    /// 返回信息
    /// </summary>
    public string? Message { get; set; } = EnumDescriptionHelper.GetEnumDescription(ResultCode.UnprocessableEntity);

    /// <summary>
    /// 数据集合
    /// </summary>
    public dynamic? Data { get; set; } = null;

    /// <summary>
    /// 时间戳
    /// </summary>
    public long Timestamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
}