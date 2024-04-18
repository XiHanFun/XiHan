#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IBaseService
// Guid:4dec286a-0cba-4242-8472-ae090c9335ba
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 5:45:55
// ----------------------------------------------------------------

#endregion <<版权版本注释>>


#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IBaseService
// Guid:4dec286a-0cba-4242-8472-ae090c9335ba
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 5:45:55
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Domain.Core.Repositories.Abstracts;

namespace XiHan.Domain.Core.Services.Abstracts;

/// <summary>
/// 通用服务接口
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IBaseService<TEntity> : IBaseRepository<TEntity> where TEntity : class, new();