#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:TestController
// Guid:845e3ab1-519a-407f-bd95-1204e9506dbd
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-06-17 上午 04:42:29
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using XiHan.Infrastructures.Apps;
using XiHan.Infrastructures.Apps.HttpContexts;
using XiHan.Infrastructures.Responses;
using XiHan.Models.Syses;
using XiHan.Models.Syses.SeedData;
using XiHan.Models.Syses.SeedDatas;
using XiHan.Utils.Extensions;
using XiHan.Utils.Serializes;
using XiHan.WebHost.Controllers.Bases;
using XiHan.WebCore.Common.Swagger;
using XiHan.WebCore.Filters;

namespace XiHan.WebHost.Controllers.Test;

/// <summary>
/// 测试管理
/// <code>包含：工具/客户端信息/IP信息/授权信息</code>
/// </summary>
[EnableCors("AllowAll")]
[ApiGroup(ApiGroupNameEnum.Test)]
public class TestController : BaseApiController
{
    /// <summary>
    /// 客户端信息
    /// </summary>
    /// <returns></returns>
    [HttpGet("ClientInfo")]
    public ApiResult ClientInfo()
    {
        return ApiResult.Success(new
        {
            App.ClientInfo,
            App.AddressInfo,
            App.AuthInfo
        });
    }

    /// <summary>
    /// 过时
    /// </summary>
    /// <returns></returns>
    [Obsolete("过时接口", true)]
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
    public string NotImplementedException()
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
    public string LogInfo(string log)
    {
        return $"测试日志写入:{log}";
    }

    /// <summary>
    /// 限流
    /// </summary>
    /// <returns></returns>
    [HttpGet("RateLimiting")]
    [EnableRateLimiting("MyPolicy")]
    public string RateLimiting()
    {
        return "测试限流";
    }

    /// <summary>
    /// 异步资源过滤器
    /// </summary>
    /// <returns></returns>
    [HttpGet("ResourceFilterAsyncAttribute")]
    [TypeFilter(typeof(ResourceFilterAsyncAttribute))]
    public string ResourceFilterAsyncAttribute()
    {
        return "异步资源过滤器" + DateTime.Now;
    }

    /// <summary>
    /// Base64加密
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    [HttpGet("Base64Encode")]
    public string Base64Encode(string password)
    {
        return password.Base64Encode();
    }

    /// <summary>
    /// Base64解密
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    [HttpGet("Base64Decode")]
    public string Base64Decode(string password)
    {
        return password.Base64Decode();
    }

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="file"></param>
    /// <returns>完整文件路径</returns>
    [HttpPost("Upload/File")]
    public new async Task<ApiResult> UploadFile(IFormFile file)
    {
        var path = await base.UploadFile(file);
        return ApiResult.Success($"上传成功！文件路径：{path}");
    }

    /// <summary>
    /// 下载文件
    /// </summary>
    /// <returns></returns>
    [HttpGet("Download/File")]
    public async Task DownloadFile()
    {
        string path = @"E:\Repository\XiHanFun\Other\架构设计\系统设计\日志管理\image-20210409150248593.png";
        string fileName = @"image-20210409150248593.png";
        await DownloadFile(fileName, path, ContentTypeEnum.ImagePng);
    }

    /// <summary>
    /// 下载导入模板
    /// </summary>
    /// <returns></returns>
    [HttpGet("Download/ImportTemplate")]
    public async Task DownloadImportTemplate()
    {
        var sysUserSeedData = new SysUserSeedData();
        var dataSource = sysUserSeedData.HasData();
        await DownloadImportTemplate<SysUser>("系统用户种子数据", dataSource, "SysUser");
    }

    /// <summary>
    /// 导入工作表
    /// </summary>
    /// <param name="file">文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns></returns>
    [HttpPost("Import/Excel")]
    public async Task<ApiResult> ImportExcel(IFormFile file, string sheetName)
    {
        var userData = await ImportExcel<SysUser>(file, sheetName);
        return ApiResult.Success(userData.SerializeToJson());
    }

    /// <summary>
    /// 导入多个工作表
    /// </summary>
    /// <param name="file">文件流</param>
    /// <returns></returns>
    [HttpPost("Import/Excel/MultipleSheets")]
    public async Task<ApiResult> ImportExcelMultipleSheets(IFormFile file)
    {
        var data = await ImportExcel(file);
        return ApiResult.Success(data.SerializeToJson());
    }

    /// <summary>
    /// 导出工作表
    /// </summary>
    /// <returns></returns>
    [HttpGet("Export/Excel")]
    public async Task ExportExcel()
    {
        var sysUserSeedData = new SysUserSeedData();
        var dataSource = sysUserSeedData.HasData();
        await ExportExcel<SysUser>("系统用户种子数据", dataSource, "SysUser");
    }

    /// <summary>
    /// 导出多个工作表
    /// </summary>
    /// <returns></returns>
    [HttpGet("Export/Excel/MultipleSheets")]
    public async Task ExportExcelMultipleSheets()
    {
        var sysUserSeedData = new SysUserSeedData();
        var sysConfigSeedData = new SysConfigSeedData();
        var dataSourceSysUser = sysUserSeedData.HasData();
        var dataSourceSysConfig = sysConfigSeedData.HasData();
        var dataSource = new Dictionary<string, object>()
        {
            {"SysUserSeedData",dataSourceSysUser },
            {"SysConfigSeedData",dataSourceSysConfig },
        };
        await ExportExcel("系统种子数据", dataSource);
    }
}