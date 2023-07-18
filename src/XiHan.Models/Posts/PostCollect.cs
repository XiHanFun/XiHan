#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysUserCollect
// Guid:9a936a6b-f0bf-4fe9-9c43-868982a69e01
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 06:02:22
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Entity;

namespace XiHan.Models.Posts;

/// <summary>
/// 文章收藏表
/// </summary>
/// <remarks>记录新增信息</remarks>
[SugarTable(TableName = "Post_Collect")]
public class PostCollect : BaseCreateEntity
{
    /// <summary>
    /// 分类编码
    /// </summary>
    public long TypeId { get; set; }

    /// <summary>
    /// 收藏文章
    /// </summary>
    public long ArticleId { get; set; }
}