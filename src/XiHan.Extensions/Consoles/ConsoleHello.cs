#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:ConsoleHello
// Guid:5cf676a5-01b1-4135-90d2-976b1f491419
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-10 下午 11:12:04
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Utils.Consoles;

namespace XiHan.Extensions.Consoles;

/// <summary>
/// ConsoleHello
/// </summary>
public static class ConsoleHello
{
    /// <summary>
    /// 欢迎使用曦寒
    /// </summary>
    public static void SayHello()
    {
        Logo.WriteLineHandle();
        SendWord.WriteLineHandle();
        Copyright.WriteLineHandle();
        OfficialDocuments.WriteLineHandle();
        OfficialOrganization.WriteLineHandle();
        SourceCodeRepository.WriteLineHandle();
    }

    /// <summary>
    /// Logo
    /// </summary>
    public static string Logo { get; set; } = $@"
██╗  ██╗██╗██╗  ██╗ █████╗ ███╗   ██╗
╚██╗██╔╝██║██║  ██║██╔══██╗████╗  ██║
 ╚███╔╝ ██║███████║███████║██╔██╗ ██║
 ██╔██╗ ██║██╔══██║██╔══██║██║╚██╗██║
██╔╝ ██╗██║██║  ██║██║  ██║██║ ╚████║
╚═╝  ╚═╝╚═╝╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝";

    /// <summary>
    /// 寄语
    /// </summary>
    public static string SendWord = $@"碧落降恩承淑颜，共挚崎缘挽曦寒。迁般故事终成忆，谨此葳蕤换思短。";

    /// <summary>
    /// 未来
    /// </summary>
    public static string Future = $@"碧落降恩承淑颜，共挚崎缘挽曦寒。迁般故事终成忆，谨此葳蕤换思短。";

    /// <summary>
    /// Copyright
    /// </summary>
    public static string Copyright { get; set; } = $@"Copyright (C){DateTime.Now.Year} ZhaiFanhua All Rights Reserved.";

    /// <summary>
    /// 官方文档
    /// </summary>
    public static string OfficialDocuments { get; set; } = $@"官方文档：https://docs.xihan.fun";

    /// <summary>
    /// 官方组织
    /// </summary>
    public static string OfficialOrganization { get; set; } = $@"官方组织：https://github.com/XiHanFun";

    /// <summary>
    /// 源码仓库
    /// </summary>
    public static string SourceCodeRepository { get; set; } = $@"源码仓库：https://github.com/XiHanFun/XiHan.Framework";
}