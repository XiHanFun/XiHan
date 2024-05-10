#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ISoftDelete
// Guid:e581b8a9-6fcc-440c-b8e4-fdf866466ad4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/27 0:05:44
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Framework.Core.Application;

/// <summary>
/// 软删除接口
/// </summary>
public interface ISoftDelete
{
    /// <summary>
    /// 实体是否已删除
    /// </summary>
    bool IsDeleted { get; }
}