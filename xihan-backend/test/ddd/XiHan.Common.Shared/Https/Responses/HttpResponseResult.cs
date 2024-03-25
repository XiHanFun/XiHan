#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:HttpResponseResult
// Guid:4abbac7e-e91a-4ad2-a048-9f4c16a43464
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-02-20 下午 08:35:52
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Common.Shared.Https.Enums;
using XiHan.Common.Utilities.Extensions;

namespace XiHan.Common.Shared.Https.Responses;

/// <summary>
/// 网络响应结果
/// </summary>
public class HttpResponseResult
{
    /// <summary>
    /// 是否成功
    /// </summary>
    public bool IsSuccess { get; private init; } = true;

    /// <summary>
    /// 响应码
    /// </summary>
    public HttpResponseCodeEnum Code { get; set; } = HttpResponseCodeEnum.Success;

    /// <summary>
    /// 响应信息
    /// </summary>
    public string? Message { get; set; } = HttpResponseCodeEnum.Success.GetEnumDescriptionByKey();

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
    public static HttpResponseResult Continue()
    {
        return new HttpResponseResult
        {
            IsSuccess = true,
            Code = HttpResponseCodeEnum.Continue,
            Message = HttpResponseCodeEnum.Continue.GetEnumDescriptionByKey(),
            Datas = null
        };
    }

    /// <summary>
    /// 响应成功 200
    /// </summary>
    /// <returns></returns>
    public static HttpResponseResult Success()
    {
        return new HttpResponseResult
        {
            IsSuccess = true,
            Code = HttpResponseCodeEnum.Success,
            Message = HttpResponseCodeEnum.Success.GetEnumDescriptionByKey(),
            Datas = null
        };
    }

    /// <summary>
    /// 响应成功 200
    /// </summary>
    /// <param name="messageDatas"></param>
    /// <returns></returns>
    public static HttpResponseResult Success(string messageDatas)
    {
        return new HttpResponseResult
        {
            IsSuccess = true,
            Code = HttpResponseCodeEnum.Success,
            Message = HttpResponseCodeEnum.Success.GetEnumDescriptionByKey(),
            Datas = messageDatas
        };
    }

    /// <summary>
    /// 响应成功，返回通用数据 200
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static HttpResponseResult Success(dynamic data)
    {
        return new HttpResponseResult
        {
            IsSuccess = true,
            Code = HttpResponseCodeEnum.Success,
            Message = HttpResponseCodeEnum.Success.GetEnumDescriptionByKey(),
            Datas = data
        };
    }

    /// <summary>
    /// 响应失败，访问出错 400
    /// </summary>
    /// <param name="messageDatas"></param>
    /// <returns></returns>
    public static HttpResponseResult BadRequest(string messageDatas)
    {
        return new HttpResponseResult
        {
            IsSuccess = false,
            Code = HttpResponseCodeEnum.BadRequest,
            Message = HttpResponseCodeEnum.BadRequest.GetEnumDescriptionByKey(),
            Datas = messageDatas
        };
    }

    /// <summary>
    /// 响应失败，访问出错 400
    /// </summary>
    /// <param name="errorDatas"></param>
    /// <returns></returns>
    public static HttpResponseResult BadRequest(dynamic errorDatas)
    {
        return new HttpResponseResult
        {
            IsSuccess = false,
            Code = HttpResponseCodeEnum.BadRequest,
            Message = HttpResponseCodeEnum.BadRequest.GetEnumDescriptionByKey(),
            Datas = errorDatas
        };
    }

    /// <summary>
    /// 响应失败，访问未授权 401
    /// </summary>
    /// <returns></returns>
    public static HttpResponseResult Unauthorized()
    {
        return new HttpResponseResult
        {
            IsSuccess = false,
            Code = HttpResponseCodeEnum.Unauthorized,
            Message = HttpResponseCodeEnum.Unauthorized.GetEnumDescriptionByKey(),
            Datas = null
        };
    }

    /// <summary>
    /// 响应失败，访问未授权 401
    /// </summary>
    /// <param name="messageDatas"></param>
    /// <returns></returns>
    public static HttpResponseResult Unauthorized(string messageDatas)
    {
        return new HttpResponseResult
        {
            IsSuccess = false,
            Code = HttpResponseCodeEnum.Unauthorized,
            Message = HttpResponseCodeEnum.Unauthorized.GetEnumDescriptionByKey(),
            Datas = messageDatas
        };
    }

    /// <summary>
    /// 响应失败，内容禁止访问 403
    /// </summary>
    /// <returns></returns>
    public static HttpResponseResult Forbidden()
    {
        return new HttpResponseResult
        {
            IsSuccess = false,
            Code = HttpResponseCodeEnum.Forbidden,
            Message = HttpResponseCodeEnum.Forbidden.GetEnumDescriptionByKey(),
            Datas = null
        };
    }

    /// <summary>
    /// 响应失败，数据未找到 404
    /// </summary>
    /// <returns></returns>
    public static HttpResponseResult NotFound()
    {
        return new HttpResponseResult
        {
            IsSuccess = false,
            Code = HttpResponseCodeEnum.NotFound,
            Message = HttpResponseCodeEnum.NotFound.GetEnumDescriptionByKey(),
            Datas = null
        };
    }

    /// <summary>
    /// 响应失败，数据未找到 404
    /// </summary>
    /// <param name="messageDatas"></param>
    /// <returns></returns>
    public static HttpResponseResult NotFound(string messageDatas)
    {
        return new HttpResponseResult
        {
            IsSuccess = false,
            Code = HttpResponseCodeEnum.NotFound,
            Message = HttpResponseCodeEnum.NotFound.GetEnumDescriptionByKey(),
            Datas = messageDatas
        };
    }

    /// <summary>
    ///  响应失败，参数不合法 422
    /// </summary>
    /// <returns></returns>
    public static HttpResponseResult UnprocessableEntity()
    {
        return new HttpResponseResult
        {
            IsSuccess = false,
            Code = HttpResponseCodeEnum.UnprocessableEntity,
            Message = HttpResponseCodeEnum.UnprocessableEntity.GetEnumDescriptionByKey(),
            Datas = null
        };
    }

    /// <summary>
    ///  响应失败，参数不合法 422
    /// </summary>
    /// <param name="messageDatas"></param>
    /// <returns></returns>
    public static HttpResponseResult UnprocessableEntity(string messageDatas)
    {
        return new HttpResponseResult
        {
            IsSuccess = false,
            Code = HttpResponseCodeEnum.UnprocessableEntity,
            Message = HttpResponseCodeEnum.UnprocessableEntity.GetEnumDescriptionByKey(),
            Datas = messageDatas
        };
    }

    /// <summary>
    /// 响应失败，参数不合法 422
    /// </summary>
    /// <param name="errorDatas"></param>
    /// <returns></returns>
    public static HttpResponseResult UnprocessableEntity(dynamic errorDatas)
    {
        return new HttpResponseResult
        {
            IsSuccess = false,
            Code = HttpResponseCodeEnum.UnprocessableEntity,
            Message = HttpResponseCodeEnum.UnprocessableEntity.GetEnumDescriptionByKey(),
            Datas = errorDatas
        };
    }

    /// <summary>
    /// 并发请求过多 429
    /// </summary>
    /// <param name="messageDatas"></param>
    /// <returns></returns>
    public static HttpResponseResult TooManyRequests(string messageDatas)
    {
        return new HttpResponseResult
        {
            IsSuccess = false,
            Code = HttpResponseCodeEnum.TooManyRequests,
            Message = HttpResponseCodeEnum.TooManyRequests.GetEnumDescriptionByKey(),
            Datas = messageDatas
        };
    }

    /// <summary>
    /// 响应出错，服务器内部错误 500
    /// </summary>
    /// <returns></returns>
    public static HttpResponseResult InternalServerError()
    {
        return new HttpResponseResult
        {
            IsSuccess = false,
            Code = HttpResponseCodeEnum.InternalServerError,
            Message = HttpResponseCodeEnum.InternalServerError.GetEnumDescriptionByKey(),
            Datas = null
        };
    }

    /// <summary>
    /// 响应出错，服务器内部错误 500
    /// </summary>
    /// <param name="errorDatas"></param>
    /// <returns></returns>
    public static HttpResponseResult InternalServerError(string errorDatas)
    {
        return new HttpResponseResult
        {
            IsSuccess = false,
            Code = HttpResponseCodeEnum.InternalServerError,
            Message = HttpResponseCodeEnum.InternalServerError.GetEnumDescriptionByKey(),
            Datas = errorDatas
        };
    }

    /// <summary>
    /// 响应出错，功能未实施 501
    /// </summary>
    /// <returns></returns>
    public static HttpResponseResult NotImplemented()
    {
        return new HttpResponseResult
        {
            IsSuccess = false,
            Code = HttpResponseCodeEnum.NotImplemented,
            Message = HttpResponseCodeEnum.NotImplemented.GetEnumDescriptionByKey(),
            Datas = null
        };
    }
}