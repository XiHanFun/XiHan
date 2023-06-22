#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SysOperationLog
// long:d7a2c392-4915-4831-9b7c-5cde51f9d618
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-21 下午 04:52:59
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using System.ComponentModel;
using XiHan.Models.Bases.Entity;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统操作日志表
/// </summary>
/// <remarks>记录创建信息</remarks>
[SugarTable(TableName = "Sys_Operation_Log")]
public class SysOperationLog : BaseCreateEntity
{
    /// <summary>
    /// 操作模块
    ///</summary>
    [SugarColumn(Length = 20, IsNullable = true)]
    public string Module { get; set; } = string.Empty;

    /// <summary>
    /// 业务类型
    /// BusinessTypeEnum
    /// 0其它 1新增 2修改 3删除 4授权 5导出 6导入 7强退 8生成代码 9清空数据
    /// </summary>
    public int BusinessType { get; set; }

    /// <summary>
    /// 操作方法
    ///</summary>
    [SugarColumn(Length = 50, IsNullable = true)]
    public string? Method { get; set; }

    /// <summary>
    /// 请求类型
    /// HttpRequestMethodEnum GET、POST等
    /// </summary>
    public int HttpRequestMethod { get; set; }

    /// <summary>
    /// 操作人员Id
    ///</summary>
    [SugarColumn(Length = 20, IsNullable = true)]
    public string? UserId { get; set; }

    /// <summary>
    /// 操作人员
    ///</summary>
    [SugarColumn(Length = 20, IsNullable = true)]
    public string? UserName { get; set; }

    /// <summary>
    /// 操作Ip
    ///</summary>
    [SugarColumn(Length = 20, IsNullable = true)]
    public string? Ip { get; set; }

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
    /// 请求参数
    ///</summary>
    [SugarColumn(Length = 200, IsNullable = true)]
    public string? RequestParam { get; set; }

    /// <summary>
    /// 请求结果
    ///</summary>
    [SugarColumn(Length = 4000, IsNullable = true)]
    public string? RequestResult { get; set; }

    /// <summary>
    /// 错误消息
    /// </summary>
    [DisplayName("错误消息")]
    [SugarColumn(Length = 4000, IsNullable = true)]
    public string? ErrorMsg { get; set; }

    /// <summary>
    /// 操作用时
    /// </summary>
    [DisplayName("操作用时")]
    public long Elapsed { get; set; }
}