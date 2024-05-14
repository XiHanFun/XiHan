﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:DirectoryHelper
// Guid:cf1009e0-c9d4-4bee-b84a-a731e7b161f0
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/21 16:36:17
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Core.IO;

/// <summary>
/// 目录帮助类
/// </summary>
public static class DirectoryHelper
{
    #region 目录操作

    /// <summary>
    /// 创建一个新目录，如果目录已存在则不执行任何操作
    /// </summary>
    /// <param name="directoryPath">要创建的目录的路径</param>
    public static void CreateIfNotExists(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
    }

    /// <summary>
    /// 删除一个目录，如果目录存在
    /// </summary>
    /// <param name="directoryPath">要删除的目录的路径</param>
    public static void DeleteIfExists(string directoryPath)
    {
        if (Directory.Exists(directoryPath))
        {
            // true 表示删除目录及其所有子目录和文件
            Directory.Delete(directoryPath, true);
        }
    }

    /// <summary>
    /// 清空一个目录，不删除目录本身，只删除其中的所有文件和子目录
    /// </summary>
    /// <param name="directoryPath">要清空的目录的路径</param>
    public static void Clear(string directoryPath)
    {
        foreach (string file in Directory.GetFiles(directoryPath))
        {
            File.Delete(file);
        }

        foreach (string dir in Directory.GetDirectories(directoryPath))
        {
            DeleteIfExists(dir);
        }
    }

    /// <summary>
    /// 移动目录到另一个位置
    /// </summary>
    /// <param name="sourcePath">当前目录的路径</param>
    /// <param name="destinationPath">目标目录的路径</param>
    public static void Move(string sourcePath, string destinationPath)
    {
        Directory.Move(sourcePath, destinationPath);
    }

    /// <summary>
    /// 复制一个目录到另一个位置
    /// </summary>
    /// <param name="sourcePath">当前目录的路径</param>
    /// <param name="destinationPath">目标目录的路径</param>
    /// <param name="overwrite">如果目标位置已经存在同名目录，是否覆盖</param>
    public static void Copy(string sourcePath, string destinationPath, bool overwrite = false)
    {
        // 检查目标目录是否存在，如果存在且overwrite为false，则不执行复制
        if (Directory.Exists(destinationPath) && !overwrite)
        {
            throw new IOException("目标目录已存在且overwrite参数为false ");
        }

        // 复制目录
        DirectoryInfo sourceDir = new(sourcePath);
        if (!Directory.Exists(destinationPath))
        {
            Directory.CreateDirectory(destinationPath);
        }

        FileInfo[] files = sourceDir.GetFiles();
        foreach (FileInfo file in files)
        {
            file.CopyTo(Path.Combine(destinationPath, file.Name), overwrite);
        }

        DirectoryInfo[] dirs = sourceDir.GetDirectories();
        foreach (DirectoryInfo dir in dirs)
        {
            string newDirPath = Path.Combine(destinationPath, dir.Name);
            Copy(dir.FullName, newDirPath, overwrite);
        }
    }

    #endregion

    #region 目录信息

    /// <summary>
    /// 获取当前目录中所有文件的路径
    /// </summary>
    /// <param name="directoryPath">目录的路径 </param>
    /// <returns>包含目录中文件路径的数组 </returns>
    public static string[] GetFiles(string directoryPath)
    {
        return Directory.GetFiles(directoryPath);
    }

    /// <summary>
    /// 获取目录中所有文件的路径
    /// </summary>
    /// <param name="directoryPath">目录的路径 </param>
    /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符 范例："Log*.xml"表示搜索所有以Log开头的Xml文件</param>
    /// <param name="isSearchChild">是否搜索子目录</param>
    /// <returns>包含目录中所有文件路径的数组</returns>
    public static string[] GetFiles(string directoryPath, string searchPattern, bool isSearchChild)
    {
        return Directory.GetFiles(directoryPath, searchPattern,
            isSearchChild ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
    }

    /// <summary>
    /// 获取当前目录中所有子目录的路径
    /// </summary>
    /// <param name="directoryPath">目录的路径 </param>
    /// <returns>包含目录中所有子目录路径的数组 </returns>
    public static string[] GetDirectories(string directoryPath)
    {
        return Directory.GetDirectories(directoryPath);
    }

    /// <summary>
    /// 获取指定目录及子目录中所有子目录列表
    /// </summary>
    /// <param name="directoryPath">指定目录的绝对路径</param>
    /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符 范例："Log*.xml"表示搜索所有以Log开头的Xml目录</param>
    /// <param name="isSearchChild">是否搜索子目录</param>
    /// <returns>包含目录中所有文件路径的数组</returns>
    public static string[] GetDirectories(string directoryPath, string searchPattern, bool isSearchChild)
    {
        return Directory.GetDirectories(directoryPath, searchPattern,
            isSearchChild ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
    }

    /// <summary>
    /// 获取指定目录大小
    /// </summary>
    /// <param name="dirPath"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static long GetDirectorySize(string dirPath)
    {
        // 定义一个DirectoryInfo对象
        DirectoryInfo di = new(dirPath);
        // 通过GetFiles方法,获取di目录中的所有文件的大小
        var len = di.GetFiles().Sum(fi => fi.Length);
        // 获取di中所有的文件夹,并存到一个新的对象数组中,以进行递归
        var dis = di.GetDirectories();
        if (dis.Length <= 0) return len;

        len += dis.Sum(t => GetDirectorySize(t.FullName));
        return len;
    }

    #endregion

    #region 目录检查

    /// <summary>
    /// 检查给定路径是否为目录
    /// </summary>
    /// <param name="path">要检查的路径</param>
    /// <returns>true如果路径是一个目录，否则false </returns>
    public static bool IsDirectory(string path)
    {
        return Directory.Exists(path);
    }

    /// <summary>
    /// 检测指定目录中是否存在指定的文件(搜索子目录)
    /// </summary>
    /// <param name="directoryPath">指定目录的绝对路径</param>
    /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符 范例："Log*.xml"表示搜索所有以Log开头的Xml文件 </param>
    /// <param name="isSearchChild">是否搜索子目录</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static bool IsContainsFiles(string directoryPath, string searchPattern, bool isSearchChild)
    {
        // 获取指定的文件列表
        var fileNames = GetFiles(directoryPath, searchPattern, isSearchChild);
        // 判断指定文件是否存在
        return fileNames.Length != 0;
    }

    /// <summary>
    /// 检测指定目录是否为空
    /// </summary>
    /// <param name="directoryPath">指定目录的绝对路径</param>
    /// <returns></returns>
    public static bool IsEmptyDirectory(string directoryPath)
    {
        // 判断是否存在文件
        var fileNames = GetFiles(directoryPath);
        if (fileNames.Length != 0)
            return false;
        // 判断是否存在文件夹
        var directoryNames = GetDirectories(directoryPath);
        return directoryNames.Length == 0;
    }

    #endregion
}