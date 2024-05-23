﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:FileHelper
// Guid:1de11462-1af5-4f59-bed1-66e085d154af
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/21 16:36:29
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Security.Cryptography;
using System.Text;
using XiHan.Utils.Text;

namespace XiHan.Utils.IO;

/// <summary>
/// 文件帮助类
/// </summary>
public static class FileHelper
{
    #region 文件操作

    /// <summary>
    /// 读取文件内容到字符串
    /// </summary>
    /// <param name="filePath">要读取的文件路径</param>
    /// <returns>文件内容为字符串</returns>
    public static string ReadAllText(string filePath)
    {
        return File.ReadAllText(filePath);
    }

    /// <summary>
    /// 打开一个文本文件，读取文件的所有行，然后关闭文件
    /// </summary>
    /// <param name="filePath">要打开以进行读取的文件路径</param>
    /// <returns>包含文件所有行的字符串</returns>
    public static async Task<string> ReadAllTextAsync(string filePath)
    {
        using var reader = File.OpenText(filePath);
        return await reader.ReadToEndAsync();
    }

    /// <summary>
    /// 打开一个文本文件，读取文件的所有字节，然后关闭文件
    /// </summary>
    /// <param name="filePath">要打开以进行读取的文件路径</param>
    /// <returns>包含文件所有字节的字节数组</returns>
    public static async Task<byte[]> ReadAllBytesAsync(string filePath)
    {
        using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        var result = new byte[stream.Length];
        await stream.ReadAsync(result.AsMemory(0, (int)stream.Length));
        return result;
    }

    /// <summary>
    /// 打开一个文本文件，读取文件的所有行，然后关闭文件
    /// </summary>
    /// <param name="path">要打开以进行读取的文件</param>
    /// <param name="encoding">文件的编码默认为 UTF8</param>
    /// <param name="fileMode">指定操作系统应如何打开文件默认为 Open</param>
    /// <param name="fileAccess">定义对文件的读取、写入或读写访问的常量默认为 Read</param>
    /// <param name="fileShare">包含控制其他 FileStream 对象可以对同一文件拥有的访问类型的常量默认为 Read</param>
    /// <param name="bufferSize">StreamReader 缓冲区的长度默认为 4096</param>
    /// <param name="fileOptions">指示 FileStream 选项默认为 Asynchronous（文件将用于异步读取）和 SequentialScan（文件将从开始到末尾顺序访问）</param>
    /// <returns>包含文件所有行的字符串数组</returns>
    public static async Task<string[]> ReadAllLinesAsync(string path,
        Encoding? encoding = null,
        FileMode fileMode = FileMode.Open,
        FileAccess fileAccess = FileAccess.Read,
        FileShare fileShare = FileShare.Read,
        int bufferSize = 4096,
        FileOptions fileOptions = FileOptions.Asynchronous | FileOptions.SequentialScan)
    {
        encoding ??= Encoding.UTF8;
        var lines = new List<string>();
        using (var stream = new FileStream(path, fileMode, fileAccess, fileShare, bufferSize, fileOptions))
        {
            using var reader = new StreamReader(stream, encoding);
            StringBuilder sb = new();
            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                lines.Add(line);
            }
        }
        return [.. lines];
    }

    /// <summary>
    /// 打开一个文本文件，读取不包含 BOM 的内容
    /// </summary>
    /// <param name="path">要打开以进行读取的文件</param>
    /// <returns>包含文件所有行的字符串</returns>
    public static async Task<string> ReadFileWithoutBomAsync(string path)
    {
        var content = await ReadAllBytesAsync(path);
        return StringHelper.ConvertFromBytesWithoutBom(content)!;
    }

    /// <summary>
    /// 将文本内容写入到文件
    /// </summary>
    /// <param name="filePath">要写入的文件路径</param>
    /// <param name="content">要写入的文本内容</param>
    public static void WriteAllText(string filePath, string content)
    {
        File.WriteAllText(filePath, content);
    }

    /// <summary>
    /// 将文本内容追加到文件
    /// </summary>
    /// <param name="filePath">要追加的文件路径</param>
    /// <param name="content">要追加的文本内容</param>
    public static void AppendAllText(string filePath, string content)
    {
        File.AppendAllText(filePath, content);
    }

    /// <summary>
    /// 创建文件，如果文件不存在
    /// </summary>
    /// <param name="filePath">文件路径</param>
    public static void CreateIfNotExists(string filePath)
    {
        if (!File.Exists(filePath))
        {
            File.Create(filePath);
        }
    }

    /// <summary>
    /// 删除一个文件，如果文件存在
    /// </summary>
    /// <param name="filePath">要删除的文件路径</param>
    public static void DeleteIfExists(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    /// <summary>
    /// 移动文件到另一个位置
    /// </summary>
    /// <param name="sourcePath">当前文件的路径</param>
    /// <param name="destinationPath">目标文件的路径</param>
    public static void Move(string sourcePath, string destinationPath)
    {
        File.Move(sourcePath, destinationPath);
    }

    /// <summary>
    /// 复制文件到另一个位置
    /// </summary>
    /// <param name="sourcePath">当前文件的路径</param>
    /// <param name="destinationPath">目标文件的路径</param>
    /// <param name="overwrite">如果目标位置已经存在同名文件，是否覆盖</param>
    public static void Copy(string sourcePath, string destinationPath, bool overwrite = false)
    {
        File.Copy(sourcePath, destinationPath, overwrite);
    }

    /// <summary>
    /// 清空文件内容
    /// </summary>
    /// <param name="filePath">文件的绝对路径</param>
    public static void CleanFile(string filePath)
    {
        if (!File.Exists(filePath))
            return;
        // 删除文件
        File.Delete(filePath);
        // 重新创建该文件
        File.Create(filePath);
    }

    #endregion

    #region 文件信息

    /// <summary>
    /// 获取文件的哈希值
    /// </summary>
    /// <param name="filePath">要计算哈希值的文件路径</param>
    /// <param name="hashAlgorithm">使用的哈希算法</param>
    /// <returns>文件的哈希值</returns>
    public static string GetFileHash(string filePath, HashAlgorithm hashAlgorithm)
    {
        using var stream = File.OpenRead(filePath);
        byte[] hash = hashAlgorithm.ComputeHash(stream);
        StringBuilder sb = new();
        foreach (byte b in hash)
        {
            sb.Append(b.ToString("X2"));
        }
        return sb.ToString();
    }

    /// <summary>
    /// 获取指定文件大小
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static long GetFileSize(string filePath)
    {
        return new FileInfo(filePath).Length;
    }

    /// <summary>
    /// 从文件的绝对路径中获取文件名(包含扩展方法名)
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static string GetFileName(string filePath)
    {
        return Path.GetFileName(filePath);
    }

    /// <summary>
    /// 获取随机文件名
    /// </summary>
    /// <returns></returns>
    public static string GetRandomFileName()
    {
        return Path.GetRandomFileName();
    }

    /// <summary>
    /// 根据时间得到文件名
    /// yyyyMMddHHmmssfff
    /// </summary>
    /// <returns></returns>
    public static string GetDateFileName()
    {
        return DateTime.Now.ToString("yyyyMMddHHmmssfff");
    }

    /// <summary>
    /// 从文件的绝对路径中获取扩展方法名
    /// 文件扩展方法名是包含点（.）的
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static string GetFileExtension(string filePath)
    {
        return Path.GetExtension(filePath);
    }

    /// <summary>
    /// 从文件的绝对路径中获取文件名(不包含扩展方法名)
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static string GetFileNameWithoutExtension(string filePath)
    {
        return Path.GetFileNameWithoutExtension(filePath);
    }

    /// <summary>
    /// 生成唯一的文件名，上传文件使用
    /// </summary>
    /// <param name="fileName">包含扩展方法名的源文件名</param>
    /// <returns></returns>
    public static string GetUniqueFileName(string fileName)
    {
        var fileNameWithoutExtension = GetFileNameWithoutExtension(fileName);
        var fileExtension = GetFileExtension(fileName);
        var uniqueFileName = $"{GetDateFileName()}_{GetRandomFileName()}_{fileNameWithoutExtension}";
        return uniqueFileName + fileExtension;
    }

    /// <summary>
    /// 获取文本文件的行数
    /// </summary>
    /// <param name="filePath">文件的绝对路径</param>
    /// <returns></returns>
    public static int GetTextFileLineCount(string filePath)
    {
        // 将文本文件的各行读到一个字符串数组中
        var rows = File.ReadAllLines(filePath);
        // 返回行数
        return rows.Length;
    }

    #endregion

    #region 文件检查

    /// <summary>
    /// 检查文件是否存在
    /// </summary>
    /// <param name="filePath">要检查的文件路径</param>
    /// <returns>如果文件存在返回true，否则返回false</returns>
    public static bool IsFile(string filePath)
    {
        return File.Exists(filePath);
    }

    /// <summary>
    /// 检查文件是否被锁定
    /// </summary>
    /// <param name="filePath">要检查的文件路径</param>
    /// <returns>true如果文件没有被锁定，可以进行读写操作，否则false</returns>
    public static bool IsFileUnlocked(string filePath)
    {
        try
        {
            using var fileStream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            // 如果没有异常，文件没有被锁定
            return true;
        }
        catch (IOException)
        {
            // 如果发生异常，文件可能被锁定
            return false;
        }
    }

    #endregion
}