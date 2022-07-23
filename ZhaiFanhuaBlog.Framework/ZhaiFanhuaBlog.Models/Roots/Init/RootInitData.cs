// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RootInitData
// Guid:93d1dcc9-a012-4025-92ee-217fb8713fa0
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2022-07-23 上午 10:49:27
// ----------------------------------------------------------------

namespace ZhaiFanhuaBlog.Models.Roots.Init;

/// <summary>
/// RootInitData
/// </summary>
public class RootInitData
{
    public static List<RootState> RootStateList = new()
    {
        // All 总状态
        new RootState
        {
            TypeKey = "All",
            TypeName = "总状态",
            StateKey = -1,
            StateName = "异常",
        },
        new RootState
        {
            TypeKey = "All",
            TypeName = "总状态",
            StateKey = 0,
            StateName = "删除",
        },
        new RootState
        {
            TypeKey = "All",
            TypeName = "总状态",
            StateKey = 1,
            StateName = "正常",
        },
        new RootState
        {
            TypeKey = "All",
            TypeName = "总状态",
            StateKey = 2,
            StateName = "审核",
        }
    };
}