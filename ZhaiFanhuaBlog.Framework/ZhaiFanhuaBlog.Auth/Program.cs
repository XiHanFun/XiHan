// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:Program
// Guid:aff8ee16-2e94-41c5-bf52-ae09fbae16ad
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-17 下午 04:01:21
// ----------------------------------------------------------------

using Microsoft.AspNetCore.HttpOverrides;
using System.Reflection;
using ZhaiFanhuaBlog.Core.AppSettings;
using ZhaiFanhuaBlog.Extensions.Middlewares;
using ZhaiFanhuaBlog.Setups;
using ZhaiFanhuaBlog.Utils.Console;

var builder = WebApplication.CreateBuilder(args);

var log = builder.Logging;
ConsoleHelper.WriteLineWarning("Log Start……");
log.AddLogSetup();
ConsoleHelper.WriteLineSuccess("Log Started Successfully！");

var config = builder.Configuration;
ConsoleHelper.WriteLineWarning("Configuration Start……");
AppSettings appSettings = new(config);
ConsoleHelper.WriteLineSuccess("Configuration Started Successfully！");

var services = builder.Services;
ConsoleHelper.WriteLineWarning("Services Start……");
// Cache
services.AddCacheSetup();
// Http请求
services.AddHttpClient();
// Swagger
services.AddSwaggerSetup();
// 性能分析
services.AddMiniProfilerSetup();
// SqlSugar
services.AddSqlSugarSetup();
// IOC
services.AddIocSetup();
// AutoMapper
services.AddAutoMapperSetup();
// Route
services.AddRouteSetup();
// Cors
services.AddCorsSetup();
// Controllers
services.AddControllersSetup();
ConsoleHelper.WriteLineSuccess("Services Started Successfully！");

var app = builder.Build();
ConsoleHelper.WriteLineWarning("ZhaiFanhuaBlog Application Start……");
// 环境变量，开发环境
if (app.Environment.IsDevelopment())
{
    // 生成异常页面
    app.UseDeveloperExceptionPage();
}
else
{
    // 使用HSTS的中间件，该中间件添加了严格传输安全头
    app.UseHsts();
}
// Nginx 反向代理获取真实IP
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
// 强制https跳转
app.UseHttpsRedirection();
// MiniProfiler
app.UseMiniProfilerMiddleware();
// Swagger
app.UseSwaggerMiddleware(() => Assembly.GetExecutingAssembly().GetManifestResourceStream("ZhaiFanhuaBlog.Auth.index.html")!);
// 使用静态文件
app.UseStaticFiles();
// Serilog请求日志中间件---必须在 UseStaticFiles 和 UseRouting 之间
//app.UseSerilog();
// 路由
app.UseRouting();
// 跨域
app.UseCorsMiddleware();
// 身份验证
app.UseAuthentication();
// 认证授权
app.UseAuthorization();

// 路由映射
app.UseEndpoints(options =>
{
    // 不对约定路由做任何假设，也就是不使用约定路由，依赖用户的特性路由， 一般用在WebAPI项目中
    options.MapControllers();
});
ConsoleHelper.WriteLineSuccess("ZhaiFanhuaBlog Application Started Successfully！");

app.Run();