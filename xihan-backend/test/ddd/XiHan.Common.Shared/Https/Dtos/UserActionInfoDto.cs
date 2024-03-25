#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:UserActionInfoDto
// Guid:08982b53-efad-41d1-9ea7-7334bb5ed683
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/2/29 1:59:32
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Common.Shared.Enums;

namespace XiHan.Common.Shared.Https.Dtos;

/// <summary>
/// 控制器信息
/// </summary>
public class UserActionInfoDto
{
    /// <summary>
    /// 请求方式
    /// </summary>
    public string RequestMethod { get; set; } = string.Empty;

    /// <summary>
    /// 请求地址
    /// </summary>
    public string RequestUrl { get; set; } = string.Empty;

    /// <summary>
    /// 请求参数
    /// </summary>
    public string RequestParameters { get; set; } = string.Empty;

    /// <summary>
    /// 响应结果
    /// </summary>
    public string ResponseResult { get; set; } = string.Empty;

    /// <summary>
    /// 控制器名称
    /// </summary>
    public string ControllerName { get; set; } = string.Empty;

    /// <summary>
    /// 操作名称
    /// </summary>
    public string ActionName { get; set; } = string.Empty;

    /// <summary>
    /// 方法名称
    /// </summary>
    public string MethodName { get; set; } = string.Empty;

    /// <summary>
    /// 操作模块
    ///</summary>
    public string Module { get; set; } = string.Empty;

    /// <summary>
    /// 业务类型
    /// 0其它 1新增 2修改 3删除 4授权 5导出 6导入 7强退 8生成代码 9清空数据
    /// </summary>
    public BusinessTypeEnum BusinessType { get; set; }
}