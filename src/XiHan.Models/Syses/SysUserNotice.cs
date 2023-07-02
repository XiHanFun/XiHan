#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SysUserNotice
// long:ee3e1bfb-ce5a-4642-84c8-4bef82afb198
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:13:44
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Entity;

namespace XiHan.Models.Syses;

/// <summary>
/// 用户通知表
/// </summary>
/// <remarks>记录新增，修改，删除信息</remarks>
[SugarTable(TableName = "Sys_User_Notice")]
public class SysUserNotice : BaseDeleteEntity
{
    /// <summary>
    /// 通知标题
    /// </summary>
    [SugarColumn(Length = 200)]
    public string NoticeTitle { get; set; } = string.Empty;

    /// <summary>
    /// 通知内容
    /// </summary>
    [SugarColumn(ColumnDataType = "text")]
    public string NoticeContent { get; set; } = string.Empty;
}