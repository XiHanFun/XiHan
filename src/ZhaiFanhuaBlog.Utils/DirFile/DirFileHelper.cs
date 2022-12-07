#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:DirFileHelper
// Guid:4e1014f7-200b-42f3-a1bf-cde1c500054a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-03 下午 08:48:25
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Text;

namespace ZhaiFanhuaBlog.Utils.DirFile;

/// <summary>
/// 文件或目录信息
/// </summary>
public static class DirFileHelper
{
    /// <summary>
    /// 清空指定目录下所有文件及子目录
    /// </summary>
    /// <param name="directoryPath">指定目录的绝对路径</param>
    public static void ClearDirectory(string directoryPath)
    {
        if (Directory.Exists(directoryPath))
        {
            // 删除目录中所有的文件
            string[] fileNames = GetFiles(directoryPath);
            for (int i = 0; i < fileNames.Length; i++)
            {
                DeleteFile(fileNames[i]);
            }
            // 删除目录中所有的子目录
            string[] directoryNames = GetDirectories(directoryPath);
            for (int i = 0; i < directoryNames.Length; i++)
            {
                DeleteDirectory(directoryNames[i]);
            }
        }
    }

    /// <summary>
    /// 清空文件内容
    /// </summary>
    /// <param name="filePath">文件的绝对路径</param>
    public static void ClearFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            // 删除文件
            File.Delete(filePath);
            // 重新创建该文件
            CreateFile(filePath, string.Empty, Encoding.UTF8);
        }
    }

    /// <summary>
    /// 将源文件的内容复制到目标文件中
    /// </summary>
    /// <param name="sourceFilePath">源文件的绝对路径</param>
    /// <param name="destFilePath">目标文件的绝对路径</param>
    public static void CopyFile(string sourceFilePath, string destFilePath)
    {
        File.Copy(sourceFilePath, destFilePath, true);
    }

    /// <summary>
    /// 复制文件夹(递归)
    /// </summary>
    /// <param name="varFromDirectory">源文件夹路径</param>
    /// <param name="varToDirectory">目标文件夹路径</param>
    public static void CopyFolder(string varFromDirectory, string varToDirectory)
    {
        Directory.CreateDirectory(varToDirectory);
        if (Directory.Exists(varFromDirectory))
        {
            string[] directories = Directory.GetDirectories(varFromDirectory);
            if (directories.Length > 0)
            {
                foreach (string d in directories)
                {
                    CopyFolder(d, varToDirectory + d[d.LastIndexOf(@"\")..]);
                }
            }
            string[] files = Directory.GetFiles(varFromDirectory);
            if (files.Length > 0)
            {
                foreach (string s in files)
                {
                    File.Copy(s, varToDirectory + s[s.LastIndexOf(@"\")..], true);
                }
            }
        }
    }

    /// <summary>
    /// 创建目录
    /// </summary>
    /// <param name="dir">要创建的目录路径包括目录名</param>
    public static void CreateDirectory(string dir)
    {
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
    }

    /// <summary>
    /// 创建文件
    /// </summary>
    /// <param name="dir">带后缀的文件名</param>
    /// <param name="pagestr">文件内容</param>
    /// <param name="encoding">文件编码</param>
    public static void CreateFile(string dir, string pagestr, Encoding encoding)
    {
        try
        {
            dir = dir.Replace(@"/", @"\");
            if (dir.IndexOf(@"\") > -1)
            {
                CreateDirectory(dir.Substring(0, dir.LastIndexOf(@"\")));
            }
            StreamWriter sw = new(dir, false, encoding);
            sw.Write(pagestr);
            sw.Close();
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// 删除目录及其所有子目录
    /// </summary>
    /// <param name="dir">要删除的目录路径和名称</param>
    public static void DeleteDirectory(string dir)
    {
        if (Directory.Exists(dir))
        {
            Directory.Delete(dir, true);
        }
    }

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="file">要删除的文件路径和名称</param>
    public static void DeleteFile(string file)
    {
        if (File.Exists(file))
        {
            File.Delete(file);
        }
    }

    /// <summary>
    /// 仅删除指定文件夹和子文件夹的文件
    /// </summary>
    /// <param name="varFromDirectory">指定文件夹路径</param>
    /// <param name="varToDirectory">对应其他文件夹路径</param>
    public static void DeleteFolderFiles(string varFromDirectory, string varToDirectory)
    {
        Directory.CreateDirectory(varToDirectory);
        if (!Directory.Exists(varFromDirectory)) return;
        string[] directories = Directory.GetDirectories(varFromDirectory);
        if (directories.Length > 0)
        {
            foreach (string d in directories)
            {
                DeleteFolderFiles(d, string.Concat(varToDirectory, d.AsSpan(d.LastIndexOf(@"\"))));
            }
        }
        string[] files = Directory.GetFiles(varFromDirectory);
        if (files.Length > 0)
        {
            foreach (string s in files)
            {
                File.Delete(string.Concat(varToDirectory, s.AsSpan(s.LastIndexOf(@"\"))));
            }
        }
    }

    /// <summary>
    /// 检查文件,如果文件不存在则创建
    /// </summary>
    /// <param name="FilePath">路径,包括文件名</param>
    public static void ExistsFile(string FilePath)
    {
        if (!File.Exists(FilePath))
        {
            FileStream fs = File.Create(FilePath);
            fs.Close();
        }
    }

    /// <summary>
    /// 根据时间得到目录名yyyyMMdd
    /// </summary>
    /// <returns></returns>
    public static string GetDateDirName()
    {
        return DateTime.Now.ToString("yyyyMMdd");
    }

    /// <summary>
    /// 根据时间得到文件名HHmmssff
    /// </summary>
    /// <returns></returns>
    public static string GetDateFileName()
    {
        return DateTime.Now.ToString("HHmmssff");
    }

    /// <summary>
    /// 获取指定目录中所有子目录列表,若要搜索嵌套的子目录列表,请使用重载方法
    /// </summary>
    /// <param name="directoryPath">指定目录的绝对路径</param>
    public static string[] GetDirectories(string directoryPath)
    {
        try
        {
            return Directory.GetDirectories(directoryPath);
        }
        catch (IOException ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// 获取指定目录及子目录中所有子目录列表
    /// </summary>
    /// <param name="directoryPath">指定目录的绝对路径</param>
    /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
    /// <param name="isSearchChild">是否搜索子目录</param>
    public static string[] GetDirectories(string directoryPath, string searchPattern, bool isSearchChild)
    {
        try
        {
            if (isSearchChild)
            {
                return Directory.GetDirectories(directoryPath, searchPattern, SearchOption.AllDirectories);
            }
            else
            {
                return Directory.GetDirectories(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
            }
        }
        catch (IOException ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// 获取指定目录大小
    /// </summary>
    /// <param name="dirPath"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static long GetDirectorySize(string dirPath)
    {
        try
        {
            long len = 0;
            // 定义一个DirectoryInfo对象
            DirectoryInfo di = new DirectoryInfo(dirPath);
            // 通过GetFiles方法,获取di目录中的所有文件的大小
            foreach (FileInfo fi in di.GetFiles())
            {
                len += fi.Length;
            }
            // 获取di中所有的文件夹,并存到一个新的对象数组中,以进行递归
            DirectoryInfo[] dis = di.GetDirectories();
            if (dis.Length > 0)
            {
                for (int i = 0; i < dis.Length; i++)
                {
                    len += GetDirectorySize(dis[i].FullName);
                }
            }
            return len;
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// 从文件的绝对路径中获取扩展名
    /// </summary>
    /// <param name="filePath">文件的绝对路径</param>
    public static string GetFileExtension(string filePath)
    {
        // 获取文件的名称
        FileInfo fi = new(filePath);
        return fi.Extension;
    }

    /// <summary>
    /// 从文件的绝对路径中获取文件名(不包含扩展名)
    /// </summary>
    /// <param name="filePath">文件的绝对路径</param>
    public static string GetFileNameNoExtension(string filePath)
    {
        // 获取文件的名称
        FileInfo fi = new(filePath);
        return fi.Name.Split('.')[0];
    }

    /// <summary>
    /// 从文件的绝对路径中获取文件名(包含扩展名)
    /// </summary>
    /// <param name="filePath">文件的绝对路径</param>
    public static string GetFileNameWithExtension(string filePath)
    {
        // 获取文件的名称
        FileInfo fi = new(filePath);
        return fi.Name;
    }

    /// <summary>
    /// 获取指定目录中所有文件列表
    /// </summary>
    /// <param name="directoryPath">指定目录的绝对路径</param>
    public static string[] GetFiles(string directoryPath)
    {
        // 如果目录不存在，则抛出异常
        if (!IsExistDirectory(directoryPath))
        {
            throw new FileNotFoundException();
        }
        // 获取文件列表
        return Directory.GetFiles(directoryPath);
    }

    /// <summary>
    /// 查找指定目录及子目录中指定名称文件列表
    /// </summary>
    /// <param name="directoryPath">指定目录的绝对路径</param>
    /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
    /// <param name="isSearchChild">是否搜索子目录</param>
    public static string[] GetFiles(string directoryPath, string searchPattern, bool isSearchChild)
    {
        // 如果目录不存在，则抛出异常
        if (!IsExistDirectory(directoryPath))
        {
            throw new FileNotFoundException();
        }
        try
        {
            if (isSearchChild) return Directory.GetFiles(directoryPath, searchPattern, SearchOption.AllDirectories);
            else return Directory.GetFiles(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
        }
        catch (IOException ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// 获取指定文件大小
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static long GetFileSize(string filePath)
    {
        try
        {
            long temp = 0;
            // 判断当前路径所指向的是否为文件
            if (File.Exists(filePath))
            {
                // 定义一个FileInfo对象,使之与filePath所指向的文件向关联,以获取其大小
                FileInfo fileInfo = new(filePath);
                return fileInfo.Length;
            }
            else
            {
                string[] str1 = Directory.GetFileSystemEntries(filePath);
                foreach (string s1 in str1)
                {
                    temp += GetFileSize(s1);
                }
            }
            return temp;
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// 获取指定驱动器剩余空间大小
    /// </summary>
    /// <param name="hardDiskName"></param>
    /// <returns></returns>
    public static long GetHardDiskFreeSpace(string hardDiskName)
    {
        return new DriveInfo(hardDiskName).TotalFreeSpace;
    }

    /// <summary>
    /// 获取指定驱动器总空间大小
    /// </summary>
    /// <param name="hardDiskName"></param>
    /// <returns></returns>
    public static long GetHardDiskSpace(string hardDiskName)
    {
        return new DriveInfo(hardDiskName).TotalSize;
    }

    /// <summary>
    /// 获取文本文件的行数
    /// </summary>
    /// <param name="filePath">文件的绝对路径</param>
    public static int GetLineCount(string filePath)
    {
        // 将文本文件的各行读到一个字符串数组中
        string[] rows = File.ReadAllLines(filePath);
        // 返回行数
        return rows.Length;
    }

    /// <summary>
    /// 检测指定目录中是否存在指定的文件,若要搜索子目录请使用重载方法
    /// </summary>
    /// <param name="directoryPath">指定目录的绝对路径</param>
    /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static bool IsContainsFiles(string directoryPath, string searchPattern)
    {
        try
        {
            // 获取指定的文件列表
            string[] fileNames = GetFiles(directoryPath, searchPattern, false);
            // 判断指定文件是否存在
            if (fileNames.Length == 0) return false;
            else return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// 检测指定目录中是否存在指定的文件
    /// </summary>
    /// <param name="directoryPath">指定目录的绝对路径</param>
    /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
    /// <param name="isSearchChild">是否搜索子目录</param>
    public static bool IsContainsFiles(string directoryPath, string searchPattern, bool isSearchChild)
    {
        try
        {
            //获取指定的文件列表
            string[] fileNames = GetFiles(directoryPath, searchPattern, true);
            //判断指定文件是否存在
            if (fileNames.Length == 0) return false;
            else return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// 检测指定目录是否为空
    /// </summary>
    /// <param name="directoryPath">指定目录的绝对路径</param>
    public static bool IsEmptyDirectory(string directoryPath)
    {
        try
        {
            // 判断是否存在文件
            string[] fileNames = GetFiles(directoryPath);
            if (fileNames.Length > 0) return false;
            // 判断是否存在文件夹
            string[] directoryNames = GetDirectories(directoryPath);
            if (directoryNames.Length > 0) return false;
            return true;
        }
        catch
        {
            return true;
        }
    }

    /// <summary>
    /// 检测指定目录是否存在
    /// </summary>
    /// <param name="directoryPath">目录的绝对路径</param>
    /// <returns></returns>
    public static bool IsExistDirectory(string directoryPath)
    {
        return Directory.Exists(directoryPath);
    }

    /// <summary>
    /// 检测指定文件是否存在,如果存在则返回true。
    /// </summary>
    /// <param name="filePath">文件的绝对路径</param>
    public static bool IsExistFile(string filePath)
    {
        return File.Exists(filePath);
    }

    /// <summary>
    /// 移动文件(剪贴粘贴)
    /// </summary>
    /// <param name="dir1">旧文件路径</param>
    /// <param name="dir2">新文件路径</param>
    public static void MoveFile(string dir1, string dir2)
    {
        dir1 = dir1.Replace(@"/", @"\");
        dir2 = dir2.Replace(@"/", @"\");
        if (File.Exists(dir1)) File.Move(dir1, dir2);
    }

    /// <summary>
    /// 将文件移动到指定目录
    /// </summary>
    /// <param name="sourceFilePath">文件路径</param>
    /// <param name="descDirectoryPath">目标目录路径</param>
    public static void MoveFileToDirectory(string sourceFilePath, string descDirectoryPath)
    {
        // 获取源文件的名称
        string sourceFileName = GetFileNameWithExtension(sourceFilePath);
        if (IsExistDirectory(descDirectoryPath))
        {
            // 如果目标中存在同名文件,则删除
            if (IsExistFile(descDirectoryPath + @"\" + sourceFileName))
            {
                DeleteFile(descDirectoryPath + @"\" + sourceFileName);
            }
            // 将文件移动到指定目录
            File.Move(sourceFilePath, descDirectoryPath + @"\" + sourceFileName);
        }
    }

    /// <summary>
    /// 指定驱动器剩余空间大小与总空间大小占比
    /// </summary>
    /// <param name="hardDiskName"></param>
    /// <returns></returns>
    public static string ProportionOfHardDiskFreeSpace(string hardDiskName)
    {
        return Math.Round((decimal)GetHardDiskFreeSpace(hardDiskName) / GetHardDiskSpace(hardDiskName) * 100, 3) + " %";
    }

    /// <summary>
    /// 向文本文件中写入内容
    /// </summary>
    /// <param name="filePath">文件的绝对路径</param>
    /// <param name="text">写入的内容</param>
    /// <param name="encoding">编码</param>
    public static void WriteText(string filePath, string text, Encoding encoding)
    {
        // 向文件写入内容
        File.WriteAllText(filePath, text, encoding);
    }
}