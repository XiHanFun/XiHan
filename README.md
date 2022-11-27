![LOGO](LOGO.png)

![license-MIT](https://img.shields.io/badge/license-MIT-blue.svg?longCache=true&style=flat-square)[![Codacy Badge](https://app.codacy.com/project/badge/Grade/5045beecc88149cf8c52432086aeb06c)](https://www.codacy.com/gh/ZhaiFanhuaBlog/ZhaiFanhuaBlog.Framework/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=ZhaiFanhuaBlog/ZhaiFanhuaBlog.Framework&amp;utm_campaign=Badge_Grade)

![last commit](https://img.shields.io/github/last-commit/ZhaiFanhuaBlog/ZhaiFanhuaBlog.Framework.svg?style=flat-square)![downloads](https://img.shields.io/github/downloads/ZhaiFanhuaBlog/ZhaiFanhuaBlog.Framework/total?style=flat-square)![release](https://img.shields.io/github/v/release/ZhaiFanhuaBlog/ZhaiFanhuaBlog.Framework?style=flat-square)![commit activity](https://img.shields.io/github/commit-activity/y/ZhaiFanhuaBlog/ZhaiFanhuaBlog.Framework?style=flat-square)![issues](https://img.shields.io/github/issues/ZhaiFanhuaBlog/ZhaiFanhuaBlog.Framework?style=flat-square)![](https://img.shields.io/github/issues-closed-raw/ZhaiFanhuaBlog/ZhaiFanhuaBlog.Framework?style=flat-square)![forks](https://img.shields.io/github/forks/ZhaiFanhuaBlog/ZhaiFanhuaBlog.Framework?style=flat-square)![stars](https://img.shields.io/github/stars/ZhaiFanhuaBlog/ZhaiFanhuaBlog.Framework?style=flat-square)![watchers](https://img.shields.io/github/watchers/ZhaiFanhuaBlog/ZhaiFanhuaBlog.Framework?style=flat-square)

这是基于DotNet7的博客项目，高效、快速、开源、免费。

## 快速开始

请参考部署文档： [https://docs.blog.zhaifanhua.com](https://docs.blog.zhaifanhua.com)

## 所用框架

- DotNet7(CSharp);
- WebApi;

### 框架模块：

- [x] 采用`仓储+服务+接口`的形式封装框架；

- [x] 全面异步 async/await 开发；

- [x] 国产数据库 ORM 组件 SqlSugar ；

- [x] 支持自由切换多种数据库，MySql/SqlServer/Sqlite/Oracle/Postgresql/达梦/人大金仓；

- [x] 项目启动，支持自动生成种子数据；

- [ ] 五种日志记录，审计/异常/请求响应/服务操作/Sql记录等；

- [x] 搭配全套前端项目：

  - 后端：ZhaiFanhuaBlog.Framework；

  - 前端：ZhaiFanhuaBlog.Manage（后台管理）、ZhaiFanhuaBlog.Display（前台展示）；

  - 文档：ZhaiFanhuaBlog.Docs

- [ ] 统一集成 IdentityServer4 认证 ✨;

### 组件模块：

- [x] 提供 Redis 做缓存处理；
- [x] 使用 Swagger 做API文档；
- [x] 使用 MiniProfiler 做接口性能分析 ✨；
- [x] 使用 Automapper 处理对象映射；
- [x] 使用自带高性能依赖注入容器，并提供批量服务注入 ✨；
- [x] 支持 CORS 跨域；
- [x] 封装 JWT 自定义策略授权；
- [x] 使用 Serilog日志框架，高性能日志记录；
- [ ] 使用 SignalR 双工通讯 ✨；
- [ ] 添加 IpRateLimiting 做 API 限流处理;
- [ ] 使用 Quartz 做任务调度;
- [x] 支持 数据库`读写分离`和多库操作 ✨;
- [ ] 新增 Redis 消息队列 ✨;
- [ ] 新增 RabbitMQ 消息队列 ✨;
- [ ] 新增 EventBus 事件总线 ✨;
- [ ] 新增 ES 搜索配置;

### 微服务模块：

- [x] 可配合 Docker 实现容器化；
- [x] 可配合 Nginx 实现负载均衡；
- [ ] 可配合 Ids4 实现认证中心；

## 项目相关

贡献代码：[贡献代码](CONTRIBUTING.md)

更新沿程：[更新沿程](CHANGELOG.md)

启动：

```
dotnet /home/web/zhaifanhua.com/blog/ZhaiFanhuaBlog.WebApi.dll --environment=Production --urls=http://*:9708 --ip=127.0.0.1
```



## Status

![Status](https://repobeats.axiom.co/api/embed/6e6dcd83875e06131527cf7e55007e5f72fd1860.svg)

## Star History

![Star History](https://api.star-history.com/svg?repos=ZhaiFanhuaBlog/ZhaiFanhuaBlog.Framework&type=Date)
