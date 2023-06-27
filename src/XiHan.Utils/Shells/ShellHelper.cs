#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:ShellHelper
// Guid:11a08ee1-6099-4d00-9545-a177bf8a8393
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-09 上午 05:25:38
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Diagnostics;

namespace XiHan.Utils.Shells;

/// <summary>
/// ShellHelper
/// </summary>
public static class ShellHelper
{
    /// <summary>
    /// Linux 系统命令
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public static string Bash(string command)
    {
        var escapedArgs = command.Replace(@"""", @"\""");
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = @"/bin/bash",
                Arguments = $@"-c ""{escapedArgs}""",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };
        process.Start();
        var result = process.StandardOutput.ReadToEnd();
        process.WaitForExit();
        process.Dispose();
        return result;
    }

    /// <summary>
    /// Windows 系统命令
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static string Cmd(string fileName, string args)
    {
        var output = string.Empty;

        var info = new ProcessStartInfo
        {
            FileName = fileName,
            Arguments = args,
            RedirectStandardOutput = true
        };

        using var process = Process.Start(info);
        if (process == null) return output;
        output = process.StandardOutput.ReadToEnd();
        return output;
    }
}