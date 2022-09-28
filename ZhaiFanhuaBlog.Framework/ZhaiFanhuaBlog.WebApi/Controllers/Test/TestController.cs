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
using ZhaiFanhuaBlog.Extensions.Common.Swagger;
using ZhaiFanhuaBlog.Extensions.Filters;
using ZhaiFanhuaBlog.Utils.Encryptions;
using ZhaiFanhuaBlog.Utils.Info;
using ZhaiFanhuaBlog.ViewModels.Bases.Results;
using ZhaiFanhuaBlog.ViewModels.Response;
using ZhaiFanhuaBlog.WebApi.Controllers.Bases;

namespace ZhaiFanhuaBlog.WebApi.Controllers.Test;

/// <summary>
/// 系统测试
/// <code>包含：测试工具/客户端信息/IP信息</code>
/// </summary>
[AllowAnonymous]
[ApiExplorerSettings(GroupName = SwaggerGroup.Test)]
public class TestController : BaseApiController
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
    /// 获取客户端信息
    /// </summary>
    /// <returns></returns>
    [HttpGet("ClientInfo")]
    public ActionResult<BaseResultDto> ClientInfo()
    {
        // 获取 HttpContext 和 HttpRequest 对象
        var httpContext = _IHttpContextAccessor.HttpContext!;
        ClientInfoHelper clientInfoHelper = new(httpContext);
        //string ip = "60.163.239.151";
        //string datatype = "jsonp";
        //string token = "d09f8d316fcfdbe68108cab08cb8bd0d";
        //var result = HttpHelper.GetAsync($@"http://api.ip138.com/ip/?ip={ip}&datatype={datatype}&token={token}");
        //var result = HttpHelper.PostAsync();
        return BaseResponseDto.OK(clientInfoHelper);
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
    /// 未实现的异常接口
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet]
    [Route("Exception")]
    public string Exception()
    {
        throw new NotImplementedException("这是一个未实现的异常接口");
    }

    /// <summary>
    /// 测试日志
    /// </summary>
    /// <param name="iLog"></param>
    /// <returns></returns>
    [HttpGet("Log")]
    [TypeFilter(typeof(ActionFilterAsyncAttribute))]
    public ActionResult<BaseResultDto> TestLog(string iLog)
    {
        return BaseResponseDto.OK($"测试日志写入:{iLog}");
    }

    /// <summary>
    /// 测试工具类加密
    /// </summary>
    /// <param name="encryptType">加密类型</param>
    /// <param name="iStr">待加密字符串</param>
    /// <returns></returns>
    [HttpGet("Encrypt")]
    public ActionResult<BaseResultDto> Encrypt(string encryptType, string iStr)
    {
        if (string.IsNullOrEmpty(iStr)) return BaseResponseDto.BadRequest("请输入待加密字符串");
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
    /// 测试工具类加密或解密
    /// </summary>
    /// <param name="iEncryptOrDecrypt">选择加密或解密</param>
    /// <param name="iType">待加密解密方式</param>
    /// <param name="iStr">待加密解密字符串</param>
    /// <returns></returns>
    [HttpGet("EncryptOrDecrypt")]
    public ActionResult<BaseResultDto> EncryptOrDecrypt(string iEncryptOrDecrypt, string iType, string iStr)
    {
        if (string.IsNullOrEmpty(iStr)) return BaseResponseDto.BadRequest("请输入待加密或解密字符串");
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
        return BaseResponseDto.OK("你选择了" + iEncryptOrDecrypt + "," + resultString);
    }

    /// <summary>
    /// 测试工具类资源过滤器属性
    /// </summary>
    /// <returns></returns>
    [HttpGet("ResourceFilterAttribute")]
    [TypeFilter(typeof(ResourceFilterAsyncAttribute))]
    public ActionResult<BaseResultDto> ResourceFilterAttribute()
    {
        return BaseResponseDto.OK(DateTime.Now);
    }

    /// <summary>
    /// 测试工具类异步资源过滤器属性
    /// </summary>
    /// <returns></returns>
    [HttpGet("ResourceFilterAsyncAttribute")]
    [TypeFilter(typeof(ResourceFilterAsyncAttribute))]
    public ActionResult<BaseResultDto> ResourceFilterAsyncAttribute()
    {
        return BaseResponseDto.OK(DateTime.Now);
    }
}