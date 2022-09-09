# ZhaiFanhuaBlog.Framework更新沿程

---



## 2022-09-09

1. 新增：汉语拼音转换；
2. 新增：常用工具类；
3. [新增：性能分析工具；](https://github.com/ZhaiFanhuaBlog/ZhaiFanhuaBlog.Framework/commit/913d7f8e66a93cce7742f2e5f3d75bc28e33778f)
4. [优化：SqlSugar拓展分页；](https://github.com/ZhaiFanhuaBlog/ZhaiFanhuaBlog.Framework/commit/db3b4c8ba56f5cb6dbea497ab2a3ff8721d2209a)
5. [优化：统一返回状态模型提示；](https://github.com/ZhaiFanhuaBlog/ZhaiFanhuaBlog.Framework/commit/d57b71d619969280a153944109f32fbf586f85a9)
6. 优化：数据验证封装；
7. 优化：分页返回数据封装；
8. [修复：用户注册邮箱长度验证；](https://github.com/ZhaiFanhuaBlog/ZhaiFanhuaBlog.Framework/commit/33daa87c8b4dbaa7040a51fb134d77643f2e5de1)
9. 修复：分页总页数错误的bug；
10. [修复：Swagger的Jwt Bearer 需要认证的接口发请求不带token；](https://github.com/ZhaiFanhuaBlog/ZhaiFanhuaBlog.Framework/commit/c45115a23cb1840f2ef50ee3e3a9fe349e01577b)

## 2022-08-30

1. [优化：所有代码添加注释，对代码警告零容忍；](https://github.com/ZhaiFanhuaBlog/ZhaiFanhuaBlog.Framework/commit/2834cabee69f1fbf2e80c4a16831236c876eac6e)
2. [优化：站点初始化；](https://github.com/ZhaiFanhuaBlog/ZhaiFanhuaBlog.Framework/commit/650d67a5666fc6f5bcac6e466a365f83d98ee76b)

## 2022-08-06

1. [新增：网站配置；](https://github.com/ZhaiFanhuaBlog/ZhaiFanhuaBlog.Framework/commit/96d7294ccdb3580b97975f19ffe2101c2197a3e5)
2. [新增：系统菜单；](https://github.com/ZhaiFanhuaBlog/ZhaiFanhuaBlog.Framework/commit/28809cf2191bfcc5cd69700f45051d62c801723d) 
3. [优化：移除多余的包，减小体积；](https://github.com/ZhaiFanhuaBlog/ZhaiFanhuaBlog.Framework/commit/883d1b749dbc819b11f4fa1b573ec51db5d61c18)
4. [优化：默认开启内存缓存；](https://github.com/ZhaiFanhuaBlog/ZhaiFanhuaBlog.Framework/commit/4981865e7f9590d6e2bb8bc8695c68d02fbcce08)
5. [修复：系统菜单、系统角色服务注入异常；](https://github.com/ZhaiFanhuaBlog/ZhaiFanhuaBlog.Framework/commit/548126e46811d861927cbf7ddba874f76c59f326)

## 2022-07-31

1. 新增：发布设置文件；
2. 新增：预览环境；
3. [新增：日志启用开关；](https://github.com/ZhaiFanhuaBlog/ZhaiFanhuaBlog.Framework/commit/017ba6cb4363b0efdd0c18ce6021156653ae9056)
4. [优化：日志格式优化；](https://github.com/ZhaiFanhuaBlog/ZhaiFanhuaBlog.Framework/commit/7ea3ae3a6aeca8c1f7714d15e354e1f5c4a50252)

## 2022-07-29

1. 新增：博客文章功能；
2. 新增：博客标签功能；
3. 新增：博客文章添加标签功能；
4. 新增：博客文章点赞功能；
5. 新增：博客文章评论功能；
6. 新增：博客文章评论点赞功能；
7. 新增：测试Api；
8. 优化：博客文章分类功能；
9. 优化：账户认证；
10. 优化：AutoMapper映射；
11. 优化：说明文档；
12. 修复：用户账户、权限、角色软删除问题；

## 2022-07-26

1. 新增：Ip转换帮助类；
2. 优化：实体字段说明，方便生成数据字典；
3. 优化：字段为string类型的，数据库可为空则默认不赋值，不为空则默认赋值string.Empty;
4. 修复：IpV6和IpV4无法保存在同一字段的问题；
5. 修复：AutoMapper字段映射问题；

## 2022-07-25

1. 新增：全局系统状态种子数据；
2. 新增：用户权限、用户角色、用户账户种子数据；
3. 优化：系统初始化功能；
4. 优化：命名规范；
5. 修复：本地多网卡Ip错误的bug；
6. 修复：用户权限、用户角色、用户账户非登录操作的严重bug；
7. 修复：修改用户密码存储非加密密码的严重bug；
8. 修复：全局软删除功能；
9. 修复：全局状态功能；

## 2022-07-24

1. 新增：项目Logo；
2. 新增：博客文章分类功能；
3. 优化：本地Ip获取方法改写；
4. 优化：跨域问题；
5. 优化：数据库初始化检测；

## 2022-07-21

1. 新增：初始化种子数据；
2. 优化：用户账户管理；
3. 优化：发布不生成debug文件；

## 2022-07-20

1. 新增：测试用一键删除数据库；
2. 优化：异常过滤器；
3. 优化：权限验证；

## 2022-07-15

1. 优化：为用户账户分配角色；
2. 优化：Model调整；

## 2022-07-01

1. 新增：为用户账户分配角色；
2. 新增：为用户角色分配权限；
3. 新增：用户登录授权；
4. 优化：错误日志信息；
5. 优化：用户角色功能；
6. 优化：角色权限功能；
7. 优化：用户账户功能；

## 2022-06-30

1. 新增：Excel导入导出；

## 2022-06-29

1. 修复：部分缓存问题；

## 2022-06-20

1. 新增：日志框架Serilog、NLog支持；
2. 新增：分布式缓存；
3. 优化：完善权限接口；
4. 修复：缓存失效，取不到值的问题；

## 2022-06-18

1. 优化：参数字段调整；
2. 修复：结果过滤器不能获取到数据的问题；

## 2022-06-17

1. 优化：模型验证多次优化；

## 2022-06-16

1. 新增：模型验证；
2. 优化：删除非异步过滤器，返回结果封装，添加缓存；
3. 修复：跨域获取不到域名数组的问题；

## 2022-06-05

1. `重要变更`：前后端项目分离、更名仓库；由`ZhaiFanhuaBlog`变更为`ZhaiFanhuaBlog.Framework`和前端仓库；本仓库为`ZhaiFanhuaBlog.Framework`；
2. 新增：其他博客平台资源迁移功能（目前有hexo迁移）；
3. 优化：移除配置文件铭感数据，如数据库密码、第三方登录配置、CDN配置；
4. 优化：对象相互映射；

## 2022-05-09

1. 新增：RBAC权限管理；
2. 新增：包装API返回模型，统一规范接口；
3. 新增：Swagger分组；
4. 优化：命名规范；
5. 优化：缓存、部分帮助类；
6. 优化：为空检测；

## 2022-04-11

1. 新增：文件大小格式化工具类；
2. 优化：命名规范，文件规范；

## 2022-02-24

1. 新增：批量删除；
1. 优化：.net6 新命名空间规范；
1. 优化：项目目录调整，删除无用功能接口，部分功能向Service聚焦；
1. 优化：项目整合；
1. 优化：完善用户角色功能；

## 2022-01-27

1. 新增：用户权限功能；
2. 新增：用户角色功能；
3. 新增：用户账户功能；
4. 新增：管理员功能、系统管理；
5. 新增：网站配置、日志、第三方授权、皮肤；
6. 优化：授权完善；
7. 优化：代码命名规范，Service层和Api层功能隔离；
8. 优化：工具类调整，Repository层调整，Server层调整；
9. 优化：命名规范，文件规范；
10. 优化：Id全部采用Guid；
11. 修复：AutoMapper映射错误的bug；
12. 修复：注入Bug；

## 2022-01-17

1. 新增：版权和作者信息；
2. 新增：分页查询功能；
3. 新增：前端界面返回AutoMapper映射的安全数据，如密码等敏感数据；
4. 优化：缓存；

## 2022-01-16

1. 新增：特性支持、缓存支持、日志支持；
1. 新增：日志写入组件log4net，测试日志写入；
1. 新增：AES、SHA、MD5、SHA加密算法；
1. 新增：测试项目；
1. 优化：文件命名统一化；
1. 优化：配置文件，工具类移动；

## 2021-12-26

1. 新增：JWT授权鉴权；
3. 优化：项目配置；

## 2021-10-22

1. 新增：Docker支持；
1. 新增：IOC注入，自动生成数据库；
1. 优化：API路由；
1. 优化：部分字段名称改为系统非保留字段；
1. 优化：Service层；

## 2021-10-17

1. `启动`：项目初始化；
1. `设计`：初步设计数据库；
1. 新增：Model层、Repository层、Service层、WebApi项目；