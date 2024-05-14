#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:PageInfo
// Guid:b3d13169-fb3b-4999-b869-74c73aa426f9
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 6:59:38
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Communication.Https.Entities;

/// <summary>
/// 通用分页信息基类
/// </summary>
public class PageInfo
{
    #region 默认值

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
    private int _pageSize = 20;

    /// <summary>
    /// 构造函数
    /// </summary>
    public PageInfo()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="currentIndex"></param>
    /// <param name="pageSize"></param>
    public PageInfo(int currentIndex, int pageSize)
    {
        CurrentIndex = currentIndex;
        PageSize = pageSize;
    }

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
            else if (value < DefaultMinPageSize)
                value = DefaultMinPageSize;
            _pageSize = value;
        }
    }
}