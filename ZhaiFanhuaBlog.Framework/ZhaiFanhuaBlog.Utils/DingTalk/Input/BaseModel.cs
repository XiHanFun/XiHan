// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BaseMessage
// Guid:0b12b734-f4e4-4d13-b275-adfba426bd2a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-03 上午 01:55:59
// ----------------------------------------------------------------

namespace ZhaiFanhuaBlog.Utils.DingTalk.Input;

/// <summary>
/// BaseMessage
/// </summary>
public class BaseModel : IBaseModel
{
    /// <summary>
    /// @相关
    /// </summary>
    public BaseAtModel? At { get; set; }
}

/// <summary>
/// BaseMessage
/// </summary>
public class BaseAtModel
{
    /// <summary>
    /// @某成员
    /// </summary>
    public string AtMobiles { get; set; } = string.Empty;

    /// <summary>
    /// 是否@全员
    /// </summary>
    public bool IsAtAll { get; set; } = false;
}