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

    public RootStateService(IRootStateRepository iRootStateRepository)
    {
        _IRootStateRepository = iRootStateRepository;
        base._IBaseRepository = iRootStateRepository;
    }

    /// <summary>
    /// 初始化状态
    /// </summary>
    /// <returns></returns>
    public async Task<bool> InitRootStatesAsync()
    {
        return await _IRootStateRepository.CreateBatchAsync(RootStates);
    }

    public List<RootState> RootStates = new()
    {
        // All 总状态
        new RootState{
            TypeKey = "All",
            TypeName = "总状态",
            StateKey = -1,
            StateName = "异常",
        },
        new RootState{
            TypeKey = "All",
            TypeName = "总状态",
            StateKey = 0,
            StateName = "删除",
        },
        new RootState{
            TypeKey = "All",
            TypeName = "总状态",
            StateKey = 1,
            StateName = "正常",
        },
        new RootState{
            TypeKey = "All",
            TypeName = "总状态",
            StateKey = 2,
            StateName = "审核",
        }
    };
}