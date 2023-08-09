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
using System.Reflection;

namespace XiHan.WebCore.Common.Excels;

/// <summary>
/// Excel 操作帮助类
/// </summary>
public static class ExcelHelper
{
    /// <summary>
    /// 从 Excel 文件中读取数据到指定类型的对象列表中
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="filePath">Excel 文件路径</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>对象列表</returns>
    public static List<T> ReadFromExcel<T>(string filePath, string sheetName)
    {
        var data = MiniExcel.Query(filePath, true, sheetName).ToList();
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var result = new List<T>();

        foreach (var row in data.Skip(1))
        {
            var item = Activator.CreateInstance<T>();

            for (var i = 0; i < properties.Length; i++)
            {
                var property = properties[i];
                var propertyType = property.PropertyType;
                var cellValue = row[i];

                if (string.IsNullOrEmpty(cellValue)) continue;

                var value = propertyType switch
                {
                    // 常规类型
                    Type t when t == typeof(bool) => cellValue.ParseToBool(),
                    Type t when t == typeof(short) => cellValue.ParseToShort(),
                    Type t when t == typeof(long) => cellValue.ParseToLong(),
                    Type t when t == typeof(float) => cellValue.ParseToFloat(),
                    Type t when t == typeof(double) => cellValue.ParseToDouble(),
                    Type t when t == typeof(decimal) => cellValue.ParseToDecimal(),
                    Type t when t == typeof(int) => cellValue.ParseToInt(),
                    Type t when t == typeof(string) => cellValue.ParseToString(),
                    Type t when t == typeof(DateTime) => cellValue.ParseToDateTime(),
                    Type t when t == typeof(Guid) => cellValue.ParseToGuid(),
                    // 处理未知类型的情况
                    _ => Convert.ChangeType(cellValue, propertyType),
                };

                property.SetValue(item, value);
            }
            result.Add(item);
        }
        return result;
    }

    /// <summary>
    /// 将指定类型的对象列表写入 Excel 文件中
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="data">对象列表</param>
    /// <param name="filePath">Excel 文件路径</param>
    /// <param name="sheetName">工作表名称</param>
    public static void WriteToExcel<T>(IEnumerable<T> data, string filePath, string sheetName)
    {
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var header = properties.Select(p => p.GetCustomAttribute<ExcelColumnAttribute>()?.Name ?? p.Name).ToArray();
        var rows = new List<object[]>();

        foreach (var item in data)
        {
            var row = new object[properties.Length];
            for (var i = 0; i < properties.Length; i++)
            {
                var property = properties[i];
                var value = property.GetValue(item);
                row[i] = value ?? DBNull.Value;
            }
            rows.Add(row);
        }
        var result = new List<object[]> { header }.Concat(rows);

        // 写入数据到 Excel 文件
        MiniExcel.SaveAs(filePath, result, true, sheetName);
    }

    /// <summary>
    /// 将指定类型的对象列表导出为 Excel 文件，并返回文件路径
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="data">对象列表</param>
    /// <param name="fileName">Excel 文件名(不包含扩展名)</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel 文件路径</returns>
    public static string ExportToExcel<T>(IEnumerable<T> data, string fileName, string sheetName)
    {
        // 临时文件夹
        var filePath = Path.Combine(Path.GetTempPath(), $"{fileName}.xlsx");
        // 将数据写入 Excel 文件
        WriteToExcel(data, filePath, sheetName);
        return filePath;
    }

    /// <summary>
    /// 获取指定属性的 Excel 列名称
    /// </summary>
    /// <param name="property">属性</param>
    /// <returns>Excel 列名称</returns>
    public static string GetExcelColumnName(PropertyInfo property)
    {
        return property.GetCustomAttribute<ExcelColumnAttribute>()?.Name ?? property.Name;
    }
}