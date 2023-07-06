#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BaseService
// Guid:26bf5f09-21b1-40cf-9bb7-25402f70baf2
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 10:19:56
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Repositories.Bases;

namespace XiHan.Services.Bases;

/// <summary>
/// 服务基类
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <remarks>
/// 方法命名规范（这里由于继承自BaseRepository，所以不更改）：
/// Create
/// Delete
/// Modify
/// Get
/// </remarks>
public class BaseService<TEntity> : BaseRepository<TEntity> where TEntity : class, new()
{
}