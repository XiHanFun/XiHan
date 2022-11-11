#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BaseResponseDto
// Guid:3874a6ae-1ebc-49ab-b4a3-55e76dc68945
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 11:46:50
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Mvc.Filters;
using ZhaiFanhuaBlog.Utils.Summary;
using ZhaiFanhuaBlog.ViewModels.Bases.Results;
using ZhaiFanhuaBlog.ViewModels.Bases.Validation;
using ZhaiFanhuaBlog.ViewModels.Response.Enum;

namespace ZhaiFanhuaBlog.ViewModels.Response;

/// <summary>
/// 通用响应实体
/// </summary>
public class BaseResponseDto
{
    /// <summary>
    /// 继续响应 100
    /// </summary>
    /// <returns></returns>
    public static BaseResultDto Continue()
    {
        return new BaseResultDto
        {
            Success = true,
            Code = ResponseEnum.Continue,
            Message = EnumDescriptionHelper.GetEnumDescription(ResponseEnum.Continue),
            Data = null
        };
    }

    /// <summary>
    /// 响应成功 200
    /// </summary>
    /// <returns></returns>
    public static BaseResultDto OK()
    {
        return new BaseResultDto
        {
            Success = true,
            Code = ResponseEnum.OK,
            Message = EnumDescriptionHelper.GetEnumDescription(ResponseEnum.OK),
            Data = null
        };
    }

    /// <summary>
    /// 响应成功 200
    /// </summary>
    /// <returns></returns>
    public static BaseResultDto OK(string messageData)
    {
        return new BaseResultDto
        {
            Success = true,
            Code = ResponseEnum.OK,
            Message = EnumDescriptionHelper.GetEnumDescription(ResponseEnum.OK),
            Data = messageData
        };
    }

    /// <summary>
    /// 响应成功，返回通用数据 200
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static BaseResultDto OK(dynamic data)
    {
        return new BaseResultDto
        {
            Success = true,
            Code = ResponseEnum.OK,
            Message = EnumDescriptionHelper.GetEnumDescription(ResponseEnum.OK),
            Data = data
        };
    }

    /// <summary>
    /// 响应失败，访问出错 400
    /// </summary>
    /// <returns></returns>
    public static BaseResultDto BadRequest(string messageData)
    {
        return new BaseResultDto
        {
            Success = false,
            Code = ResponseEnum.BadRequest,
            Message = EnumDescriptionHelper.GetEnumDescription(ResponseEnum.BadRequest),
            Data = messageData
        };
    }

    /// <summary>
    /// 响应失败，访问出错 400
    /// </summary>
    /// <returns></returns>
    public static BaseResultDto BadRequest(dynamic data)
    {
        return new BaseResultDto
        {
            Success = false,
            Code = ResponseEnum.BadRequest,
            Message = EnumDescriptionHelper.GetEnumDescription(ResponseEnum.BadRequest),
            Data = data
        };
    }

    /// <summary>
    /// 响应失败，访问未授权 401
    /// </summary>
    /// <returns></returns>
    public static BaseResultDto Unauthorized()
    {
        return new BaseResultDto
        {
            Success = false,
            Code = ResponseEnum.Unauthorized,
            Message = EnumDescriptionHelper.GetEnumDescription(ResponseEnum.Unauthorized),
            Data = null
        };
    }

    /// <summary>
    /// 响应失败，访问未授权 401
    /// </summary>
    /// <returns></returns>
    public static BaseResultDto Unauthorized(string messageData)
    {
        return new BaseResultDto
        {
            Success = false,
            Code = ResponseEnum.Unauthorized,
            Message = EnumDescriptionHelper.GetEnumDescription(ResponseEnum.Unauthorized),
            Data = messageData
        };
    }

    /// <summary>
    /// 响应失败，内容禁止访问 403
    /// </summary>
    /// <returns></returns>
    public static BaseResultDto Forbidden()
    {
        return new BaseResultDto
        {
            Success = false,
            Code = ResponseEnum.Forbidden,
            Message = EnumDescriptionHelper.GetEnumDescription(ResponseEnum.Forbidden),
            Data = null
        };
    }

    /// <summary>
    /// 响应失败，数据未找到 404
    /// </summary>
    /// <returns></returns>
    public static BaseResultDto NotFound()
    {
        return new BaseResultDto
        {
            Success = false,
            Code = ResponseEnum.NotFound,
            Message = EnumDescriptionHelper.GetEnumDescription(ResponseEnum.NotFound),
            Data = null
        };
    }

    /// <summary>
    /// 响应失败，数据未找到 404
    /// </summary>
    /// <returns></returns>
    public static BaseResultDto NotFound(string messageData)
    {
        return new BaseResultDto
        {
            Success = false,
            Code = ResponseEnum.NotFound,
            Message = EnumDescriptionHelper.GetEnumDescription(ResponseEnum.NotFound),
            Data = messageData
        };
    }

    /// <summary>
    /// 响应失败，参数不合法 422
    /// </summary>
    /// <returns></returns>
    public static BaseResultDto UnprocessableEntity(string messageData)
    {
        return new BaseResultDto
        {
            Success = false,
            Code = ResponseEnum.UnprocessableEntity,
            Message = EnumDescriptionHelper.GetEnumDescription(ResponseEnum.UnprocessableEntity),
            Data = messageData
        };
    }

    /// <summary>
    /// 响应失败，参数不合法 422
    /// </summary>
    /// <returns></returns>
    public static BaseResultDto UnprocessableEntity(ActionExecutingContext context)
    {
        return new BaseResultDto
        {
            Success = false,
            Code = ResponseEnum.UnprocessableEntity,
            Message = EnumDescriptionHelper.GetEnumDescription(ResponseEnum.UnprocessableEntity),
            Data = new BaseValidationDto(context)
        };
    }

    /// <summary>
    /// 响应失败，服务器内部错误 500
    /// </summary>
    /// <returns></returns>
    public static BaseResultDto InternalServerError()
    {
        return new BaseResultDto
        {
            Success = false,
            Code = ResponseEnum.InternalServerError,
            Message = EnumDescriptionHelper.GetEnumDescription(ResponseEnum.InternalServerError),
            Data = null
        };
    }

    /// <summary>
    /// 响应失败，功能未实施 501
    /// </summary>
    /// <returns></returns>
    public static BaseResultDto NotImplemented()
    {
        return new BaseResultDto
        {
            Success = false,
            Code = ResponseEnum.NotImplemented,
            Message = EnumDescriptionHelper.GetEnumDescription(ResponseEnum.NotImplemented),
            Data = null
        };
    }
}