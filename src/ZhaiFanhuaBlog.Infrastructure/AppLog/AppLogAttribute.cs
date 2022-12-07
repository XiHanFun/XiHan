#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:AppLogAttribute
// Guid:072cbfb1-e0d3-43b9-8a49-20e42986b30a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-12-07 下午 08:15:49
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Serilog;

namespace ZhaiFanhuaBlog.Infrastructure.AppLog;

/// <summary>
/// AppLogAttribute
/// </summary>
[AttributeUsage(AttributeTargets.All, Inherited = false)]
public class AppLogAttribute : Attribute
{
    public AppLogAttribute()
    {
    }
}