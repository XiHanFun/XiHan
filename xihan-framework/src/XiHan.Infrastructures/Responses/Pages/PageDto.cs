#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:PageDto
// Guid:ac953e11-5c32-44d5-a450-d377ef3a0453
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-12-04 下午 11:15:06
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Infrastructures.Responses.Pages;

/// <summary>
/// 通用分页实体基类
/// </summary>
public class PageDto
{
    #region 私有字段

    /// <summary>
    /// 默认当前页(防止非安全性传参)
    /// </summary>
    private const int DefaultIndex = 1;

    /// <summary>
    /// 默认每页大小最大值(防止非安全性传参)
    /// </summary>
    private const int DefaultMaxPageSize = 100;

    /// <summary>
    /// 默认每页大小最小值(防止非安全性传参)
    /// </summary>
    private const int DefaultMinPageSize = 1;

    #endregion

    private int _currentIndex = 1;

    /// <summary>
    /// 当前页标
    /// </summary>
    public int CurrentIndex
    {
        get => _currentIndex;
        set
        {
            if (value < DefaultIndex) value = DefaultIndex;
            _currentIndex = value;
        }
    }

    private int _pageSize = 10;

    /// <summary>
    /// 每页大小
    /// </summary>
    public int PageSize
    {
        get => _pageSize;
        set
        {
            if (value > DefaultMaxPageSize)
                value = DefaultMaxPageSize;
            else if (value < DefaultMinPageSize) value = DefaultMinPageSize;
            _pageSize = value;
        }
    }
}