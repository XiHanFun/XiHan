#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ProjectInfoHelper
// Guid:d32ec338-c3a6-4fac-85e5-b76de49d6f6c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/7/21 4:49:46
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Infrastructures.Infos;

/// <summary>
/// 项目信息帮助类
/// </summary>
public static class ProjectInfoHelper
{
    /// <summary>
    /// Logo
    /// </summary>
    public static string Logo => $@"
██╗  ██╗██╗██╗  ██╗ █████╗ ███╗   ██╗
╚██╗██╔╝██║██║  ██║██╔══██╗████╗  ██║
 ╚███╔╝ ██║███████║███████║██╔██╗ ██║
 ██╔██╗ ██║██╔══██║██╔══██║██║╚██╗██║
██╔╝ ██╗██║██║  ██║██║  ██║██║ ╚████║
╚═╝  ╚═╝╚═╝╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝";

    /// <summary>
    /// 寄语
    /// </summary>
    public static string SendWord => $@"碧落降恩承淑颜，共挚崎缘挽曦寒。迁般故事终成忆，谨此葳蕤换思短。";

    /// <summary>
    /// Copyright
    /// </summary>
    public static string Copyright => $@"Copyright (C){DateTime.Now.Year} ZhaiFanhua All Rights Reserved.";

    /// <summary>
    /// 官方文档
    /// </summary>
    public static string OfficialDocuments => $@"https://docs.xihan.fun";

    /// <summary>
    /// 官方组织
    /// </summary>
    public static string OfficialOrganization => $@"https://github.com/XiHanFun";

    /// <summary>
    /// 源码仓库
    /// </summary>
    public static string SourceCodeRepository => $@"https://github.com/XiHanFun/XiHan-Backend";
}