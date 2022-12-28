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
using ZhaiFanhuaBlog.Api;
using ZhaiFanhuaBlog.Extensions.Setups;

var builder = WebApplication.CreateBuilder(args);

var log = builder.Logging;
log.AddLogSetup();

try
{
    var config = builder.Configuration;
    config.AddConfigSetup();

    var host = builder.WebHost;
    host.AddWebHostSetup();

    var services = builder.Services;
    services.AddServiceSetup();

    var app = builder.Build();
    app.UseApplicationSetup(app.Environment, () => Assembly.GetExecutingAssembly().GetManifestResourceStream("ZhaiFanhuaBlog.Api.index.html")!);

    // 启动信息打印
    ConsoleInfo.Print();
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