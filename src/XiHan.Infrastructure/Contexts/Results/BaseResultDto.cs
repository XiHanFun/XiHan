#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BaseResultDto
// Guid:4abbac7e-e91a-4ad2-a048-9f4c16a43464
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-02-20 下午 08:35:52
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Utils.Enums;

namespace XiHan.Infrastructure.Contexts.Results;

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
    public HttpStatusEnum StatusCode { get; set; } = HttpStatusEnum.Success;

    /// <summary>
    /// 状态信息
    /// </summary>
    public string StatusMessage { get; set; } = HttpStatusEnum.Success.GetEnumDescriptionByKey();

    /// <summary>
    /// 响应码
    /// </summary>
    public HttpResponseEnum ResponseCode { get; set; } = HttpResponseEnum.Ok;

    /// <summary>
    /// 响应信息
    /// </summary>
    public string? ResponseMessage { get; set; } = HttpResponseEnum.Ok.GetEnumDescriptionByKey();

    /// <summary>
    /// 数据集合
    /// </summary>
    public dynamic? Datas { get; set; }

    /// <summary>
    /// 时间戳
    /// </summary>
    public long Timestamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
}