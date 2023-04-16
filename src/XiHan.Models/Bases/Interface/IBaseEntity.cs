#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IBaseEntity
// long:c54f6677-db24-4a58-91a8-97bf61cfdd27
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-05 上午 12:42:39
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Models.Bases.Interface;

/// <summary>
/// IBaseEntity
/// </summary>
public interface IBaseEntity<TKey>
{
    /// <summary>
    /// 主键
    /// </summary>
    TKey BaseId { get; set; }
}