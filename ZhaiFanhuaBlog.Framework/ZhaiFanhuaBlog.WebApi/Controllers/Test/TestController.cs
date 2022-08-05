// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:TestController
// Guid:845e3ab1-519a-407f-bd95-1204e9506dbd
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-17 上午 04:42:29
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using ZhaiFanhuaBlog.Models.Bases.Response.Model;
using ZhaiFanhuaBlog.Models.Response;
using ZhaiFanhuaBlog.Utils.Encryptions;
using ZhaiFanhuaBlog.WebApi.Common.Filters;

namespace ZhaiFanhuaBlog.WebApi.Controllers.Test;

/// <summary>
/// 测试接口
/// </summary>
[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class TestController : ControllerBase
{
    private readonly IHttpContextAccessor _IHttpContextAccessor;
    private readonly IConfiguration _IConfiguration;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iHttpContextAccessor"></param>
    /// <param name="iConfiguration"></param>
    public TestController(IHttpContextAccessor iHttpContextAccessor, IConfiguration iConfiguration)
    {
        _IHttpContextAccessor = iHttpContextAccessor;
        _IConfiguration = iConfiguration;
    }

    /// <summary>
    /// 获取客户端Ip
    /// </summary>
    /// <returns></returns>
    [HttpGet("ClientIp")]
    public string? ClientIp()
    {
        return _IHttpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
    }

    /// <summary>
    /// 测试接口【过时】
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    [HttpPost("Test")]
    [Obsolete]
    public string Test(string? str)
    {
        return "测试字符串：" + str;
    }

    /// <summary>
    /// 测试日志
    /// </summary>
    /// <param name="iLog"></param>
    /// <returns></returns>
    [HttpGet("Log")]
    [TypeFilter(typeof(CustomActionFilterAsyncAttribute))]
    public ActionResult<ResultModel> TestLog(string iLog)
    {
        return ResultResponse.OK($"测试日志写入:{iLog}");
    }

    /// <summary>
    /// 测试工具类加密
    /// </summary>
    /// <param name="encryptType">加密类型</param>
    /// <param name="iStr">待加密字符串</param>
    /// <returns></returns>
    [HttpGet("Encrypt")]
    public ActionResult<ResultModel> Encrypt(string encryptType, string iStr)
    {
        if (string.IsNullOrEmpty(iStr)) return ResultResponse.BadRequest("请输入待加密字符串");
        string resultString = encryptType switch
        {
            "SHA1" => SHAHelper.EncryptSHA1(Encoding.UTF8, iStr),
            "SHA256" => SHAHelper.EncryptSHA256(Encoding.UTF8, iStr),
            "SHA384" => SHAHelper.EncryptSHA384(Encoding.UTF8, iStr),
            "SHA512" => SHAHelper.EncryptSHA512(Encoding.UTF8, iStr),
            _ => SHAHelper.EncryptSHA1(Encoding.UTF8, iStr),
        };
        return ResultResponse.OK(encryptType + "加密后结果为【" + resultString + "】");
    }

    /// <summary>
    /// 测试工具类加密或解密
    /// </summary>
    /// <param name="iEncryptOrDecrypt">选择加密或解密</param>
    /// <param name="iType">待加密解密方式</param>
    /// <param name="iStr">待加密解密字符串</param>
    /// <returns></returns>
    [HttpGet("EncryptOrDecrypt")]
    public ActionResult<ResultModel> EncryptOrDecrypt(string iEncryptOrDecrypt, string iType, string iStr)
    {
        if (string.IsNullOrEmpty(iStr)) return ResultResponse.BadRequest("请输入待加密或解密字符串");
        string aesKey = _IConfiguration.GetValue<string>("Encryptions:AesKey");
        string desKey = _IConfiguration.GetValue<string>("Encryptions:DesKey");
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
        return ResultResponse.OK("你选择了" + iEncryptOrDecrypt + "," + resultString);
    }

    /// <summary>
    /// 测试工具类资源过滤器属性
    /// </summary>
    /// <returns></returns>
    [HttpGet("ResourceFilterAttribute")]
    [TypeFilter(typeof(CustomResourceFilterAsyncAttribute))]
    public ActionResult<ResultModel> ResourceFilterAttribute()
    {
        return ResultResponse.OK(DateTime.Now);
    }

    /// <summary>
    /// 测试工具类异步资源过滤器属性
    /// </summary>
    /// <returns></returns>
    [HttpGet("ResourceFilterAsyncAttribute")]
    [TypeFilter(typeof(CustomResourceFilterAsyncAttribute))]
    public ActionResult<ResultModel> ResourceFilterAsyncAttribute()
    {
        return ResultResponse.OK(DateTime.Now);
    }
}