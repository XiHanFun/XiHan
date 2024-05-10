#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ServiceExposingActionList
// Guid:fafa2975-6e3e-42d6-95f2-85a14991c24b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/24 21:51:44
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Framework.Core.DependencyInjection;

/// <summary>
/// 服务暴露时操作列表
/// </summary>
public class ServiceExposingActionList : List<Action<IOnServiceExposingContext>>
{
}