#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BaseService
// Guid:7dcbfb12-fc3a-484a-bf10-7276f890c4de
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 5:50:00
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructure.Persistence.Repositories;

namespace XiHan.Application.Core.Services;

/// <summary>
/// 服务基类
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <remarks>
/// 服务方法命名规范(这里由于继承自BaseRepository，所以不更改)：
/// 新增：Create
/// 删除：Delete
/// 修改：Modify
/// 查询：Get
/// </remarks>
public class BaseService<TEntity> : BaseRepository<TEntity> where TEntity : class, new();