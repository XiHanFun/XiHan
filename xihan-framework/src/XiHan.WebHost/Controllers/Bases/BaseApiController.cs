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

namespace XiHan.WebHost.Controllers.Bases;

/// <summary>
/// BaseApiController
/// </summary>
[ApiController]
[Route("Api/[controller]")]
[Produces("application/json")]
[ApiGroup(ApiGroupNameEnum.All)]
public class BaseApiController : ControllerBase
{
    #region 上传文件

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="file"></param>
    /// <returns>完整文件路径</returns>
    /// <exception cref="CustomException"></exception>
    protected async Task<string> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new CustomException("No file uploaded.");
        }

        // 唯一文件名
        string uniqueFileName = GetUniqueFileName(file.FileName);
        // 上传文件路径
        string fullPath = Path.Combine(App.RootUploadPath, FileHelper.GetDateDirName(), FileHelper.GetFileNameWithExtension(uniqueFileName));
        // 创建目录
        FileHelper.CreateDirectory(fullPath);
        using FileStream stream = new(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);
        return fullPath;
    }

    #endregion

    #region 下载文件

    /// <summary>
    /// 下载文件
    /// </summary>
    /// <param name="fileName">带扩展的文件名</param>
    /// <param name="fullPath">完整文件路径</param>
    /// <param name="contentType">文件类型</param>
    /// <returns></returns>
    protected async Task DownloadFile(string fileName, string fullPath, ContentTypeEnum contentType)
    {
        if (!FileHelper.IsExistFile(fullPath))
        {
            throw new CustomException(fileName + "文件不存在！");
        }
        await HttpContext.DownloadFile(fileName, fullPath, contentType);
    }

    #endregion

    #region 下载导入模板

    /// <summary>
    /// 下载指定源导入模板(默认保存在模板目录)
    /// </summary>
    /// <param name="fileName">Excel 文件名(不包含扩展名)</param>
    /// <returns></returns>
    protected async Task DownloadImportTemplate(string fileName)
    {
        fileName = $"{fileName}_模板.xlsx";
        // 模板文件路径
        string fullPath = Path.Combine(App.RootTemplatePath, fileName);
        if (FileHelper.IsExistDirectory(fullPath))
        {
            await HttpContext.DownloadFile(fileName, fullPath, ContentTypeEnum.ApplicationXlsx);
        }

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
    protected async Task DownloadImportTemplate<T>(string fileName, IEnumerable<T> dataSource, string sheetName) where T : class, new()
    {
        fileName = $"{fileName}_模板.xlsx";
        // 模板文件路径
        string fullPath = Path.Combine(App.RootTemplatePath, fileName);
        // 存在模板就删除重新写入
        if (FileHelper.IsExistDirectory(fullPath))
        {
            // 删除原模板文件
            FileHelper.DeleteFile(fullPath);
        }
        List<T> templateData = dataSource.ToList();
        templateData.Clear();
        // 临时文件路径
        string tempPath = ExcelHelper.ExportToExcel(fileName, templateData, sheetName);
        // 将临时文件从临时文件路径复制到导入模板文件路径
        FileHelper.CopyFile(tempPath, fullPath);
        // 删除临时文件
        FileHelper.DeleteFile(tempPath);
        await HttpContext.DownloadFile(fileName, fullPath, ContentTypeEnum.ApplicationXlsx);
    }

    #endregion

    #region 导入

    /// <summary>
    /// 导入 Excel
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="file">文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>对象源序列</returns>
    protected async Task<IEnumerable<T>> ImportExcel<T>(IFormFile file, string sheetName) where T : class, new()
    {
        if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
        {
            throw new CustomException("Invalid file format. Only .xlsx files are supported.");
        }
        // 唯一文件名
        string uniqueFileName = GetUniqueFileName(file.FileName);
        // 导入文件路径
        string fullPath = Path.Combine(App.RootImportPath, FileHelper.GetDateDirName(), FileHelper.GetFileNameWithExtension(uniqueFileName));
        // 创建目录
        FileHelper.CreateDirectory(fullPath);
        using FileStream stream = new(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);
        return ExcelHelper.ImportToExcel<T>(fullPath, sheetName);
    }

    /// <summary>
    /// 导入 Excel(多个工作表)
    /// </summary>
    /// <param name="file">文件流</param>
    /// <returns>表格数据源</returns>
    protected async Task<IDictionary<string, object>> ImportExcel(IFormFile file)
    {
        if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
        {
            throw new CustomException("Invalid file format. Only .xlsx files are supported.");
        }
        // 唯一文件名
        string uniqueFileName = GetUniqueFileName(file.FileName);
        // 导入文件路径
        string fullPath = Path.Combine(App.RootImportPath, FileHelper.GetDateDirName(), FileHelper.GetFileNameWithExtension(uniqueFileName));
        // 创建目录
        FileHelper.CreateDirectory(fullPath);
        using FileStream stream = new(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);
        return ExcelHelper.ImportToExcel(fullPath);
    }

    #endregion

    #region 导出

    /// <summary>
    /// 导出 Excel
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="fileName">Excel 文件名(不包含扩展名)</param>
    /// <param name="dataSource">对象源序列</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns></returns>
    protected async Task ExportExcel<T>(string fileName, IEnumerable<T> dataSource, string sheetName) where T : class, new()
    {
        // 临时文件路径
        string tempPath = ExcelHelper.ExportToExcel(fileName, dataSource, sheetName);
        // 导出文件路径
        string fullPath = Path.Combine(App.RootExportPath, FileHelper.GetFileNameWithExtension(tempPath));
        // 将临时文件从临时文件路径复制到导出文件路径
        FileHelper.CopyFile(tempPath, fullPath);
        // 删除临时文件
        FileHelper.DeleteFile(tempPath);
        await HttpContext.DownloadFile(fileName, fullPath, ContentTypeEnum.ApplicationXlsx);
    }

    /// <summary>
    /// 导出 Excel(多个工作表)
    /// </summary>
    /// <param name="fileName">Excel 文件名(不包含扩展名)</param>
    /// <param name="sheetsSource">表格数据源</param>
    /// <returns></returns>
    protected async Task ExportExcel(string fileName, IDictionary<string, object> sheetsSource)
    {
        // 临时文件路径
        string tempPath = ExcelHelper.ExportToExcel(fileName, sheetsSource);
        // 导出文件路径
        string fullPath = Path.Combine(App.RootExportPath, FileHelper.GetFileNameWithExtension(tempPath));
        // 将临时文件从临时文件路径复制到导出文件路径
        FileHelper.CopyFile(tempPath, fullPath);
        // 删除临时文件
        FileHelper.DeleteFile(tempPath);
        await HttpContext.DownloadFile(fileName, fullPath, ContentTypeEnum.ApplicationXlsx);
    }

    #endregion

    /// <summary>
    /// 生成唯一的文件名
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    private string GetUniqueFileName(string fileName)
    {
        string fileNameWithoutExtension = FileHelper.GetFileNameWithoutExtension(fileName);
        string fileExtension = FileHelper.GetFileExtension(fileName);
        string uniqueFileName = $"{fileNameWithoutExtension}_{FileHelper.GetRandomFileName()}{fileExtension}";
        return uniqueFileName;
    }
}