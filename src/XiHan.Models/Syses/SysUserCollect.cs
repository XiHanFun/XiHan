#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SysUserCollect
// long:9a936a6b-f0bf-4fe9-9c43-868982a69e01
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:02:22
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Entity;

namespace XiHan.Models.Syses;

/// <summary>
/// 用户收藏表
/// </summary>
/// <remarks>记录创建信息</remarks>
[SugarTable(TableName = "Sys_User_Collect")]
public class SysUserCollect : BaseCreateEntity
{
    /// <summary>
    /// 收藏分类
    /// </summary>
    public long CategoryId { get; set; }

    /// <summary>
    /// 收藏文章
    /// </summary>
    public long ArticleId { get; set; }
}