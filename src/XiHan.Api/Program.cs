#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:Program
// Guid:fccfeb28-624c-41cb-9c5c-0b0652648a6b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-17 下午 04:01:21
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Serilog;
using System.Reflection;
using XiHan.Api;
using XiHan.Extensions.Setups;

var builder = WebApplication.CreateBuilder(args);

// 日志
var log = builder.Logging;
log.AddLogSetup();

try
{
    // 配置
    var config = builder.Configuration;
    config.AddConfigSetup();

    // 配置主机
    var host = builder.WebHost;
    host.AddWebHostSetup();

    // 配置服务
    var services = builder.Services;
    services.AddServiceSetup();

    // 配置中间件
    var app = builder.Build();
    app.UseApplicationSetup(app.Environment, () => Assembly.GetExecutingAssembly().GetManifestResourceStream("XiHan.Api.index.html")!);

    // 启动信息打印
    ConsoleInfo.SayHello();

    await app.RunAsync();

    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.Information("Application has closed");
    Log.CloseAndFlush();
}