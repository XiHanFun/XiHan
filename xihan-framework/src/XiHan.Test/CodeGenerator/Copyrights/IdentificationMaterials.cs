﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IdentificationMaterials
// Guid:4092a030-b98c-4d7f-9aa0-9a5ccc678a16
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-09-13 下午 02:41:00
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Text;
using XiHan.Utils.Extensions;
using XiHan.Utils.Files;

namespace XiHan.Test.CodeGenerator.Copyrights;

/// <summary>
/// 生成软件著作权鉴别材料
/// </summary>
public class IdentificationMaterials
{
    /// <summary>
    /// 主函数
    /// </summary>
    public static void Main()
    {
        $@"开始读取所有代码文件……".WriteLineHandle();
        var files = FindAllFiles(@"D:\DataMine\Repository\XiHan.Framework\xihan-framework\src", "cs");
        $@"找到代码文件共{files.Count()}个。".WriteLineHandle();

        var ignorePaths = new List<string>()
        {
            "obj","bin","Test"
        };
        files = IgnorePathFiles(files, ignorePaths).OrderBy(s => s).ToList();
        $@"移除忽略后文件共{files.Count()}个。".WriteLineHandle();

        var sb = new StringBuilder();

        files.Skip(1).ToList().ForEach(file =>
        {
            sb.AppendLine(file);
            sb.AppendLine("```csharp");
            var lines = File.ReadAllLines(file)
            .Skip(13)
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .ToList();
            lines.ForEach(line =>
            {
                sb.AppendLine(line);
            });
            sb.AppendLine("```");
            sb.AppendLine();
        });

        string code = sb.ToString();
        FileHelper.WriteText(@"D:\曦寒软著代码.md", code, Encoding.UTF8);
        $@"生成成功！路径【D:\曦寒软著代码.md】。".WriteLineSuccess();
    }

    /// <summary>
    /// 找到代码文件夹下所有的指定后缀的文件路径
    /// </summary>
    /// <param name="path"></param>
    /// <param name="suffix"></param>
    /// <returns></returns>
    private static List<string> FindAllFiles(string path, string suffix = "cs")
    {
        return FileHelper.GetFiles(path, $"*.{suffix}", true).ToList();
    }

    /// <summary>
    /// 移除忽略文件
    /// </summary>
    /// <param name="path"></param>
    /// <param name="suffix"></param>
    /// <returns></returns>
    private static List<string> IgnorePathFiles(List<string> filePaths, List<string> ignorePaths)
    {
        return filePaths.Where(path => ignorePaths.All(ignorePath => !path.Contains(ignorePath))).ToList();
    }
}