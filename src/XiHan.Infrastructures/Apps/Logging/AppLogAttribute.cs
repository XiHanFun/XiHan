#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:AppLogAttribute
// Guid:072cbfb1-e0d3-43b9-8a49-20e42986b30a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-12-07 下午 08:15:49
// ----------------------------------------------------------------

#endregion <<版权版本注释>>


namespace XiHan.Infrastructures.Apps.Logging;

/// <summary>
/// 日志标记
/// </summary>
[AttributeUsage(AttributeTargets.All, Inherited = false)]
public class AppLogAttribute : Attribute
{
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 日志类型
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
    /// <param name="name"></param>
    public AppLogAttribute(string name)
    {
        Title = name;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name"></param>
    /// <param name="businessType"></param>
    /// <param name="saveRequestData"></param>
    /// <param name="saveResponseData"></param>
    public AppLogAttribute(string name, BusinessTypeEnum businessType, bool saveRequestData = true, bool saveResponseData = true)
    {
        Title = name;
        BusinessType = businessType;
        IsSaveRequestData = saveRequestData;
        IsSaveResponseData = saveResponseData;
    }
}