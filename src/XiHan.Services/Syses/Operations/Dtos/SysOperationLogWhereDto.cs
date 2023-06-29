#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:SysOperationLogWhereDto
// Guid:3f7ca12f-9a86-4e07-b304-1a0f951a133c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-06-30 上午 02:50:05
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Services.Syses.Operations.Dtos;

/// <summary>
/// SysOperationLogWhereDto
/// </summary>
public abstract class SysOperationLogWhereDto
{
    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? BeginTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// 操作模块
    ///</summary>
    public string? Module { get; set; }

    /// <summary>
    /// 业务类型
    /// BusinessTypeEnum
    /// 0其它 1新增 2修改 3删除 4授权 5导出 6导入 7强退 8生成代码 9清空数据
    /// </summary>
    public int? BusinessType { get; set; }

    /// <summary>
    /// 请求类型
    /// HttpRequestMethodEnum GET、POST等
    /// </summary>
    public int? HttpRequestMethod { get; set; }

    /// <summary>
    /// 操作状态（true 正常 false异常）
    /// </summary>
    public bool? Status { get; set; }
}