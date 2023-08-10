#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BaseApiController
// Guid:6c522a26-5ace-4fb9-b35b-636ca94ef20e
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-09-03 上午 12:20:06
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Mvc;
using MiniExcelLibs;
using XiHan.Infrastructures.Apps.HttpContexts;
using XiHan.Infrastructures.Consts;
using XiHan.Utils.Extensions;
using XiHan.Utils.Files;
using XiHan.WebCore.Common.Swagger;

namespace XiHan.WebApi.Controllers.Bases;

/// <summary>
/// BaseApiController
/// </summary>
[ApiController]
[Route("Api/[controller]")]
[Produces("application/json")]
[ApiGroup(ApiGroupNames.All)]
public class BaseApiController : ControllerBase
{
    /// <summary>
    /// 导出文件
    /// </summary>
    /// <param name="path">完整文件路径</param>
    /// <param name="fileName">带扩展文件名</param>
    /// <returns></returns>
    protected void ExportFile(string path, string fileName)
    {
        // 创建文件流
        var fullPath = Path.Combine(GlobalConst.RootExportPath, fileName);
        var fileStream = System.IO.File.OpenRead(path);
        using MemoryStream memoryStream = new();
        HttpContext.ExportFile(memoryStream.ToArray(), ContentTypeEnum.ApplicationStream, fullPath);
    }

    /// <summary>
    /// 导出工作表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="sheetName"></param>
    /// <param name="fileName"></param>
    protected (string, string) ExportExcel<T>(List<T> list, string sheetName, string fileName)
    {
        var exportFileName = $"{fileName}.xlsx";
        var fullPath = Path.Combine(GlobalConst.RootExportPath, exportFileName);
        FileHelper.CreateDirectory(GlobalConst.RootExportPath);
        MiniExcel.SaveAs(path: fullPath, value: list, sheetName: sheetName, overwriteFile: true);
        return (exportFileName, fullPath);
    }

    /// <summary>
    /// 导出多个工作表(Sheet)
    /// </summary>
    /// <param name="sheets"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    protected (string, string) ExportExcelMini(Dictionary<string, object> sheets, string fileName)
    {
        var exportFileName = $"{fileName}.xlsx";
        var fullPath = Path.Combine(GlobalConst.RootExportPath, exportFileName);
        FileHelper.CreateDirectory(fullPath);
        MiniExcel.SaveAs(path: fullPath, value: sheets, overwriteFile: true);
        return (exportFileName, fullPath);
    }

    /// <summary>
    /// 下载导入模板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="fileName">下载文件名</param>
    /// <returns></returns>
    protected string DownloadImportTemplate<T>(List<T> list, string fileName)
    {
        var templateFileName = $"{fileName}模板.xlsx";
        var fullPath = Path.Combine(GlobalConst.RootImportTemplatePath, templateFileName);
        FileHelper.CreateDirectory(fullPath);
        MiniExcel.SaveAs(path: fullPath, value: list, overwriteFile: true);
        return fullPath;
    }

    /// <summary>
    /// 下载指定文件模板
    /// </summary>
    /// <param name="fileName">下载文件名</param>
    /// <returns></returns>
    protected (string, string) DownloadImportTemplate(string fileName)
    {
        var templateFileName = $"{fileName}模板.xlsx";
        var fullPath = Path.Combine(GlobalConst.RootImportTemplatePath, templateFileName);
        return (templateFileName, fullPath);
    }
}