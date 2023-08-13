#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysAnnouncement
// Guid:0eeca4e2-11de-44f3-9c56-9205e87a9890
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 06:42:44
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases;
using XiHan.Models.Bases.Attributes;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统公告表
/// </summary>
/// <remarks>记录新增，修改，删除信息</remarks>
[SystemTable]
[SugarTable(TableName = "Sys_Announcement")]
public class SysAnnouncement : BaseDeleteEntity
{
    /// <summary>
    /// 公告标题
    /// </summary>
    [SugarColumn(Length = 256)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 公告内容
    /// </summary>
    [SugarColumn(ColumnDataType = StaticConfig.CodeFirst_BigString)]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 公告链接
    /// </summary>
    [SugarColumn(Length = 256, IsNullable = true)]
    public string? Url { get; set; }

    /// <summary>
    /// 公告结束时间
    /// </summary>
    public DateTime? ShowEndTime { get; set; }
}