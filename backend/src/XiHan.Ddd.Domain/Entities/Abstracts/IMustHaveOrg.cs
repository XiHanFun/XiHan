#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IMustHaveOrg
// Guid:f7181676-8bca-492c-b222-6549a152fbb6
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/26 7:03:58
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Ddd.Domain.Entities.Abstracts;

/// <summary>
/// 通用机构接口
/// </summary>
public interface IMustHaveOrg<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// 机构标识
    /// </summary>
    TKey OrgId { get; }
}