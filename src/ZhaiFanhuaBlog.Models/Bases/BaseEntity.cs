#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BaseEntity
// Guid:84d15648-b4c6-40a5-8195-aae92765eb04
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 04:12:12
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Models.Bases.Entity;

namespace ZhaiFanhuaBlog.Models.Bases;

/// <summary>
/// 基类
/// </summary>
public abstract class BaseEntity : BaseStateEntity<Guid>
{
}