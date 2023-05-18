#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ISoftDelete
// long:1814189a-40ec-447b-95ad-8d77a973df7e
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-05-18 下午 05:52:13
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Models.Bases.Interface;

/// <summary>
/// 是否软删除
/// </summary>
public interface ISoftDelete
{
    /// <summary>
    /// 是否已删除
    /// </summary>
    bool IsDeleted { get; set; }
}