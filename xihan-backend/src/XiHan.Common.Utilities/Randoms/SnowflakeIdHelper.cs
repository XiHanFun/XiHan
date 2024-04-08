#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SnowflakeIdHelper
// Guid:d74ec167-2636-4ede-9b4f-0153be246e81
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/30 3:31:53
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Yitter.IdGenerator;

namespace XiHan.Common.Utilities.Randoms;

/// <summary>
/// 雪花 Id 帮助类
/// </summary>
public static class SnowflakeIdHelper
{
    /// <summary>
    /// 获取雪花 Id
    /// </summary>
    /// <returns></returns>
    public static long GetNextId()
    {
        return YitIdHelper.NextId();
    }
}