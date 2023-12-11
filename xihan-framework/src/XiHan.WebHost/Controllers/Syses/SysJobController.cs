#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysJobController
// Guid:0c1e2c70-82ae-4c79-8e24-d98969c41151
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/30 2:24:25
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using XiHan.Infrastructures.Apps.Logging;
using XiHan.Infrastructures.Responses;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Jobs.Bases.Servers;
using XiHan.Models.Syses;
using XiHan.Models.Syses.Enums;
using XiHan.Services.Syses.Jobs;
using XiHan.Services.Syses.Jobs.Dtos;
using XiHan.Utils.Exceptions;
using XiHan.Utils.Extensions;
using XiHan.WebCore.Common.Swagger;

namespace XiHan.WebHost.Controllers.Syses;

/// <summary>
/// 系统任务管理
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="sysJobService"></param>
/// <param name="taskSchedulerServer"></param>
[Authorize]
[ApiGroup(ApiGroupNameEnum.Manage)]
public class SysJobController(ISysJobService sysJobService, ITaskSchedulerServer taskSchedulerServer) : BaseApiController
{
    private readonly ISysJobService _sysJobService = sysJobService;
    private readonly ITaskSchedulerServer _taskSchedulerServer = taskSchedulerServer;

    /// <summary>
    /// 新增系统任务
    /// </summary>
    /// <param name="jobCDto"></param>
    /// <returns></returns>
    [HttpPost("Create")]
    [AppLog(Module = "系统任务", BusinessType = BusinessTypeEnum.Create)]
    public async Task<ApiResult> CreateJob([FromBody] SysJobCDto jobCDto)
    {
        _ = VerifyJobParams(jobCDto);

        var result = await _sysJobService.CreateJob(jobCDto);

        return ApiResult.Success(result);
    }

    /// <summary>
    /// 批量删除系统任务
    /// </summary>
    /// <param name="jobIds"></param>
    /// <returns></returns>
    [HttpDelete("Delete/ByIds")]
    [AppLog(Module = "系统任务", BusinessType = BusinessTypeEnum.Delete)]
    public async Task<ApiResult> DeleteJobByIds([FromBody] long[] jobIds)
    {
        Dictionary<SysJob, dynamic> errorMessages = [];
        List<SysJob> sysJobs = await _sysJobService.QueryAsync(j => jobIds.Contains(j.BaseId));
        sysJobs.ForEach(async sysJob =>
        {
            // 先删除任务调度中心的任务
            var result = await _taskSchedulerServer.DeleteTaskScheduleAsync(sysJob);
            if (result.IsSuccess)
                // 再删除数据库的任务
                _ = await _sysJobService.DeleteJobByIds([sysJob.BaseId]);
            errorMessages.Add(sysJob, result.Data);
        });

        return ApiResult.Success(errorMessages);
    }

    /// <summary>
    /// 修改系统任务
    /// </summary>
    /// <param name="jobMDto"></param>
    /// <returns></returns>
    [HttpPut("Modify")]
    [AppLog(Module = "系统任务", BusinessType = BusinessTypeEnum.Modify)]
    public async Task<ApiResult> ModifyJob([FromBody] SysJobMDto jobMDto)
    {
        _ = VerifyJobParams(jobMDto);

        var sysJob = await _sysJobService.GetJobById(jobMDto.BaseId);
        if (sysJob.IsStart) throw new CustomException($"该任务正在运行中，请先停止再更新！");

        // 先更新数据库的任务
        var result = await _sysJobService.ModifyJob(jobMDto);
        if (result)
            // 再更新任务调度中心的任务
            return await _taskSchedulerServer.ModifyTaskScheduleAsync(sysJob);

        return ApiResult.Success(result);
    }

    /// <summary>
    /// 启动系统任务
    /// </summary>
    /// <param name="jobIdDto"></param>
    /// <returns></returns>
    [HttpPut("Start")]
    [AppLog(Module = "系统任务", BusinessType = BusinessTypeEnum.Other)]
    public async Task<ApiResult> StartJob(SysJobIdDto jobIdDto)
    {
        var sysJob = await _sysJobService.GetJobById(jobIdDto.BaseId);

        // 先启动任务调度中心的任务
        var result = await _taskSchedulerServer.CreateTaskScheduleAsync(sysJob);
        if (result.IsSuccess)
        {
            sysJob.IsStart = true;
            // 再更新数据库的任务
            _ = await _taskSchedulerServer.ModifyTaskScheduleAsync(sysJob);
        }

        return ApiResult.Success(result);
    }

    /// <summary>
    /// 停止系统任务
    /// </summary>
    /// <param name="jobIdDto"></param>
    /// <returns></returns>
    [HttpPut("Stop")]
    [AppLog(Module = "系统任务", BusinessType = BusinessTypeEnum.Other)]
    public async Task<ApiResult> StopJob(SysJobIdDto jobIdDto)
    {
        var sysJob = await _sysJobService.GetJobById(jobIdDto.BaseId);

        // 先停止任务调度中心的任务
        var result = await _taskSchedulerServer.DeleteTaskScheduleAsync(sysJob);
        if (result.IsSuccess)
        {
            sysJob.IsStart = false;
            // 再更新数据库的任务
            _ = await _taskSchedulerServer.ModifyTaskScheduleAsync(sysJob);
        }

        return ApiResult.Success(result);
    }

    /// <summary>
    /// 立即执行一次系统任务
    /// </summary>
    /// <param name="jobIdDto"></param>
    /// <returns></returns>
    [HttpPut("Run")]
    [AppLog(Module = "系统任务", BusinessType = BusinessTypeEnum.Other)]
    public async Task<ApiResult> RunJob(SysJobIdDto jobIdDto)
    {
        var sysJob = await _sysJobService.GetJobById(jobIdDto.BaseId);

        var result = await _taskSchedulerServer.RunTaskScheduleAsync(sysJob);

        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统任务(根据Id)
    /// </summary>
    /// <param name="jobId"></param>
    /// <returns></returns>
    [HttpPost("Get/ById")]
    [AppLog(Module = "系统任务", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetJobById([FromBody] long jobId)
    {
        var result = await _sysJobService.GetJobById(jobId);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统任务列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    [HttpPost("Get/List")]
    [AppLog(Module = "系统任务", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetJobList([FromBody] SysJobWDto whereDto)
    {
        var result = await _sysJobService.GetJobList(whereDto);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统任务列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    [HttpPost("Get/PageList")]
    [AppLog(Module = "系统任务", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetJobPageList([FromBody] PageWhereDto<SysJobWDto> pageWhere)
    {
        var result = await _sysJobService.GetJobPageList(pageWhere);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 导出系统任务
    /// </summary>
    /// <returns></returns>
    [HttpGet("Export/Data")]
    [AppLog(Module = "系统任务", BusinessType = BusinessTypeEnum.Export, IsSaveRequestData = false)]
    public async Task<ApiResult> ExportJob()
    {
        List<SysJob> result = await _sysJobService.GetListAsync();
        await ExportExcel("系统任务", result, "SysJob");
        return ApiResult.Success($"系统任务导出成功！");
    }

    /// <summary>
    /// 验证任务参数
    /// </summary>
    /// <param name="jobCDto"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    private static bool VerifyJobParams(SysJobCDto jobCDto)
    {
        // 根据任务类型验证任务参数
        if (jobCDto.JobType == JobTypeEnum.Assembly)
            if (jobCDto.AssemblyName.IsNullOrEmpty() || jobCDto.ClassName.IsNullOrEmpty())
                throw new CustomException($"任务类型为【程序集】时，程序集或所在类不能为空！");
        if (jobCDto.JobType == JobTypeEnum.NetworkRequest)
            if (jobCDto.RequestMethod == null || jobCDto.ApiUrl.IsNullOrEmpty())
                throw new CustomException($"任务类型为【网络请求】时，请求方式或执行地址不能为空！");
        if (jobCDto.JobType == JobTypeEnum.SqlStatement)
            if (jobCDto.SqlText.IsNullOrEmpty())
                throw new CustomException($"任务类型为【SQL语句】时，SQL语句不能为空！");

        // 根据触发器类型验证执行参数
        if (jobCDto.TriggerType == TriggerTypeEnum.Interval)
            if (jobCDto.IntervalSecond == null || jobCDto.CycleRunTimes == null || jobCDto.BeginTime == null ||
                jobCDto.EndTime == null)
                throw new CustomException($"触发器类型为【定时任务】时，执行间隔时间、循环执行次数、开始时间、结束时间等不能为空！");
        if (jobCDto.TriggerType == TriggerTypeEnum.Cron)
        {
            if (jobCDto.IntervalSecond.IsEmptyOrNull())
                throw new CustomException($"触发器类型为【时间点或者周期性任务】时，Cron表达式不能为空！");
            else if (!CronExpression.IsValidExpression(jobCDto.Cron!)) throw new CustomException($"Cron表达式不正确！");
        }

        return true;
    }
}