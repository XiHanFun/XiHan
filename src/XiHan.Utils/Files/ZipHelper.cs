#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ZipHelper
// Guid:85265278-4d78-4775-a590-07b938651804
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-21 上午 09:30:46
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.IO.Compression;

namespace XiHan.Utils.Files;

/// <summary>
/// 压缩解压帮助类
/// </summary>
public class ZipHelper
{
    /// <summary>
    /// 提取
    /// </summary>
    /// <param name="archivePath">存档路径</param>
    /// <param name="extractPath">提取路径</param>
    /// <exception cref="FileNotFoundException"></exception>
    public static void Extract(string archivePath, string extractPath)
    {
        if (!File.Exists(archivePath))
        {
            throw new FileNotFoundException("Archive not found.", archivePath);
        }

        if (!Directory.Exists(extractPath))
        {
            Directory.CreateDirectory(extractPath);
        }

        ZipFile.ExtractToDirectory(archivePath, extractPath);
    }

    /// <summary>
    /// 压缩
    /// </summary>
    /// <param name="sourceDirectory">源目录</param>
    /// <param name="archivePath">存档路径</param>
    /// <exception cref="DirectoryNotFoundException"></exception>
    public static void Compress(string sourceDirectory, string archivePath)
    {
        if (!Directory.Exists(sourceDirectory))
        {
            throw new DirectoryNotFoundException("Source directory not found.");
        }

        ZipFile.CreateFromDirectory(sourceDirectory, archivePath, CompressionLevel.Optimal, false);
    }
}