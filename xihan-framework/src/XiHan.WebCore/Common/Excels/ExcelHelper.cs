#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ExcelHelper
// Guid:4180647d-307e-4904-a3dc-38048e5e39b0
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-06-15 上午 03:44:24
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using MiniExcelLibs;

namespace XiHan.WebCore.Common.Excels;

/// <summary>
/// Excel 操作帮助类
/// </summary>
public static class ExcelHelper
{
    #region 读取

    /// <summary>
    /// 从 Excel 文件中读取数据到指定类型的对象序列
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="fullPath">Excel 文件全路径</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>对象列表</returns>
    public static IEnumerable<T> ReadFromExcel<T>(string fullPath, string sheetName) where T : class, new()
    {
        return MiniExcel.Query<T>(fullPath, sheetName);
    }

    /// <summary>
    /// 从 Excel 文件中读取多个 Sheet 数据到字典
    /// </summary>
    /// <param name="fullPath">Excel 文件全路径</param>
    /// <returns></returns>
    public static IDictionary<string, object> ReadFromExcel(string fullPath)
    {
        Dictionary<string, object> resultData = new();

        List<string> sheetNames = MiniExcel.GetSheetNames(fullPath);
        foreach (var sheetName in sheetNames)
        {
            IEnumerable<dynamic> data = MiniExcel.Query(fullPath, true, sheetName);
            resultData.Add(sheetName, data);
        }

        return resultData;
    }

    #endregion

    #region 写入

    /// <summary>
    /// 将指定类型的对象序列写入 Excel 文件
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="fullPath">Excel 文件全路径</param>
    /// <param name="dataSource">序列对象源</param>
    /// <param name="sheetName">工作表名称</param>
    public static void WriteToExcel<T>(string fullPath, IEnumerable<T> dataSource, string sheetName)
        where T : class, new()
    {
        // 写入数据到 Excel 文件
        MiniExcel.SaveAs(fullPath, dataSource, true, sheetName, overwriteFile: true);
    }

    /// <summary>
    /// 将多个类型的对象写入 Excel 文件
    /// </summary>
    /// <param name="fullPath">Excel 文件全路径</param>
    /// <param name="sheetsSource">表格数据源</param>
    public static void WriteToExcel(string fullPath, IDictionary<string, object> sheetsSource)
    {
        MiniExcel.SaveAs(fullPath, sheetsSource, overwriteFile: true);
    }

    #endregion

    #region 导入

    /// <summary>
    /// 将 Excel 文件导入为指定类型的对象序列，并返回该序列
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="fullPath">Excel 文件名(包含扩展名)</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>指定类型的对象序列</returns>
    public static IEnumerable<T> ImportToExcel<T>(string fullPath, string sheetName) where T : class, new()
    {
        // 读取 Excel 文件
        return ReadFromExcel<T>(fullPath, sheetName);
    }

    /// <summary>
    /// 将 Excel 文件导入为多个类型的对象字典，并返回该字典
    /// </summary>
    /// <param name="fullPath">Excel 文件名(包含扩展名)</param>
    /// <returns>多个类型的对象字典</returns>
    public static IDictionary<string, object> ImportToExcel(string fullPath)
    {
        // 读取 Excel 文件
        return ReadFromExcel(fullPath);
    }

    #endregion

    #region 导出

    /// <summary>
    /// 将指定类型的对象序列导出为 Excel 文件，并返回临时文件路径
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="fileName">Excel 文件名(不包含扩展名)</param>
    /// <param name="dataSource">对象源序列</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel 文件路径</returns>
    public static string ExportToExcel<T>(string fileName, IEnumerable<T> dataSource, string sheetName)
        where T : class, new()
    {
        fileName = $"{fileName}.xlsx";
        // 临时文件夹
        var tempPath = Path.Combine(Path.GetTempPath(), fileName);
        // 将数据写入 Excel 文件
        WriteToExcel(tempPath, dataSource, sheetName);
        return tempPath;
    }

    /// <summary>
    /// 将多个类型的对象导出为 Excel 文件，并返回临时文件路径
    /// </summary>
    /// <param name="fileName">Excel 文件名(不包含扩展名)</param>
    /// <param name="sheetsSource">表格数据源</param>
    public static string ExportToExcel(string fileName, IDictionary<string, object> sheetsSource)
    {
        fileName = $"{fileName}.xlsx";
        // 临时文件夹
        var tempPath = Path.Combine(Path.GetTempPath(), fileName);
        // 将数据写入 Excel 文件
        WriteToExcel(tempPath, sheetsSource);
        return tempPath;
    }

    #endregion
}