#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysUserNotice
// Guid:ee3e1bfb-ce5a-4642-84c8-4bef82afb198
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 06:13:44
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHanCore.Models.Bases.Attributes;
using XiHanCore.Models.Bases.Entities;

namespace XiHanCore.Models.Syses;

/// <summary>
/// 系统用户通知表
/// </summary>
/// <remarks>记录新增，修改，删除信息</remarks>
[SugarTable, SystemTable]
public class SysUserNotice : BaseDeleteEntity
{
    /// <summary>
    /// 通知标题
    /// </summary>
    [SugarColumn(Length = 256)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 通知内容
    /// </summary>
    [SugarColumn(ColumnDataType = StaticConfig.CodeFirst_BigString)]
    public string Content { get; set; } = string.Empty;
}