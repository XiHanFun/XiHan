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
using ZhaiFanhuaBlog.Utils.Encryptions;
using ZhaiFanhuaBlog.ViewModels.Bases.Results;
using ZhaiFanhuaBlog.ViewModels.Response;
using ZhaiFanhuaBlog.WebApi.Common.Extensions.Swagger;
using ZhaiFanhuaBlog.WebApi.Common.Filters;

namespace ZhaiFanhuaBlog.WebApi.Controllers.Test;

/// <summary>
/// 测试接口
/// </summary>
[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = SwaggerGroup.Test)]
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
    /// 获取客户端信息
    /// </summary>
    /// <returns></returns>
    [HttpGet("ClientInfo")]
    public ActionResult<BaseResultDto> ClientInfo()
    {
        // 获取 HttpContext 和 HttpRequest 对象
        var httpContext = _IHttpContextAccessor.HttpContext;
        // 获取客户端 Ip 地址
        var ip = httpContext?.Connection.RemoteIpAddress == null ? string.Empty : httpContext.Connection.RemoteIpAddress.ToString();
        string userAgent = httpContext?.Request.Headers == null ? string.Empty : httpContext.Request.Headers.UserAgent.ToString();

        string osVersion = "未知";

        if (userAgent.Contains("NT 10.0"))
        {
            osVersion = "Windows 10";
        }
        else if (userAgent.Contains("NT 6.3"))
        {
            osVersion = "Windows 8.1";
        }
        else if (userAgent.Contains("NT 6.2"))
        {
            osVersion = "Windows 8";
        }
        else if (userAgent.Contains("NT 6.1"))
        {
            osVersion = "Windows 7";
        }
        else if (userAgent.Contains("NT 6.1"))
        {
            osVersion = "Windows 7";
        }
        else if (userAgent.Contains("NT 6.0"))
        {
            osVersion = "Windows Vista/Server 2008";
        }
        else if (userAgent.Contains("NT 5.2"))
        {
            if (userAgent.Contains("64"))
                osVersion = "Windows XP";
            else
                osVersion = "Windows Server 2003";
        }
        else if (userAgent.Contains("NT 5.1"))
        {
            osVersion = "Windows XP";
        }
        else if (userAgent.Contains("NT 5"))
        {
            osVersion = "Windows 2000";
        }
        else if (userAgent.Contains("NT 4"))
        {
            osVersion = "Windows NT4";
        }
        else if (userAgent.Contains("Me"))
        {
            osVersion = "Windows Me";
        }
        else if (userAgent.Contains("98"))
        {
            osVersion = "Windows 98";
        }
        else if (userAgent.Contains("95"))
        {
            osVersion = "Windows 95";
        }
        else if (userAgent.Contains("Mac"))
        {
            osVersion = "Mac";
        }
        else if (userAgent.Contains("Unix"))
        {
            osVersion = "UNIX";
        }
        else if (userAgent.Contains("Linux"))
        {
            osVersion = "Linux";
        }
        else if (userAgent.Contains("SunOS"))
        {
            osVersion = "SunOS";
        }
        userAgent = userAgent.ToLower();
        string browser = "未知";
        if (userAgent.Contains("opera/ucweb"))
            browser = "UC Opera";
        else if (userAgent.Contains("openwave/ ucweb"))
            browser = "UCOpenwave";
        else if (userAgent.Contains("ucweb"))
            browser = "UC";
        else if (userAgent.Contains("360se"))
            browser = "360";
        else if (userAgent.Contains("metasr"))
            browser = "搜狗";
        else if (userAgent.Contains("maxthon"))
            browser = "遨游";
        else if (userAgent.Contains("the world"))
            browser = "世界之窗";
        else if (userAgent.Contains("tencenttraveler") || userAgent.Contains("qqbrowser"))
            browser = "腾讯";
        else if (userAgent.Contains("chrome"))
            browser = "Chrome";
        else if (userAgent.Contains("safari"))
            browser = "safari";
        else if (userAgent.Contains("firefox"))
            browser = "Firefox";
        else if (userAgent.Contains("opera"))
            browser = "Opera";
        else if (userAgent.Contains("msie"))
            browser = "IE";

        return BaseResponseDto.OK($"ip:{ip},os:{osVersion},browser:{browser},userAgent:{userAgent}"); ;
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
    [TypeFilter(typeof(CustomResourceFilterAsyncAttribute))]
    public ActionResult<BaseResultDto> ResourceFilterAttribute()
    {
        return BaseResponseDto.OK(DateTime.Now);
    }

    /// <summary>
    /// 测试工具类异步资源过滤器属性
    /// </summary>
    /// <returns></returns>
    [HttpGet("ResourceFilterAsyncAttribute")]
    [TypeFilter(typeof(CustomResourceFilterAsyncAttribute))]
    public ActionResult<BaseResultDto> ResourceFilterAsyncAttribute()
    {
        return BaseResponseDto.OK(DateTime.Now);
    }
}