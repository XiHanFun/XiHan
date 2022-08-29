// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RootStateService
// Guid:ff93f0ef-c399-4aa9-ab15-bec004e844eb
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 05:38:23
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Roots;
using ZhaiFanhuaBlog.IServices.Roots;
using ZhaiFanhuaBlog.Models.Roots;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Roots;

/// <summary>
/// RootStateService
/// </summary>
public class RootStateService : BaseService<RootState>, IRootStateService
{
    private readonly IRootStateRepository _IRootStateRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iRootStateRepository"></param>
    public RootStateService(IRootStateRepository iRootStateRepository)
    {
        _IRootStateRepository = iRootStateRepository;
        base._IBaseRepository = iRootStateRepository;
    }

    /// <summary>
    /// 初始化系统状态
    /// </summary>
    /// <param name="rootStates"></param>
    /// <returns></returns>
    public async Task<bool> InitRootStateAsync(List<RootState> rootStates)
    {
        return await _IRootStateRepository.CreateBatchAsync(rootStates);
    }
}