// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BaseResultFieldDto
// Guid:8129e62a-4ccc-409f-9332-d60794fbb9b5
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-20 上午 12:49:29
// ----------------------------------------------------------------

namespace ZhaiFanhuaBlog.ViewModels.Bases.Results;

/// <summary>
/// 通用返回字段实体
/// </summary>
public class BaseResultFieldDto
{
    /// <summary>
    /// 主键标识
    /// </summary>
    public Guid BaseId { get; set; }

    /// <summary>
    /// 创建用户
    /// </summary>
    public Guid? CreateId { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// 修改用户
    /// </summary>
    public Guid? ModifyId { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    public DateTime? ModifyTime { get; set; }

    /// <summary>
    /// 审核用户
    /// </summary>
    public Guid? AuditId { get; set; }

    /// <summary>
    /// 审核时间
    /// </summary>
    public DateTime? AuditTime { get; set; }

    /// <summary>
    /// 删除用户
    /// </summary>
    public Guid? DeleteId { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    public DateTime? DeleteTime { get; set; }

    /// <summary>
    /// 类型名称（中文表名称）
    /// </summary>
    public string? TypeName { get; set; }

    /// <summary>
    /// 状态名称
    /// </summary>
    public string? StateName { get; set; }
}