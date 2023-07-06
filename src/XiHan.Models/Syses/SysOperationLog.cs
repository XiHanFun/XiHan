#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysOperationLog
// Guid:d7a2c392-4915-4831-9b7c-5cde51f9d618
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-11-21 下午 04:52:59
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Entity;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统操作日志表
/// </summary>
/// <remarks>记录新增信息</remarks>
[SugarTable(TableName = "Sys_Operation_Log")]
public class SysOperationLog : BaseCreateEntity
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
    [SugarColumn(Length = 10, IsNullable = true)]
    public string? RequestMethod { get; set; }

    /// <summary>
    /// 请求地址
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public string? RequestUrl { get; set; }

    /// <summary>
    /// 操作地点
    ///</summary>
    [SugarColumn(Length = 50, IsNullable = true)]
    public string? Location { get; set; }

    /// <summary>
    /// 来源页面
    /// </summary>
    [SugarColumn(Length = 100, IsNullable = true)]
    public string? Referrer { get; set; }

    /// <summary>
    /// 代理信息
    /// </summary>
    [SugarColumn(Length = 200, IsNullable = true)]
    public string? Agent { get; set; }

    /// <summary>
    /// 操作Ip
    ///</summary>
    [SugarColumn(Length = 20, IsNullable = true)]
    public string? Ip { get; set; }

    #endregion

    /// <summary>
    /// 操作方法
    ///</summary>
    [SugarColumn(Length = 50, IsNullable = true)]
    public string? Method { get; set; }

    /// <summary>
    /// 操作模块
    ///</summary>
    [SugarColumn(Length = 20, IsNullable = true)]
    public string? Module { get; set; }

    /// <summary>
    /// 业务类型
    /// BusinessTypeEnum
    /// 0其它 1新增 2修改 3删除 4授权 5导出 6导入 7强退 8生成代码 9清空数据
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public int? BusinessType { get; set; }

    /// <summary>
    /// 请求参数
    ///</summary>
    [SugarColumn(Length = 200, IsNullable = true)]
    public string? RequestParameters { get; set; }

    /// <summary>
    /// 响应结果
    ///</summary>
    [SugarColumn(Length = 4000, IsNullable = true)]
    public string? ResponseResult { get; set; }

    /// <summary>
    /// 操作状态（true 正常 false异常）
    /// </summary>
    public bool Status { get; set; } = true;

    /// <summary>
    /// 错误消息
    /// </summary>
    [SugarColumn(Length = 4000, IsNullable = true)]
    public string? ErrorMsg { get; set; }

    /// <summary>
    /// 操作用时
    /// </summary>
    public long ElapsedTime { get; set; }
}