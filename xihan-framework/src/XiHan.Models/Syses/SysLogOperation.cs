#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysLogOperation
// Guid:d7a2c392-4915-4831-9b7c-5cde51f9d618
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-11-21 下午 04:52:59
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Infrastructures.Apps.Logging;
using XiHan.Models.Bases.Attributes;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统操作日志表
/// </summary>
/// <remarks>记录新增信息</remarks>
[SystemTable]
[SplitTable(SplitType.Month)]
[SugarTable($@"{nameof(SysLogOperation)}_{{year}}{{month}}{{day}}")]
public class SysLogOperation : SysLogVisit
{
    /// <summary>
    /// 操作模块
    ///</summary>
    [SugarColumn(Length = 32, IsNullable = true)]
    public string? Module { get; set; }

    /// <summary>
    /// 业务类型
    /// 0其它 1新增 2修改 3删除 4授权 5导出 6导入 7强退 8生成代码 9清空数据
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public BusinessTypeEnum? BusinessType { get; set; }

    /// <summary>
    /// 请求参数
    ///</summary>
    [SugarColumn(Length = 512, IsNullable = true)]
    public string? RequestParameters { get; set; }

    /// <summary>
    /// 响应结果
    ///</summary>
    [SugarColumn(Length = 512, IsNullable = true)]
    public string? ResponseResult { get; set; }

    /// <summary>
    /// 操作状态(true 正常 false异常)
    /// </summary>
    [SugarColumn]
    public bool Status { get; set; } = true;

    /// <summary>
    /// 操作用时
    /// </summary>
    [SugarColumn]
    public long ElapsedTime { get; set; }
}