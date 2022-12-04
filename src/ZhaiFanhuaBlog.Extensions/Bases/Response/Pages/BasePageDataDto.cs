#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BasePageDataDto
// Guid:42dc207c-570e-4128-bcf1-cea65179e64b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-12-04 下午 11:16:04
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace ZhaiFanhuaBlog.Extensions.Bases.Response.Pages;

/// <summary>
/// 通用分页数据实体基类
/// </summary>
public class BasePageDataDto<Entity> where Entity : class
{
    /// <summary>
    /// 分页数据
    /// </summary>
    public BasePageInfoDto? Page { get; set; }

    /// <summary>
    /// 数据集合
    /// </summary>
    public List<Entity>? Datas { get; set; }
}