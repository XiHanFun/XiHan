#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:EnumDescDto
// Guid:a1b01a18-a682-48dd-8b5f-7a046c120626
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-02-20 下午 02:52:42
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace ZhaiFanhuaBlog.Utils.Summary.Enums;

/// <summary>
/// EnumDescDto
/// </summary>
public class EnumDescDto
{
    /// <summary>
    /// 键
    /// </summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// 值
    /// </summary>
    public int Value { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string Label { get; set; } = string.Empty;
}