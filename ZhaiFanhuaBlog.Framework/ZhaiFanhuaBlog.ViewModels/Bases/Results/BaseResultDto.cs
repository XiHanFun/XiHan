// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BaseResultDto
// Guid:4abbac7e-e91a-4ad2-a048-9f4c16a43464
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-02-20 下午 08:35:52
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Utils.Summary;
using ZhaiFanhuaBlog.ViewModels.Response.Enum;

namespace ZhaiFanhuaBlog.ViewModels.Bases.Results;

/// <summary>
/// 通用结果实体
/// </summary>
public class BaseResultDto
{
    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Success { get; set; } = true;

    /// <summary>
    /// 状态码
    /// </summary>
    public ResponseCode Code { get; set; } = ResponseCode.OK;

    /// <summary>
    /// 返回信息
    /// </summary>
    public string? Message { get; set; } = EnumDescriptionHelper.GetEnumDescription(ResponseCode.OK);

    /// <summary>
    /// 数据集合
    /// </summary>
    public dynamic? Data { get; set; } = null;

    /// <summary>
    /// 时间戳
    /// </summary>
    public long Timestamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
}