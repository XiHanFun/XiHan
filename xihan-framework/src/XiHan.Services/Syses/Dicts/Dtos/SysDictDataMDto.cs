#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysDictDataMDto
// Guid:6c9bebdf-24c8-4e8d-b584-0b17e7a1935a
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-08-18 下午 06:10:22
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Services.Bases.Dtos;

namespace XiHan.Services.Syses.Dicts.Dtos;

/// <summary>
/// SysDictDataMDto
/// </summary>
public class SysDictDataMDto : SysDictDataCDto, IBaseIdDto
{
    /// <summary>
    /// 主键标识
    /// </summary>
    public long BaseId { get; set; }
}