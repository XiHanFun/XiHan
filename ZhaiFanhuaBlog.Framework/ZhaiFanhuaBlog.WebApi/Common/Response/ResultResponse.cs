// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ResultResponse
// Guid:3874a6ae-1ebc-49ab-b4a3-55e76dc68945
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 11:46:50
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Utils.Summary;
using ZhaiFanhuaBlog.WebApi.Common.Response.Enum;
using ZhaiFanhuaBlog.WebApi.Common.Response.Model;

namespace ZhaiFanhuaBlog.WebApi.Common.Response;

/// <summary>
/// 请求信息
/// </summary>
public class ResultResponse
{
    /// <summary>
    /// 请求成功 200
    /// </summary>
    /// <returns></returns>
    public static ResultModel Continue()
    {
        return new ResultModel
        {
            Success = true,
            Code = ResultCode.Continue,
            Message = EnumDescriptionHelper.GetEnumDescription(ResultCode.Continue),
            Data = null
        };
    }

    /// <summary>
    /// 请求成功 200
    /// </summary>
    /// <returns></returns>
    public static ResultModel OK()
    {
        return new ResultModel
        {
            Success = true,
            Code = ResultCode.OK,
            Message = EnumDescriptionHelper.GetEnumDescription(ResultCode.OK),
            Data = null
        };
    }

    /// <summary>
    /// 请求成功 200
    /// </summary>
    /// <returns></returns>
    public static ResultModel OK(string messageData)
    {
        return new ResultModel
        {
            Success = true,
            Code = ResultCode.OK,
            Message = EnumDescriptionHelper.GetEnumDescription(ResultCode.OK),
            Data = messageData
        };
    }

    /// <summary>
    /// 请求成功，返回通用数据 200
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static ResultModel OK(dynamic data)
    {
        return new ResultModel
        {
            Success = true,
            Code = ResultCode.OK,
            Message = EnumDescriptionHelper.GetEnumDescription(ResultCode.OK),
            Data = data
        };
    }

    /// <summary>
    /// 请求成功，返回分页数据 200
    /// </summary>
    /// <param name="pageIndex">当前页标</param>
    /// <param name="pageSize">每页大小</param>
    /// <param name="totalCount">总页数</param>
    /// <param name="dataCount">数据总数</param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static ResultModel OK(int pageIndex, int pageSize, int totalCount, int dataCount, List<dynamic> data)
    {
        return new ResultModel
        {
            Success = true,
            Code = ResultCode.OK,
            Message = EnumDescriptionHelper.GetEnumDescription(ResultCode.OK),
            Data = new PageModel
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount,
                DataCount = dataCount,
                Data = data
            }
        };
    }

    /// <summary>
    /// 请求成功，返回表格数据 200
    /// </summary>
    /// <param name="dataCount"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static ResultModel OK(int dataCount, List<dynamic> data)
    {
        return new ResultModel
        {
            Success = true,
            Code = ResultCode.OK,
            Message = EnumDescriptionHelper.GetEnumDescription(ResultCode.OK),
            Data = new TableModel
            {
                DataCount = dataCount,
                Data = data
            }
        };
    }

    /// <summary>
    /// 请求失败，访问出错 400
    /// </summary>
    /// <returns></returns>
    public static ResultModel BadRequest(string messageData)
    {
        return new ResultModel
        {
            Success = false,
            Code = ResultCode.BadRequest,
            Message = EnumDescriptionHelper.GetEnumDescription(ResultCode.BadRequest),
            Data = messageData
        };
    }

    /// <summary>
    /// 请求失败，访问未授权 401
    /// </summary>
    /// <returns></returns>
    public static ResultModel Unauthorized()
    {
        return new ResultModel
        {
            Success = false,
            Code = ResultCode.Unauthorized,
            Message = EnumDescriptionHelper.GetEnumDescription(ResultCode.Unauthorized),
            Data = null
        };
    }

    /// <summary>
    /// 请求失败，内容禁止访问 403
    /// </summary>
    /// <returns></returns>
    public static ResultModel Forbidden()
    {
        return new ResultModel
        {
            Success = false,
            Code = ResultCode.Forbidden,
            Message = EnumDescriptionHelper.GetEnumDescription(ResultCode.Forbidden),
            Data = null
        };
    }

    /// <summary>
    /// 请求失败，数据未找到 404
    /// </summary>
    /// <returns></returns>
    public static ResultModel NotFound()
    {
        return new ResultModel
        {
            Success = false,
            Code = ResultCode.NotFound,
            Message = EnumDescriptionHelper.GetEnumDescription(ResultCode.NotFound),
            Data = null
        };
    }

    /// <summary>
    /// 请求失败，参数不合法 422
    /// </summary>
    /// <returns></returns>
    public static ResultModel UnprocessableEntity(int dataCount, List<dynamic> data)
    {
        return new ResultModel
        {
            Success = false,
            Code = ResultCode.UnprocessableEntity,
            Message = EnumDescriptionHelper.GetEnumDescription(ResultCode.UnprocessableEntity),
            Data = new DtoModel
            {
                DataCount = dataCount,
                Data = data
            }
        };
    }

    /// <summary>
    /// 请求失败，服务器内部错误 500
    /// </summary>
    /// <returns></returns>
    public static ResultModel InternalServerError()
    {
        return new ResultModel
        {
            Success = false,
            Code = ResultCode.InternalServerError,
            Message = EnumDescriptionHelper.GetEnumDescription(ResultCode.InternalServerError),
            Data = null
        };
    }

    /// <summary>
    /// 请求失败，功能未实施 501
    /// </summary>
    /// <returns></returns>
    public static ResultModel NotImplemented()
    {
        return new ResultModel
        {
            Success = false,
            Code = ResultCode.NotImplemented,
            Message = EnumDescriptionHelper.GetEnumDescription(ResultCode.NotImplemented),
            Data = null
        };
    }
}