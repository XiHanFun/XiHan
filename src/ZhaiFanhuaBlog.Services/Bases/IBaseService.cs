#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IBaseService
// Guid:368c93d7-dc11-4f23-a16d-c6bef363e3e0
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 10:16:45
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Repositories.Bases;

namespace ZhaiFanhuaBlog.Services.Bases;

/// <summary>
/// 服务基类接口
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IBaseService<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
{
}