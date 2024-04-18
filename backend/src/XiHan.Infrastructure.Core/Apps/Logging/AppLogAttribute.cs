#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:AppLogAttribute
// Guid:672b6e4c-3fb5-44f4-9d0c-f095672edc4e
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/29 3:30:56
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Infrastructure.Core.Apps.Logging;

/// <summary>
/// 日志标记
/// </summary>
[AttributeUsage(AttributeTargets.All, Inherited = false)]
public class AppLogAttribute : Attribute
{
    /// <summary>
    /// 操作模块
    /// </summary>
    public string Module { get; set; } = string.Empty;

    /// <summary>
    /// 业务类型
    /// </summary>
    public BusinessTypeEnum BusinessType { get; set; }

    /// <summary>
    /// 是否保存请求数据
    /// </summary>
    public bool IsSaveRequestData { get; set; } = true;

    /// <summary>
    /// 是否保存返回数据
    /// </summary>
    public bool IsSaveResponseData { get; set; } = true;

    /// <summary>
    /// 构造函数
    /// </summary>
    public AppLogAttribute()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="module"></param>
    /// <param name="businessType"></param>
    public AppLogAttribute(string module, BusinessTypeEnum businessType)
    {
        Module = module;
        BusinessType = businessType;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="module"></param>
    /// <param name="businessType"></param>
    /// <param name="saveRequestData"></param>
    /// <param name="saveResponseData"></param>
    public AppLogAttribute(string module, BusinessTypeEnum businessType, bool saveRequestData = true, bool saveResponseData = true)
    {
        Module = module;
        BusinessType = businessType;
        IsSaveRequestData = saveRequestData;
        IsSaveResponseData = saveResponseData;
    }
}