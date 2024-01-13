#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysJobCDto
// Guid:09dece81-20d6-4d30-afea-80d95d375ce3
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/29 23:18:17
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;
using XiHanCore.Infrastructures.Requests.Https;
using XiHanCore.Models.Syses.Enums;

namespace XiHanCore.Services.Syses.Jobs.Dtos;

/// <summary>
/// SysJobCDto
/// </summary>
public class SysJobCDto
{
    /// <summary>
    /// 任务分组
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(4, ErrorMessage = "{0}不能少于{1}个字")]
    [MaxLength(32, ErrorMessage = "{0}不能多于{1}个字")]
    public string Group { get; set; } = string.Empty;

    /// <summary>
    /// 任务名称
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(4, ErrorMessage = "{0}不能少于{1}个字")]
    [MaxLength(32, ErrorMessage = "{0}不能多于{1}个字")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 是否启动
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    public bool IsStart { get; set; }

    /// <summary>
    /// 任务描述
    /// </summary>
    [MaxLength(256, ErrorMessage = "{0}不能多于{1}个字")]
    public string? Description { get; set; }

    #region 任务类型

    /// <summary>
    /// 任务类型
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    public JobTypeEnum JobType { get; set; }

    #region 程序集

    /// <summary>
    /// 程序集名称
    /// </summary>
    [MaxLength(256, ErrorMessage = "{0}不能多于{1}个字")]
    public string? AssemblyName { get; set; }

    /// <summary>
    /// 任务所在类
    /// </summary>
    [MaxLength(256, ErrorMessage = "{0}不能多于{1}个字")]
    public string? ClassName { get; set; }

    #endregion

    #region 网络请求

    /// <summary>
    /// 网络请求方式
    /// </summary>
    public RequestMethodEnum? RequestMethod { get; set; }

    /// <summary>
    /// Api执行地址
    /// </summary>
    [MaxLength(256, ErrorMessage = "{0}不能多于{1}个字")]
    public string? ApiUrl { get; set; }

    /// <summary>
    /// 传入参数
    /// </summary>
    [MaxLength(512, ErrorMessage = "{0}不能多于{1}个字")]
    public string? Params { get; set; }

    #endregion

    #region SQL语句

    /// <summary>
    /// SQL语句
    /// </summary>
    public string? SqlText { get; set; }

    #endregion

    #endregion

    #region 触发器类型

    /// <summary>
    /// 触发器类型
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    public TriggerTypeEnum TriggerType { get; set; }

    #region 定时任务

    /// <summary>
    /// 执行间隔时间 单位秒
    /// </summary>
    public int? IntervalSecond { get; set; }

    /// <summary>
    /// 循环执行次数
    /// </summary>
    public int? CycleRunTimes { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? BeginTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }

    #endregion

    #region 时间点或者周期性任务

    /// <summary>
    /// 运行时间表达式
    /// </summary>
    [RegularExpression(
        @"^\\s*($|#|\\w+\\s*=|(\\?|\\*|(?:[0-5]?\\d)(?:(?:-|\\/|\\,)(?:[0-5]?\\d))?(?:,(?:[0-5]?\\d)(?:(?:-|\\/|\\,)(?:[0-5]?\\d))?)*)\\s+(\\?|\\*|(?:[0-5]?\\d)(?:(?:-|\\/|\\,)(?:[0-5]?\\d))?(?:,(?:[0-5]?\\d)(?:(?:-|\\/|\\,)(?:[0-5]?\\d))?)*)\\s+(\\?|\\*|(?:[01]?\\d|2[0-3])(?:(?:-|\\/|\\,)(?:[01]?\\d|2[0-3]))?(?:,(?:[01]?\\d|2[0-3])(?:(?:-|\\/|\\,)(?:[01]?\\d|2[0-3]))?)*)\\s+(\\?|\\*|(?:0?[1-9]|[12]\\d|3[01])(?:(?:-|\\/|\\,)(?:0?[1-9]|[12]\\d|3[01]))?(?:,(?:0?[1-9]|[12]\\d|3[01])(?:(?:-|\\/|\\,)(?:0?[1-9]|[12]\\d|3[01]))?)*)\\s+(\\?|\\*|(?:[1-9]|1[012])(?:(?:-|\\/|\\,)(?:[1-9]|1[012]))?(?:L|W)?(?:,(?:[1-9]|1[012])(?:(?:-|\\/|\\,)(?:[1-9]|1[012]))?(?:L|W)?)*|\\?|\\*|(?:JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC)(?:(?:-)(?:JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC))?(?:,(?:JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC)(?:(?:-)(?:JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC))?)*)\\s+(\\?|\\*|(?:[0-6])(?:(?:-|\\/|\\,|#)(?:[0-6]))?(?:L)?(?:,(?:[0-6])(?:(?:-|\\/|\\,|#)(?:[0-6]))?(?:L)?)*|\\?|\\*|(?:MON|TUE|WED|THU|FRI|SAT|SUN)(?:(?:-)(?:MON|TUE|WED|THU|FRI|SAT|SUN))?(?:,(?:MON|TUE|WED|THU|FRI|SAT|SUN)(?:(?:-)(?:MON|TUE|WED|THU|FRI|SAT|SUN))?)*)(|\\s)+(\\?|\\*|(?:|\\d{4})(?:(?:-|\\/|\\,)(?:|\\d{4}))?(?:,(?:|\\d{4})(?:(?:-|\\/|\\,)(?:|\\d{4}))?)*))$",
        ErrorMessage = "Cron表达式无效")]
    public string? Cron { get; set; }

    #endregion

    #endregion
}