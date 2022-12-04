#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ISysDictionaryService
// Guid:8b2a6d03-4267-4457-a19c-1fddedf0356c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-24 上午 01:19:02
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Infrastructure.Services;
using ZhaiFanhuaBlog.Models.Sys;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Sys;

/// <summary>
/// ISysDictionaryService
/// </summary>
public interface ISysDictionaryService : IBaseService<SysDicType>, IScopeDependency
{
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="SysDictionarys"></param>
    /// <returns></returns>
    Task<bool> InitSysDictionaryAsync(List<SysDicType> SysDictionarys);
}