#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BaseEntity
// Guid:84d15648-b4c6-40a5-8195-aae92765eb04
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 04:12:12
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Models.Bases.Entities;

/// <summary>
/// 实体基类，含主键，新增，修改，删除，审核，状态
/// </summary>
public abstract class BaseEntity : BaseStateEntity;