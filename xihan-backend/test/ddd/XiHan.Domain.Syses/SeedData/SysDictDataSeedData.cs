#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysDictDataSeedData
// Guid:69236020-b8bd-42d9-8a3a-3d0952ac7698
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/12/12 22:17:03
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Common.Shared.Attributes;
using XiHan.Common.Shared.Filters;

namespace XiHan.Domain.Syses.SeedData;

/// <summary>
/// 系统字典项表种子数据
/// </summary>
public class SysDictDataSeedData : ISeedDataFilter<SysDictData>
{
    /// <summary>
    /// 种子数据
    /// </summary>
    /// <returns></returns>
    [IgnoreUpdateSeedData]
    public IEnumerable<SysDictData> HasData()
    {
        List<SysDictData> result = [];

        // 用户状态
        List<SysDictData> userStatus =
        [
            new SysDictData
            {
                TypeCode = "SysUserStatus",
                Label = "启用",
                Value = "Enable",
                SortOrder = 1,
                Description = "用户状态启用"
            },
            new SysDictData
            {
                TypeCode = "SysUserStatus",
                Label = "停用",
                Value = "Disable",
                SortOrder = 2,
                IsDefault = true,
                Description = "用户状态停用"
            },
        ];

        result.AddRange(userStatus);

        return result;
    }
}