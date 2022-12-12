#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BusinessTypeEnum
// Guid:0b724ed1-caaf-45e2-867a-6690eeaa0753
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-12-13 上午 01:13:35
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace ZhaiFanhuaBlog.Infrastructure.Enums;

/// <summary>
/// 业务操作类型
/// </summary>
public enum BusinessTypeEnum
{
    /// <summary>
    /// 其它
    /// </summary>
    [Description("其它")]
    OTHER = 0,

    /// <summary>
    /// 新增
    /// </summary>
    [Description("新增")]
    INSERT = 1,

    /// <summary>
    /// 修改
    /// </summary>
    [Description("修改")]
    UPDATE = 2,

    /// <summary>
    /// 删除
    /// </summary>
    [Description("删除")]
    DELETE = 3,

    /// <summary>
    /// 授权
    /// </summary>
    [Description("授权")]
    GRANT = 4,

    /// <summary>
    /// 导出
    /// </summary>
    [Description("导出")]
    EXPORT = 5,

    /// <summary>
    /// 导入
    /// </summary>
    [Description("导入")]
    IMPORT = 6,

    /// <summary>
    /// 强退
    /// </summary>
    [Description("强退")]
    FORCE = 7,

    /// <summary>
    /// 生成代码
    /// </summary>
    [Description("生成代码")]
    GENCODE = 8,

    /// <summary>
    /// 清空数据
    /// </summary>
    [Description("清空数据")]
    CLEAN = 9,
}