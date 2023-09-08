#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysLogVisit
// Guid:8c87ae80-ee6d-4deb-a4a0-6314a90204e9
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/19 5:19:58
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Attributes;
using XiHan.Models.Bases.Entities;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统访问日志表
/// </summary>
/// <remarks>记录新增信息</remarks>
[SugarTable, SystemTable]
public class SysLogVisit : BaseCreateEntity
{
    #region 客户端信息

    /// <summary>
    /// 是否是 ajax 请求
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public bool? IsAjaxRequest { get; set; }

    /// <summary>
    /// 请求类型 GET、POST等
    /// </summary>
    [SugarColumn(Length = 16, IsNullable = true)]
    public string? RequestMethod { get; set; }

    /// <summary>
    /// 请求地址
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public string? RequestUrl { get; set; }

    /// <summary>
    /// 语言
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public string? Language { get; set; }

    /// <summary>
    /// 来源页面
    /// </summary>
    [SugarColumn(Length = 256, IsNullable = true)]
    public string? Referrer { get; set; }

    /// <summary>
    /// 代理信息
    /// </summary>
    [SugarColumn(Length = 256, IsNullable = true)]
    public string? Agent { get; set; }

    /// <summary>
    /// 设备类型
    /// </summary>
    [SugarColumn(Length = 256, IsNullable = true)]
    public string? DeviceType { get; set; } = string.Empty;

    /// <summary>
    /// 系统名称、版本
    /// </summary>
    [SugarColumn(Length = 256, IsNullable = true)]
    public string? Os { get; set; }

    /// <summary>
    /// 浏览器名称、版本
    /// </summary>
    [SugarColumn(Length = 256, IsNullable = true)]
    public string? Browser { get; set; }

    #endregion

    #region 地址信息

    /// <summary>
    /// 操作Ip
    ///</summary>
    [SugarColumn(Length = 32, IsNullable = true)]
    public string? Ip { get; set; }

    /// <summary>
    /// 操作地点
    ///</summary>
    [SugarColumn(Length = 64, IsNullable = true)]
    public string? Location { get; set; }

    #endregion

    #region 权限信息

    /// <summary>
    /// 姓名
    /// </summary>
    [SugarColumn(Length = 64, IsNullable = true)]
    public string? RealName { get; set; }

    #endregion
}