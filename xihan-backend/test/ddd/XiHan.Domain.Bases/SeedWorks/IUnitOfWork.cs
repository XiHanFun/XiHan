#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IUnitOfWork
// Guid:7cde97ed-4998-4a10-855b-012df49ccadb
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/2/5 12:33:49
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Domain.Bases.SeedWorks;

/// <summary>
/// 工作单元定义
/// </summary>
public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
}