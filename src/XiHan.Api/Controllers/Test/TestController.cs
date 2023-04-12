#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:TestController
// Guid:845e3ab1-519a-407f-bd95-1204e9506dbd
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-17 上午 04:42:29
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text;
using XiHan.Api.Controllers.Bases;
using XiHan.Extensions.Common.Swagger;
using XiHan.Extensions.Filters;
using XiHan.Infrastructure.Apps.Setting;
using XiHan.Infrastructure.Contexts;
using XiHan.Infrastructure.Contexts.Results;
using XiHan.Utils.Consoles;
using XiHan.Utils.Encryptions;
using XiHan.Utils.Formats;
using XiHan.Utils.Infos;
using XiHan.Utils.Infos.BaseInfos;
using XiHan.Utils.Serializes;

namespace XiHan.Api.Controllers.Test;

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
        HttpContexInfotHelper clientInfoHelper = new(httpContext);
        return BaseResponseDto.Ok(clientInfoHelper);
    }

    /// <summary>
    /// 服务端信息
    /// </summary>
    /// <returns></returns>
    [HttpGet("ServerInfo")]
    public ActionResult<BaseResultDto> ServerInfo()
    {
        var info =
        "==============================系统信息==============================" + Environment.NewLine +
        $@"操作系统：{SystemInfoHelper.OperatingSystem}" + Environment.NewLine +
        $@"系统描述：{SystemInfoHelper.OsDescription}" + Environment.NewLine +
        $@"系统版本：{SystemInfoHelper.OsVersion}" + Environment.NewLine +
        $@"系统平台：{SystemInfoHelper.Platform}" + Environment.NewLine +
        $@"系统架构：{SystemInfoHelper.OsArchitecture}" + Environment.NewLine +
        $@"系统目录：{SystemInfoHelper.SystemDirectory}" + Environment.NewLine +
        $@"运行时间：{SystemInfoHelper.RunningTime}" + Environment.NewLine +
        $@"交互模式：{SystemInfoHelper.InteractiveMode}" + Environment.NewLine +
        $@"处理器信息：{SystemInfoHelper.CpuInfo.SerializeToJson()}" + Environment.NewLine +
        $@"内存信息：{SystemInfoHelper.RamInfo.SerializeToJson()}" + Environment.NewLine +
        $@"磁盘信息：{SystemInfoHelper.DiskInfo.SerializeToJson()}" + Environment.NewLine +
        $@"IPv4地址：{LocalIpHelper.GetLocalIpV4()}" + Environment.NewLine +
        $@"IPv6地址：{LocalIpHelper.GetLocalIpV6()}" + Environment.NewLine +
        "==============================环境信息==============================" + Environment.NewLine +
        $@"环境框架：{EnvironmentInfoHelper.FrameworkDescription}" + Environment.NewLine +
        $@"环境版本：{EnvironmentInfoHelper.EnvironmentVersion}" + Environment.NewLine +
        $@"环境架构：{EnvironmentInfoHelper.ProcessArchitecture}" + Environment.NewLine +
        $@"环境标识：{EnvironmentInfoHelper.RuntimeIdentifier}" + Environment.NewLine +
        $@"机器名称：{EnvironmentInfoHelper.MachineName}" + Environment.NewLine +
        $@"用户域名：{EnvironmentInfoHelper.UserDomainName}" + Environment.NewLine +
        $@"关联用户：{EnvironmentInfoHelper.UserName}" + Environment.NewLine +
        "==============================应用信息==============================" + Environment.NewLine +
        $@"应用名称：{ApplicationInfoHelper.Name(Assembly.GetExecutingAssembly())}" + Environment.NewLine +
        $@"应用版本：{ApplicationInfoHelper.Version(Assembly.GetExecutingAssembly())}" + Environment.NewLine +
        $@"所在路径：{ApplicationInfoHelper.CurrentDirectory}" + Environment.NewLine +
        $@"运行文件：{ApplicationInfoHelper.ProcessPath}" + Environment.NewLine +
        $@"当前进程：{ApplicationInfoHelper.CurrentProcessId}" + Environment.NewLine +
        $@"会话标识：{ApplicationInfoHelper.CurrentProcessSessionId}" + Environment.NewLine +
        $@"占用空间：{FormatFileSizeExtensions.FormatByteToString(DiskHelper.GetDirectorySize(ApplicationInfoHelper.CurrentDirectory))}" + Environment.NewLine +
        $@"启动环境：{AppSettings.EnvironmentName.GetValue()}" + Environment.NewLine +
        $@"启动端口：{AppSettings.Port.GetValue()}" + Environment.NewLine +
        "==============================配置信息==============================" + Environment.NewLine +
        "==============数据库==============" + Environment.NewLine +
        $@"连接类型：{AppSettings.Database.Type.GetValue()}" + Environment.NewLine +
        $@"是否初始化：{AppSettings.Database.Inited.GetValue()}" + Environment.NewLine +
        "===============分析===============" + Environment.NewLine +
        $@"是否启用：{AppSettings.Miniprofiler.IsEnabled.GetValue()}" + Environment.NewLine +
        "===============缓存===============" + Environment.NewLine +
        $@"内存式缓存：默认启用；缓存时常：{AppSettings.Cache.SyncTimeout.GetValue()}分钟" + Environment.NewLine +
        $@"分布式缓存：{AppSettings.Cache.Distributedcache.IsEnabled.GetValue()}" + Environment.NewLine +
        $@"响应式缓存：{AppSettings.Cache.ResponseCache.IsEnabled.GetValue()}" + Environment.NewLine +
        "===============跨域===============" + Environment.NewLine +
        $@"是否启用：{AppSettings.Cors.IsEnabled.GetValue()}" + Environment.NewLine +
        "===============日志===============" + Environment.NewLine +
        $@"授权日志：{AppSettings.LogConfig.Authorization.GetValue()}" + Environment.NewLine +
        $@"资源日志：{AppSettings.LogConfig.Resource.GetValue()}" + Environment.NewLine +
        $@"请求日志：{AppSettings.LogConfig.Action.GetValue()}" + Environment.NewLine +
        $@"结果日志：{AppSettings.LogConfig.Result.GetValue()}" + Environment.NewLine +
        $@"异常日志：{AppSettings.LogConfig.Exception.GetValue()}" + Environment.NewLine +
        "==============================任务信息==============================";
        info.WriteLineError();
        return BaseResponseDto.Ok(info);
    }

    /// <summary>
    /// 过时
    /// </summary>
    /// <returns></returns>
    [Obsolete]
    [HttpGet("Obsolete")]
    public string Obsolete()
    {
        return "过时接口";
    }

    /// <summary>
    /// 授权
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet("Authorize")]
    public string Authorize()
    {
        return "授权接口";
    }

    /// <summary>
    /// 未实现或异常
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet("Exception")]
    public string Exception()
    {
        throw new NotImplementedException("这是一个未实现或异常的接口");
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
        var resultString = encryptType switch
        {
            "SHA1" => iStr.Sha1Encrypt(Encoding.UTF8),
            "SHA256" => iStr.Sha256Encrypt(Encoding.UTF8),
            "SHA384" => iStr.Sha384Encrypt(Encoding.UTF8),
            "SHA512" => iStr.Sha512Encrypt(Encoding.UTF8),
            _ => iStr.Sha1Encrypt(Encoding.UTF8),
        };
        return BaseResponseDto.Ok(encryptType + "加密后结果为【" + resultString + "】");
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
        var aesKey = AppSettings.Encryptions.AesKey.GetValue();
        var desKey = AppSettings.Encryptions.DesKey.GetValue();
        var resultString = iEncryptOrDecrypt switch
        {
            "Encrypt" => "加密后结果为【" + Encrypt() + "】",
            "Decrypt" => "解密后结果为【" + Decrypt() + "】",
            _ => "加密后结果为【" + Encrypt() + "】",
        };
        string Encrypt()
        {
            return iType switch
            {
                "DES" => iStr.DesEncrypt(Encoding.UTF8, desKey),
                "AES" => iStr.AesEncrypt(Encoding.UTF8, aesKey),
                _ => iStr.DesEncrypt(Encoding.UTF8, desKey),
            };
        }
        string Decrypt()
        {
            return iType switch
            {
                "DES" => iStr.DesDecrypt(Encoding.UTF8, desKey),
                "AES" => iStr.AesDecrypt(Encoding.UTF8, aesKey),
                _ => iStr.DesDecrypt(Encoding.UTF8, desKey),
            };
        }
        return BaseResponseDto.Ok("你选择了" + iEncryptOrDecrypt + "," + resultString);
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
        return BaseResponseDto.Ok($"测试日志写入:{log}");
    }

    /// <summary>
    /// 资源过滤器
    /// </summary>
    /// <returns></returns>
    [HttpGet("ResourceFilterAttribute")]
    [TypeFilter(typeof(ResourceFilterAsyncAttribute))]
    public ActionResult<BaseResultDto> ResourceFilterAttribute()
    {
        return BaseResponseDto.Ok(DateTime.Now);
    }

    /// <summary>
    /// 异步资源过滤器
    /// </summary>
    /// <returns></returns>
    [HttpGet("ResourceFilterAsyncAttribute")]
    [TypeFilter(typeof(ResourceFilterAsyncAttribute))]
    public ActionResult<BaseResultDto> ResourceFilterAsyncAttribute()
    {
        return BaseResponseDto.Ok(DateTime.Now);
    }
}