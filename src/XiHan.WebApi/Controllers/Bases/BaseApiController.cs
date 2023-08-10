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
using XiHan.Infrastructures.Apps;
using XiHan.Infrastructures.Apps.HttpContexts;
using XiHan.Utils.Exceptions;
using XiHan.Utils.Files;
using XiHan.WebCore.Common.Excels;
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
    /// <param name="fileName">带扩展的文件名</param>
    /// <param name="fullPath">完整文件路径</param>
    /// <param name="contentType">文件类型</param>
    /// <returns></returns>
    protected async Task ExportFile(string fileName, string fullPath, ContentTypeEnum contentType)
    {
        if (!FileHelper.IsExistFile(fullPath))
        {
            throw new CustomException(fileName + "文件不存在！");
        }
        await HttpContext.ExportFile(fileName, GetFileBytes(fullPath), contentType);
    }

    /// <summary>
    /// 导出 Excel
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="fileName">Excel 文件名(不包含扩展名)</param>
    /// <param name="dataSource">对象源序列</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns></returns>
    protected async Task ExportExcel<T>(string fileName, IEnumerable<T> dataSource, string sheetName)
    {
        // 临时文件路径
        var tempPath = ExcelHelper.ExportToExcel(fileName, dataSource, sheetName);
        // 导出文件路径
        var fullPath = Path.Combine(App.RootExportPath, FileHelper.GetFileNameWithExtension(tempPath));
        // 将临时文件从临时文件路径复制到导出文件路径
        FileHelper.CopyFile(tempPath, fullPath);
        // 删除临时文件
        FileHelper.DeleteFile(tempPath);
        await HttpContext.ExportFile(fileName, GetFileBytes(fullPath), ContentTypeEnum.ApplicationXlsx);
    }

    /// <summary>
    /// 导出 Excel(多个工作表)
    /// </summary>
    /// <param name="fileName">Excel 文件名(不包含扩展名)</param>
    /// <param name="sheetsSource">表格数据源</param>
    /// <returns></returns>
    protected async Task ExportExcelMultipleSheets(string fileName, IDictionary<string, object> sheetsSource)
    {
        // 临时文件路径
        var tempPath = ExcelHelper.ExportToExcel(fileName, sheetsSource);
        // 导出文件路径
        var fullPath = Path.Combine(App.RootExportPath, FileHelper.GetFileNameWithExtension(tempPath));
        // 将临时文件从临时文件路径复制到导出文件路径
        FileHelper.CopyFile(tempPath, fullPath);
        // 删除临时文件
        FileHelper.DeleteFile(tempPath);
        await HttpContext.ExportFile(fileName, GetFileBytes(fullPath), ContentTypeEnum.ApplicationXlsx);
    }

    /// <summary>
    /// 下载指定源导入模板(默认保存在模板目录)
    /// </summary>
    /// <param name="fileName">Excel 文件名(不包含扩展名)</param>
    /// <returns></returns>
    protected async Task DownloadExcelImportTemplate(string fileName)
    {
        fileName = $"{fileName}-模板.xlsx";
        // 导入模板文件路径
        var fullPath = Path.Combine(App.RootImportTemplatePath, fileName);
        if (FileHelper.IsExistDirectory(fullPath))
            await HttpContext.ExportFile(fileName, GetFileBytes(fullPath), ContentTypeEnum.ApplicationXlsx);
        throw new CustomException(fileName + "文件不存在！");
    }

    /// <summary>
    /// 下载自定义源导入模板(默认保存在模板目录)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fileName">Excel 文件名(不包含扩展名)</param>
    /// <param name="dataSource">数据源</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns></returns>
    protected async Task DownloadExcelImportTemplate<T>(string fileName, IEnumerable<T> dataSource, string sheetName)
    {
        fileName = $"{fileName}-模板.xlsx";
        // 导入模板文件路径
        var fullPath = Path.Combine(App.RootImportTemplatePath, fileName);
        // 存在模板就删除重新写入
        if (FileHelper.IsExistDirectory(fullPath))
        {
            // 删除原导入模板文件
            FileHelper.DeleteFile(fullPath);
            dataSource.ToList().Clear();
            // 临时文件路径
            var tempPath = ExcelHelper.ExportToExcel(fileName, dataSource, sheetName);
            // 将临时文件从临时文件路径复制到导入模板文件路径
            FileHelper.CopyFile(tempPath, fullPath);
            // 删除临时文件
            FileHelper.DeleteFile(tempPath);
            await HttpContext.ExportFile(fileName, GetFileBytes(fullPath), ContentTypeEnum.ApplicationXlsx);
        }
    }

    /// <summary>
    /// 获取文件字节数组
    /// </summary>
    /// <param name="path">完整文件路径</param>
    /// <returns></returns>
    private static byte[] GetFileBytes(string path)
    {
        // 创建文件流
        using var fileStream = new FileStream(path, FileMode.Open);
        using var memoryStream = new MemoryStream();
        fileStream.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }
}