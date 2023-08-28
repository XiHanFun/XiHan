﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ApiResult
// Guid:4abbac7e-e91a-4ad2-a048-9f4c16a43464
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-02-20 下午 08:35:52
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Utils.Extensions;

namespace XiHan.Infrastructures.Responses;

/// <summary>
/// 通用响应结果
/// </summary>
public class ApiResult
{
    /// <summary>
    /// 是否成功
    /// </summary>
    public bool IsSuccess { get; private init; } = true;

    /// <summary>
    /// 响应码
    /// </summary>
    public ResponseCodeEnum Code { get; set; } = ResponseCodeEnum.Success;

    /// <summary>
    /// 响应信息
    /// </summary>
    public string? Message { get; set; } = ResponseCodeEnum.Success.GetEnumDescriptionByKey();

    /// <summary>
    /// 数据集合
    /// </summary>
    public dynamic? Data { get; set; }

    /// <summary>
    /// 时间戳
    /// </summary>
    public long Timestamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

    /// <summary>
    /// 继续响应 100
    /// </summary>
    /// <returns></returns>
    public static ApiResult Continue()
    {
        return new ApiResult
        {
            IsSuccess = true,
            Code = ResponseCodeEnum.Continue,
            Message = ResponseCodeEnum.Continue.GetEnumDescriptionByKey(),
            Data = null
        };
    }

    /// <summary>
    /// 响应成功 200
    /// </summary>
    /// <returns></returns>
    public static ApiResult Success()
    {
        return new ApiResult
        {
            IsSuccess = true,
            Code = ResponseCodeEnum.Success,
            Message = ResponseCodeEnum.Success.GetEnumDescriptionByKey(),
            Data = null
        };
    }

    /// <summary>
    /// 响应成功 200
    /// </summary>
    /// <param name="messageData"></param>
    /// <returns></returns>
    public static ApiResult Success(string messageData)
    {
        return new ApiResult
        {
            IsSuccess = true,
            Code = ResponseCodeEnum.Success,
            Message = ResponseCodeEnum.Success.GetEnumDescriptionByKey(),
            Data = messageData
        };
    }

    /// <summary>
    /// 响应成功，返回通用数据 200
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static ApiResult Success(dynamic data)
    {
        return new ApiResult
        {
            IsSuccess = true,
            Code = ResponseCodeEnum.Success,
            Message = ResponseCodeEnum.Success.GetEnumDescriptionByKey(),
            Data = data
        };
    }

    /// <summary>
    /// 响应失败，访问出错 400
    /// </summary>
    /// <param name="messageData"></param>
    /// <returns></returns>
    public static ApiResult BadRequest(string messageData)
    {
        return new ApiResult
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.BadRequest,
            Message = ResponseCodeEnum.BadRequest.GetEnumDescriptionByKey(),
            Data = messageData
        };
    }

    /// <summary>
    /// 响应失败，访问出错 400
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static ApiResult BadRequest(dynamic data)
    {
        return new ApiResult
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.BadRequest,
            Message = ResponseCodeEnum.BadRequest.GetEnumDescriptionByKey(),
            Data = data
        };
    }

    /// <summary>
    /// 响应失败，访问未授权 401
    /// </summary>
    /// <returns></returns>
    public static ApiResult Unauthorized()
    {
        return new ApiResult
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.Unauthorized,
            Message = ResponseCodeEnum.Unauthorized.GetEnumDescriptionByKey(),
            Data = null
        };
    }

    /// <summary>
    /// 响应失败，访问未授权 401
    /// </summary>
    /// <param name="messageData"></param>
    /// <returns></returns>
    public static ApiResult Unauthorized(string messageData)
    {
        return new ApiResult
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.Unauthorized,
            Message = ResponseCodeEnum.Unauthorized.GetEnumDescriptionByKey(),
            Data = messageData
        };
    }

    /// <summary>
    /// 响应失败，内容禁止访问 403
    /// </summary>
    /// <returns></returns>
    public static ApiResult Forbidden()
    {
        return new ApiResult
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.Forbidden,
            Message = ResponseCodeEnum.Forbidden.GetEnumDescriptionByKey(),
            Data = null
        };
    }

    /// <summary>
    /// 响应失败，数据未找到 404
    /// </summary>
    /// <returns></returns>
    public static ApiResult NotFound()
    {
        return new ApiResult
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.NotFound,
            Message = ResponseCodeEnum.NotFound.GetEnumDescriptionByKey(),
            Data = null
        };
    }

    /// <summary>
    /// 响应失败，数据未找到 404
    /// </summary>
    /// <param name="messageData"></param>
    /// <returns></returns>
    public static ApiResult NotFound(string messageData)
    {
        return new ApiResult
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.NotFound,
            Message = ResponseCodeEnum.NotFound.GetEnumDescriptionByKey(),
            Data = messageData
        };
    }

    /// <summary>
    ///  响应失败，参数不合法 422
    /// </summary>
    /// <returns></returns>
    public static ApiResult UnprocessableEntity()
    {
        return new ApiResult
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.UnprocessableEntity,
            Message = ResponseCodeEnum.UnprocessableEntity.GetEnumDescriptionByKey(),
            Data = null
        };
    }

    /// <summary>
    ///  响应失败，参数不合法 422
    /// </summary>
    /// <param name="messageData"></param>
    /// <returns></returns>
    public static ApiResult UnprocessableEntity(string messageData)
    {
        return new ApiResult
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.UnprocessableEntity,
            Message = ResponseCodeEnum.UnprocessableEntity.GetEnumDescriptionByKey(),
            Data = messageData
        };
    }

    /// <summary>
    /// 响应失败，参数不合法 422
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static ApiResult UnprocessableEntity(dynamic data)
    {
        return new ApiResult
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.UnprocessableEntity,
            Message = ResponseCodeEnum.UnprocessableEntity.GetEnumDescriptionByKey(),
            Data = data
        };
    }

    /// <summary>
    /// 并发请求过多 429
    /// </summary>
    /// <param name="messageData"></param>
    /// <returns></returns>
    public static ApiResult TooManyRequests(string messageData)
    {
        return new ApiResult
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.TooManyRequests,
            Message = ResponseCodeEnum.TooManyRequests.GetEnumDescriptionByKey(),
            Data = messageData
        };
    }

    /// <summary>
    /// 响应出错，服务器内部错误 500
    /// </summary>
    /// <returns></returns>
    public static ApiResult InternalServerError()
    {
        return new ApiResult
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.InternalServerError,
            Message = ResponseCodeEnum.InternalServerError.GetEnumDescriptionByKey(),
            Data = null
        };
    }

    /// <summary>
    /// 响应出错，服务器内部错误 500
    /// </summary>
    /// <returns></returns>
    public static ApiResult InternalServerError(string messageData)
    {
        return new ApiResult
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.InternalServerError,
            Message = ResponseCodeEnum.InternalServerError.GetEnumDescriptionByKey(),
            Data = messageData
        };
    }

    /// <summary>
    /// 响应出错，功能未实施 501
    /// </summary>
    /// <returns></returns>
    public static ApiResult NotImplemented()
    {
        return new ApiResult
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.NotImplemented,
            Message = ResponseCodeEnum.NotImplemented.GetEnumDescriptionByKey(),
            Data = null
        };
    }
}