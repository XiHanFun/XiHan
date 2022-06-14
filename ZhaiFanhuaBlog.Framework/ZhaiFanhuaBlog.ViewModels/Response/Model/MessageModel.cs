// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:MessageModel
// Guid:4abbac7e-e91a-4ad2-a048-9f4c16a43464
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-02-20 下午 08:35:52
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.ViewModels.Response.Enum;

namespace ZhaiFanhuaBlog.ViewModels.Response.Model;

/// <summary>
/// 通用返回信息类
/// </summary>
public class MessageModel<TEntity>
{
    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 状态码
    /// </summary>
    public ResultCode Code { get; set; }

    /// <summary>
    /// 返回信息
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// 数据集合
    /// </summary>
    public TEntity? Data { get; set; }

    /// <summary>
    /// 时间戳
    /// </summary>
    public long Timestamp { get; set; }
}