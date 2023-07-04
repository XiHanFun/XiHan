#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BaseApiController
// Guid:6c522a26-5ace-4fb9-b35b-636ca94ef20e
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-03 上午 12:20:06
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Mvc;
using MiniExcelLibs;
using System.Web;
using XiHan.Application.Common.Swagger;
using XiHan.Infrastructures.Infos;

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
    private string _rootPath = ApplicationInfoHelper.CurrentDirectory;

    ///// <summary>
    ///// 导出Excel
    ///// </summary>
    ///// <param name="path">完整文件路径</param>
    ///// <param name="fileName">带扩展文件名</param>
    ///// <returns></returns>
    //protected FileStreamResult ExportExcel(string path, string fileName)
    //{
    //    // 创建文件流
    //    Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
    //    var stream = System.IO.File.OpenRead(path);
    //    var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    //    return File(stream, mimeType, HttpUtility.UrlEncode(fileName));
    //}

    ///// <summary>
    ///// 导出Excel
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="list"></param>
    ///// <param name="sheetName"></param>
    ///// <param name="fileName"></param>
    //protected (string, string) ExportExcel<T>(List<T> list, string sheetName, string fileName)
    //{
    //    return ExportExcelMini(list, sheetName, fileName);
    //}

    ///// <summary>
    /////
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="list"></param>
    ///// <param name="sheetName"></param>
    ///// <param name="fileName"></param>
    ///// <returns></returns>
    //private (string, string) ExportExcelMini<T>(List<T> list, string sheetName, string fileName)
    //{
    //    var rootpath = ApplicationInfoHelper.CurrentDirectory;
    //    var sFileName = $"{fileName}{DateTime.Now:MM-dd-HHmmss}.xlsx";
    //    var fullPath = Path.Combine(rootpath, "export", sFileName);

    //    Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

    //    MiniExcel.SaveAs(fullPath, list, sheetName: sheetName);
    //    return (sFileName, fullPath);
    //}

    ///// <summary>
    ///// 导出多个工作表(Sheet)
    ///// </summary>
    ///// <param name="sheets"></param>
    ///// <param name="fileName"></param>
    ///// <returns></returns>
    //protected (string, string) ExportExcelMini(Dictionary<string, object> sheets, string fileName)
    //{
    //    var sFileName = $"{fileName}{DateTime.Now:MM-dd-HHmmss}.xlsx";
    //    var fullPath = Path.Combine(_rootPath, "export", sFileName);

    //    Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

    //    MiniExcel.SaveAs(fullPath, sheets);
    //    return (sFileName, fullPath);
    //}

    ///// <summary>
    ///// 下载导入模板
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="list"></param>
    ///// <param name="stream"></param>
    ///// <param name="fileName">下载文件名</param>
    ///// <returns></returns>
    //protected string DownloadImportTemplate<T>(List<T> list, Stream stream, string fileName)
    //{
    //    var sFileName = $"{fileName}模板.xlsx";
    //    var newFileName = Path.Combine(_rootPath, "ImportTemplate", sFileName);

    //    if (!Directory.Exists(newFileName)) Directory.CreateDirectory(Path.GetDirectoryName(newFileName));
    //    MiniExcel.SaveAs(newFileName, list);
    //    return sFileName;
    //}

    ///// <summary>
    ///// 下载指定文件模板
    ///// </summary>
    ///// <param name="fileName">下载文件名</param>
    ///// <returns></returns>
    //protected (string, string) DownloadImportTemplate(string fileName)
    //{
    //    var sFileName = $"{fileName}.xlsx";
    //    var fullPath = Path.Combine(_rootPath, "ImportTemplate", sFileName);

    //    return (sFileName, fullPath);
    //}
}