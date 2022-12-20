﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:TestController
// Guid:845e3ab1-519a-407f-bd95-1204e9506dbd
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-17 上午 04:42:29
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZhaiFanhuaBlog.Api.Controllers.Bases;
using ZhaiFanhuaBlog.Extensions.Bases.Response.Results;
using ZhaiFanhuaBlog.Extensions.Common.Swagger;
using ZhaiFanhuaBlog.Extensions.Filters;
using ZhaiFanhuaBlog.Extensions.Response;
using ZhaiFanhuaBlog.Infrastructure.App.Setting;
using ZhaiFanhuaBlog.Utils.Encryptions;
using ZhaiFanhuaBlog.Utils.Info;

namespace ZhaiFanhuaBlog.Api.Controllers.Test;

/// <summary>
/// 系统测试
/// <code>包含：工具/客户端信息/IP信息/授权信息</code>
/// </summary>
[ApiGroup(ApiGroupNames.Test)]
public class TestController : BaseApiController
{
    private readonly IHttpContextAccessor _IHttpContextAccessor;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iHttpContextAccessor"></param>
    public TestController(IHttpContextAccessor iHttpContextAccessor)
    {
        _IHttpContextAccessor = iHttpContextAccessor;
    }

    /// <summary>
    /// 客户端信息
    /// </summary>
    /// <returns></returns>
    [HttpGet("ClientInfo")]
    public ActionResult<BaseResultDto> ClientInfo()
    {
        // 获取 HttpContext 和 HttpRequest 对象
        var httpContext = _IHttpContextAccessor.HttpContext!;
        HttpContextHelper clientInfoHelper = new(httpContext);
        return BaseResponseDto.OK(clientInfoHelper);
    }

    /// <summary>
    /// 过时
    /// </summary>
    /// <returns></returns>
    [Obsolete]
    [HttpPost("Obsolete")]
    public string Obsolete()
    {
        return "过时接口";
    }

    /// <summary>
    /// 授权
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpPost("Authorize")]
    public string Authorize()
    {
        return "授权接口";
    }

    /// <summary>
    /// 未实现或异常
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet]
    [Route("Exception")]
    public string Exception()
    {
        throw new NotImplementedException("这是一个未实现或异常的接口");
    }

    /// <summary>
    /// 日志
    /// </summary>
    /// <param name="log"></param>
    /// <returns></returns>
    [HttpGet("LogInfo")]
    [TypeFilter(typeof(ActionFilterAsyncAttribute))]
    public ActionResult<BaseResultDto> LogInfo(string log)
    {
        return BaseResponseDto.OK($"测试日志写入:{log}");
    }

    /// <summary>
    /// 工具类加密
    /// </summary>
    /// <param name="encryptType">加密类型</param>
    /// <param name="iStr">待加密字符串</param>
    /// <returns></returns>
    [HttpGet("Encrypt")]
    public ActionResult<BaseResultDto> Encrypt(string encryptType, string iStr)
    {
        if (string.IsNullOrEmpty(iStr))
        {
            return BaseResponseDto.BadRequest("请输入待加密字符串");
        }
        string resultString = encryptType switch
        {
            "SHA1" => SHAHelper.EncryptSHA1(Encoding.UTF8, iStr),
            "SHA256" => SHAHelper.EncryptSHA256(Encoding.UTF8, iStr),
            "SHA384" => SHAHelper.EncryptSHA384(Encoding.UTF8, iStr),
            "SHA512" => SHAHelper.EncryptSHA512(Encoding.UTF8, iStr),
            _ => SHAHelper.EncryptSHA1(Encoding.UTF8, iStr),
        };
        return BaseResponseDto.OK(encryptType + "加密后结果为【" + resultString + "】");
    }

    /// <summary>
    /// 工具类加密或解密
    /// </summary>
    /// <param name="iEncryptOrDecrypt">选择加密或解密</param>
    /// <param name="iType">待加密解密方式</param>
    /// <param name="iStr">待加密解密字符串</param>
    /// <returns></returns>
    [HttpGet("EncryptOrDecrypt")]
    public ActionResult<BaseResultDto> EncryptOrDecrypt(string iEncryptOrDecrypt, string iType, string iStr)
    {
        if (string.IsNullOrEmpty(iStr))
        {
            return BaseResponseDto.BadRequest("请输入待加密或解密字符串");
        }
        string aesKey = AppSettings.Encryptions.AesKey.Get();
        string desKey = AppSettings.Encryptions.DesKey.Get();
        string resultString = iEncryptOrDecrypt switch
        {
            "Encrypt" => "加密后结果为【" + Encrypt() + "】",
            "Decrypt" => "解密后结果为【" + Decrypt() + "】",
            _ => "加密后结果为【" + Encrypt() + "】",
        };
        string Encrypt()
        {
            return iType switch
            {
                "DES" => DESHelper.EncryptDES(Encoding.UTF8, desKey, iStr),
                "AES" => AESHelper.EncryptAES(Encoding.UTF8, aesKey, iStr),
                _ => DESHelper.EncryptDES(Encoding.UTF8, desKey, iStr),
            };
        }
        string Decrypt()
        {
            return iType switch
            {
                "DES" => DESHelper.DecryptDES(Encoding.UTF8, desKey, iStr),
                "AES" => AESHelper.DecryptAES(Encoding.UTF8, aesKey, iStr),
                _ => DESHelper.DecryptDES(Encoding.UTF8, desKey, iStr),
            };
        }
        return BaseResponseDto.OK("你选择了" + iEncryptOrDecrypt + "," + resultString);
    }

    /// <summary>
    /// 资源过滤器
    /// </summary>
    /// <returns></returns>
    [HttpGet("ResourceFilterAttribute")]
    [TypeFilter(typeof(ResourceFilterAsyncAttribute))]
    public ActionResult<BaseResultDto> ResourceFilterAttribute()
    {
        return BaseResponseDto.OK(DateTime.Now);
    }

    /// <summary>
    /// 异步资源过滤器
    /// </summary>
    /// <returns></returns>
    [HttpGet("ResourceFilterAsyncAttribute")]
    [TypeFilter(typeof(ResourceFilterAsyncAttribute))]
    public ActionResult<BaseResultDto> ResourceFilterAsyncAttribute()
    {
        return BaseResponseDto.OK(DateTime.Now);
    }
}