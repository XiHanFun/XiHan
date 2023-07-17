#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BaseIdDto
// Guid:14c974cd-f8af-4c85-b5b0-407980acfcb8
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-10 上午 02:13:11
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Services.Bases;

/// <summary>
/// BaseIdDto
/// </summary>
/// <remarks>
/// 数据传输对象命名规范：
/// 新增：后缀以 CDto 结尾，CreateDto
/// 删除：后缀以 DDto 结尾，DeleteDto
/// 修改：后缀以 MDto 结尾，ModifyDto
/// 查询：后缀以 GDto 结尾，GetDto
/// 条件：后缀以 WDto 结尾，WhereDto
/// 结果：后缀以 RDto 结尾，ResultDto
/// </remarks>
public class BaseIdDto
{
    /// <summary>
    /// 主键标识
    /// </summary>
    public long BaseId { get; set; }
}