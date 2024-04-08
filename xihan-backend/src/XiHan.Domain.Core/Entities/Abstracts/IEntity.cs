#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IEntity
// Guid:88af4437-cd33-4356-b4f5-a95142c0968c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/26 7:02:18
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Domain.Core.Entities.Abstracts;

/// <summary>
/// 通用实体接口，是所有实体的基接口
/// </summary>
public interface IEntity<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// 实体标识
    /// </summary>
    TKey BaseId { get; }

    /// <summary>
    /// 判断主键是否为空，常用做判定操作是 新增 还是 编辑
    /// </summary>
    /// <returns></returns>
    bool KeyIsNull();

    /// <summary>
    /// 生成默认的主键值
    /// </summary>
    void GenerateDefaultKeyVal();
}