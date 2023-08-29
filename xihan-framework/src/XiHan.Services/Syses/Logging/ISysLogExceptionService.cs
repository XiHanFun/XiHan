#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ISysLogExceptionService
// Guid:d8291498-8a92-40a9-a37b-4dd187725363
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/7/19 1:14:54
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Logging.Dtos;

namespace XiHan.Services.Syses.Logging;

/// <summary>
/// ISysLogExceptionService
/// </summary>
public interface ISysLogExceptionService : IBaseService<SysLogException>
{
    /// <summary>
    /// 新增系统异常日志
    /// </summary>
    /// <param name="logException"></param>
    /// <returns></returns>
    Task<bool> CreateLogException(SysLogException logException);

    /// <summary>
    /// 批量删除系统异常日志
    /// </summary>
    /// <param name="logIds"></param>
    /// <returns></returns>
    Task<bool> DeleteLogExceptionByIds(long[] logIds);

    /// <summary>
    /// 清空系统异常日志
    /// </summary>
    /// <returns></returns>
    Task<bool> CleanLogException();

    /// <summary>
    /// 查询系统系统异常日志(根据异常Id)
    /// </summary>
    /// <param name="exceptionId"></param>
    /// <returns></returns>
    Task<SysLogException> GetLogExceptionById(long exceptionId);

    /// <summary>
    /// 查询系统系统异常日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    Task<List<SysLogException>> GetLogExceptionList(SysLogExceptionWDto whereDto);

    /// <summary>
    /// 查询系统系统异常日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    Task<PageDataDto<SysLogException>> GetLogExceptionPageList(PageWhereDto<SysLogExceptionWDto> pageWhere);
}