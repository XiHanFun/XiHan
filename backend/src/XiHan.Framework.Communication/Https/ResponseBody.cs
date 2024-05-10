#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ResponseBody
// Guid:c5d58756-3f12-4280-b647-3b24322efc52
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 6:33:51
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Common.Utilities.Extensions;
using XiHan.Infrastructure.Communication.Https.Enums;

namespace XiHan.Infrastructure.Communication.Https;

/// <summary>
/// 通用响应体
/// </summary>
public class ResponseBody
{
    /// <summary>
    /// 是否成功
    /// </summary>
    public virtual bool IsSuccess { get; protected init; } = true;

    /// <summary>
    /// 响应码
    /// </summary>
    public virtual ResponseCodeEnum Code { get; protected set; } = ResponseCodeEnum.Success;

    /// <summary>
    /// 响应信息
    /// </summary>
    public virtual string? Message { get; protected set; } = ResponseCodeEnum.Success.GetEnumDescriptionByKey();

    /// <summary>
    /// 数据集合
    /// </summary>
    public virtual dynamic? Datas { get; protected set; }

    /// <summary>
    /// 时间戳
    /// </summary>
    public virtual long Timestamp { get; protected set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

    /// <summary>
    /// 继续响应 100
    /// </summary>
    /// <returns></returns>
    public static ResponseBody Continue()
    {
        return new ResponseBody
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
    public static ResponseBody Success()
    {
        return new ResponseBody
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
    /// <param name="messageDatas"></param>
    /// <returns></returns>
    public static ResponseBody Success(string messageDatas)
    {
        return new ResponseBody
        {
            IsSuccess = true,
            Code = ResponseCodeEnum.Success,
            Message = ResponseCodeEnum.Success.GetEnumDescriptionByKey(),
            Datas = messageDatas
        };
    }

    /// <summary>
    /// 响应成功，返回通用数据 200
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static ResponseBody Success(dynamic data)
    {
        return new ResponseBody
        {
            IsSuccess = true,
            Code = ResponseCodeEnum.Success,
            Message = ResponseCodeEnum.Success.GetEnumDescriptionByKey(),
            Datas = data
        };
    }

    /// <summary>
    /// 响应失败，访问出错 400
    /// </summary>
    /// <param name="messageDatas"></param>
    /// <returns></returns>
    public static ResponseBody BadRequest(string messageDatas)
    {
        return new ResponseBody
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.BadRequest,
            Message = ResponseCodeEnum.BadRequest.GetEnumDescriptionByKey(),
            Datas = messageDatas
        };
    }

    /// <summary>
    /// 响应失败，访问出错 400
    /// </summary>
    /// <param name="errorDatas"></param>
    /// <returns></returns>
    public static ResponseBody BadRequest(dynamic errorDatas)
    {
        return new ResponseBody
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.BadRequest,
            Message = ResponseCodeEnum.BadRequest.GetEnumDescriptionByKey(),
            Datas = errorDatas
        };
    }

    /// <summary>
    /// 响应失败，访问未授权 401
    /// </summary>
    /// <returns></returns>
    public static ResponseBody Unauthorized()
    {
        return new ResponseBody
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
    /// <param name="messageDatas"></param>
    /// <returns></returns>
    public static ResponseBody Unauthorized(string messageDatas)
    {
        return new ResponseBody
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.Unauthorized,
            Message = ResponseCodeEnum.Unauthorized.GetEnumDescriptionByKey(),
            Datas = messageDatas
        };
    }

    /// <summary>
    /// 响应失败，内容禁止访问 403
    /// </summary>
    /// <returns></returns>
    public static ResponseBody Forbidden()
    {
        return new ResponseBody
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
    public static ResponseBody NotFound()
    {
        return new ResponseBody
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
    /// <param name="messageDatas"></param>
    /// <returns></returns>
    public static ResponseBody NotFound(string messageDatas)
    {
        return new ResponseBody
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.NotFound,
            Message = ResponseCodeEnum.NotFound.GetEnumDescriptionByKey(),
            Datas = messageDatas
        };
    }

    /// <summary>
    ///  响应失败，参数不合法 422
    /// </summary>
    /// <returns></returns>
    public static ResponseBody UnprocessableEntity()
    {
        return new ResponseBody
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
    /// <param name="messageDatas"></param>
    /// <returns></returns>
    public static ResponseBody UnprocessableEntity(string messageDatas)
    {
        return new ResponseBody
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.UnprocessableEntity,
            Message = ResponseCodeEnum.UnprocessableEntity.GetEnumDescriptionByKey(),
            Datas = messageDatas
        };
    }

    /// <summary>
    /// 响应失败，参数不合法 422
    /// </summary>
    /// <param name="errorDatas"></param>
    /// <returns></returns>
    public static ResponseBody UnprocessableEntity(dynamic errorDatas)
    {
        return new ResponseBody
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.UnprocessableEntity,
            Message = ResponseCodeEnum.UnprocessableEntity.GetEnumDescriptionByKey(),
            Datas = errorDatas
        };
    }

    /// <summary>
    /// 并发请求过多 429
    /// </summary>
    /// <param name="messageDatas"></param>
    /// <returns></returns>
    public static ResponseBody TooManyRequests(string messageDatas)
    {
        return new ResponseBody
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.TooManyRequests,
            Message = ResponseCodeEnum.TooManyRequests.GetEnumDescriptionByKey(),
            Datas = messageDatas
        };
    }

    /// <summary>
    /// 响应出错，服务器内部错误 500
    /// </summary>
    /// <returns></returns>
    public static ResponseBody InternalServerError()
    {
        return new ResponseBody
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
    /// <param name="errorDatas"></param>
    /// <returns></returns>
    public static ResponseBody InternalServerError(string errorDatas)
    {
        return new ResponseBody
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.InternalServerError,
            Message = ResponseCodeEnum.InternalServerError.GetEnumDescriptionByKey(),
            Datas = errorDatas
        };
    }

    /// <summary>
    /// 响应出错，功能未实施 501
    /// </summary>
    /// <returns></returns>
    public static ResponseBody NotImplemented()
    {
        return new ResponseBody
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.NotImplemented,
            Message = ResponseCodeEnum.NotImplemented.GetEnumDescriptionByKey(),
            Datas = null
        };
    }
}

/// <summary>
/// 通用响应体
/// </summary>
public class ResponseBody<T> : ResponseBody
{
    /// <summary>
    /// 数据集合
    /// </summary>
    public new T? Datas { get; protected set; }

    /// <summary>
    /// 响应成功，返回通用数据 200
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static ResponseBody<T> Success(T data)
    {
        return new ResponseBody<T>
        {
            IsSuccess = true,
            Code = ResponseCodeEnum.Success,
            Message = ResponseCodeEnum.Success.GetEnumDescriptionByKey(),
            Datas = data
        };
    }

    /// <summary>
    /// 响应失败，访问出错 400
    /// </summary>
    /// <param name="errorDatas"></param>
    /// <returns></returns>
    public static ResponseBody<T> BadRequest(T errorDatas)
    {
        return new ResponseBody<T>
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.BadRequest,
            Message = ResponseCodeEnum.BadRequest.GetEnumDescriptionByKey(),
            Datas = errorDatas
        };
    }

    /// <summary>
    /// 响应失败，参数不合法 422
    /// </summary>
    /// <param name="errorDatas"></param>
    /// <returns></returns>
    public static ResponseBody<T> UnprocessableEntity(T errorDatas)
    {
        return new ResponseBody<T>
        {
            IsSuccess = false,
            Code = ResponseCodeEnum.UnprocessableEntity,
            Message = ResponseCodeEnum.UnprocessableEntity.GetEnumDescriptionByKey(),
            Datas = errorDatas
        };
    }
}