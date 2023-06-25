#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ResultDto
// Guid:4abbac7e-e91a-4ad2-a048-9f4c16a43464
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-02-20 下午 08:35:52
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using XiHan.Infrastructures.Responses.Actions;
using XiHan.Utils.Extensions;

namespace XiHan.Infrastructures.Responses.Results;

/// <summary>
/// 通用结果实体
/// </summary>
public class ResultDto
{
    /// <summary>
    /// 是否成功
    /// </summary>
    public bool IsSuccess { get; set; } = true;

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
    public dynamic? Datas { get; set; }

    /// <summary>
    /// 时间戳
    /// </summary>
    public long Timestamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

    /// <summary>
    /// 继续响应 100
    /// </summary>
    /// <returns></returns>
    public static ResultDto Continue()
    {
        return new ResultDto
        {
            IsSuccess = true,
            Code = ResponseCodeEnum.Continue,
            Message = ResponseCodeEnum.Continue.GetEnumDescriptionByKey(),
            Datas = null
        };
    }

    /// <summary>
    /// 响应成功 200
    /// </summary>
    /// <returns></returns>
    public static ResultDto Success()
    {
        return new ResultDto
        {
            IsSuccess = true,
            Code = ResponseCodeEnum.Success,
            Message = ResponseCodeEnum.Success.GetEnumDescriptionByKey(),
            Datas = null
        };
    }

    /// <summary>
    /// 响应成功 200
    /// </summary>
    /// <param name="messageData"></param>
    /// <returns></returns>
    public static ResultDto Success(string messageData)
    {
        return new ResultDto
        {
            IsSuccess = true,
            Code = ResponseCodeEnum.Success,
            Message = ResponseCodeEnum.Success.GetEnumDescriptionByKey(),
            Datas = messageData
        };
    }

    /// <summary>
    /// 响应成功，返回通用数据 200
    /// </summary>
    /// <param name="datas"></param>
    /// <returns></returns>
    public static ResultDto Success(dynamic datas)
    {
        return new ResultDto
        {
            IsSuccess = true,
            Code = ResponseCodeEnum.Success,
            Message = ResponseCodeEnum.Success.GetEnumDescriptionByKey(),
            Datas = datas
        };
    }

    /// <summary>
    /// 响应失败，访问出错 400
    /// </summary>
    /// <param name="messageData"></param>
    /// <returns></returns>
    public static ResultDto BadRequest(string messageData)
    {
        return new ResultDto
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.BadRequest,
            Message = ResponseCodeEnum.BadRequest.GetEnumDescriptionByKey(),
            Datas = messageData
        };
    }

    /// <summary>
    /// 响应失败，访问出错 400
    /// </summary>
    /// <param name="datas"></param>
    /// <returns></returns>
    public static ResultDto BadRequest(dynamic datas)
    {
        return new ResultDto
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.BadRequest,
            Message = ResponseCodeEnum.BadRequest.GetEnumDescriptionByKey(),
            Datas = datas
        };
    }

    /// <summary>
    /// 响应失败，访问未授权 401
    /// </summary>
    /// <returns></returns>
    public static ResultDto Unauthorized()
    {
        return new ResultDto
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.Unauthorized,
            Message = ResponseCodeEnum.Unauthorized.GetEnumDescriptionByKey(),
            Datas = null
        };
    }

    /// <summary>
    /// 响应失败，访问未授权 401
    /// </summary>
    /// <param name="messageData"></param>
    /// <returns></returns>
    public static ResultDto Unauthorized(string messageData)
    {
        return new ResultDto
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.Unauthorized,
            Message = ResponseCodeEnum.Unauthorized.GetEnumDescriptionByKey(),
            Datas = messageData
        };
    }

    /// <summary>
    /// 响应失败，内容禁止访问 403
    /// </summary>
    /// <returns></returns>
    public static ResultDto Forbidden()
    {
        return new ResultDto
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.Forbidden,
            Message = ResponseCodeEnum.Forbidden.GetEnumDescriptionByKey(),
            Datas = null
        };
    }

    /// <summary>
    /// 响应失败，数据未找到 404
    /// </summary>
    /// <returns></returns>
    public static ResultDto NotFound()
    {
        return new ResultDto
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.NotFound,
            Message = ResponseCodeEnum.NotFound.GetEnumDescriptionByKey(),
            Datas = null
        };
    }

    /// <summary>
    /// 响应失败，数据未找到 404
    /// </summary>
    /// <param name="messageData"></param>
    /// <returns></returns>
    public static ResultDto NotFound(string messageData)
    {
        return new ResultDto
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.NotFound,
            Message = ResponseCodeEnum.NotFound.GetEnumDescriptionByKey(),
            Datas = messageData
        };
    }

    /// <summary>
    ///  响应失败，参数不合法 422
    /// </summary>
    /// <returns></returns>
    public static ResultDto UnprocessableEntity()
    {
        return new ResultDto
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.UnprocessableEntity,
            Message = ResponseCodeEnum.UnprocessableEntity.GetEnumDescriptionByKey(),
            Datas = null
        };
    }

    /// <summary>
    ///  响应失败，参数不合法 422
    /// </summary>
    /// <param name="messageData"></param>
    /// <returns></returns>
    public static ResultDto UnprocessableEntity(string messageData)
    {
        return new ResultDto
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.UnprocessableEntity,
            Message = ResponseCodeEnum.UnprocessableEntity.GetEnumDescriptionByKey(),
            Datas = messageData
        };
    }

    /// <summary>
    /// 响应失败，参数不合法 422
    /// </summary>
    /// <param name="modelState"></param>
    /// <returns></returns>
    public static ResultDto UnprocessableEntity(ModelStateDictionary modelState)
    {
        return new ResultDto
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.UnprocessableEntity,
            Message = ResponseCodeEnum.UnprocessableEntity.GetEnumDescriptionByKey(),
            Datas = new ValidationDto(modelState)
        };
    }

    /// <summary>
    /// 并发请求过多 429
    /// </summary>
    /// <param name="messageData"></param>
    /// <returns></returns>
    public static ResultDto TooManyRequests(string messageData)
    {
        return new ResultDto
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.TooManyRequests,
            Message = ResponseCodeEnum.TooManyRequests.GetEnumDescriptionByKey(),
            Datas = messageData
        };
    }

    /// <summary>
    /// 响应出错，服务器内部错误 500
    /// </summary>
    /// <returns></returns>
    public static ResultDto InternalServerError()
    {
        return new ResultDto
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.InternalServerError,
            Message = ResponseCodeEnum.InternalServerError.GetEnumDescriptionByKey(),
            Datas = null
        };
    }

    /// <summary>
    /// 响应出错，服务器内部错误 500
    /// </summary>
    /// <returns></returns>
    public static ResultDto InternalServerError(string messageData)
    {
        return new ResultDto
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.InternalServerError,
            Message = ResponseCodeEnum.InternalServerError.GetEnumDescriptionByKey(),
            Datas = messageData
        };
    }

    /// <summary>
    /// 响应出错，功能未实施 501
    /// </summary>
    /// <returns></returns>
    public static ResultDto NotImplemented()
    {
        return new ResultDto
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.NotImplemented,
            Message = ResponseCodeEnum.NotImplemented.GetEnumDescriptionByKey(),
            Datas = null
        };
    }
}