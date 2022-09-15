// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:PageDataDto
// Guid:729e1048-3950-4091-bd0d-ec69af98c4f4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-16 上午 01:38:41
// ----------------------------------------------------------------

namespace ZhaiFanhuaBlog.ViewModels.Bases.Pages;

/// <summary>
/// 通用分页数据实体
/// </summary>
public class PageDataDto<Entity> where Entity : class
{
    /// <summary>
    /// 分页数据
    /// </summary>
    public PageDto? Page { get; set; }

    /// <summary>
    /// 数据集合
    /// </summary>
    public List<Entity>? Datas { get; set; }
}