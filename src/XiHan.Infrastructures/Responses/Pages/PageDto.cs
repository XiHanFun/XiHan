#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:PageDto
// Guid:ac953e11-5c32-44d5-a450-d377ef3a0453
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-12-04 下午 11:15:06
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Infrastructures.Responses.Pages;

/// <summary>
/// 通用分页实体基类
/// </summary>
public class PageDto
{
    /// <summary>
    /// 默认当前页
    /// </summary>
    private const int DefaultIndex = 1;

    /// <summary>
    /// 每页大小最大值
    /// </summary>
    private const int MaxPageSize = 100;

    /// <summary>
    /// 每页大小最大值
    /// </summary>
    private const int MinPageSize = 1;

    private int _currentIndex = 1;

    /// <summary>
    /// 当前页标
    /// </summary>
    public int CurrentIndex
    {
        get => _currentIndex;
        set
        {
            if (value < DefaultIndex)
            {
                value = DefaultIndex;
            }
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
            if (value > MaxPageSize)
            {
                value = MaxPageSize;
            }
            else if (value < MinPageSize)
            {
                value = MinPageSize;
            }
            _pageSize = value;
        }
    }
}