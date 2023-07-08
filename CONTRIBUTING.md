# 如何为本项目贡献代码

## 一、准备工作

- 安装 Git；

- 安装 Sourcetree，这个工具是 git 的一种图形化界面；

- 注意安装 git 的时候记得勾选将 git 所在目录添加到系统环境变量；

- 安装 Visual Studio 2022；

- 安装  Visual Studio 2022 的 CodeMaid 扩展插件，这个插件可以自动格式化代码；

- `若有新功能开发`，请添加文件头，如下为我的示例：

```
// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogArticleTagService
// Guid:f9b3a059-9beb-4d04-8329-48b390fb1007
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:19:29
// ----------------------------------------------------------------
```

  

## 二、贡献代码

### 1. 新建自己的分支（Fork）

将本项目仓库 fork 到自己的 git 仓库中。

### 2. 克隆（Clone）

将已经 fork 的仓库 clone 到自己的本地 PC。

### 3. 创建本地分支

如果想要在本项目上做自己的开发，最好创建属于自己的项目分支，如果是直接贡献代码，那么可以直接在 dev 分支上进行操作。

### 4. 开发

1. 发现了一个小 Bug 并进行修改。
2. 在打开的 Issues 中选择功能并进行开发。

### 5. 提交（Commit）

向本地仓库提交 Bug。

规范：

| emoji                                        | emoji 代码                    | commit 说明                  |
| -------------------------------------------- | ----------------------------- | ---------------------------- |
| :art: (调色板)                               | `:art:`                       | 改进代码结构/代码格式        |
| :zap: (闪电) :racehorse: (赛马)              | `:zap:` `:racehorse:`         | 提升性能                     |
| :fire: (火焰)                                | `:fire:`                      | 移除代码或文件               |
| :bug: (bug)                                  | `:bug:`                       | 修复 bug                     |
| :ambulance: (急救车)                         | `:ambulance:`                 | 重要补丁                     |
| :sparkles: (火花)                            | `:sparkles:`                  | 引入新功能                   |
| :memo: (备忘录)                              | `:memo:`                      | 撰写文档                     |
| :rocket: (火箭)                              | `:rocket:`                    | 部署功能                     |
| :lipstick: (口红)                            | `:lipstick:`                  | 更新 UI 和样式文件           |
| :tada: (庆祝)                                | `:tada:`                      | 初次提交                     |
| :white_check_mark: (白色复选框)              | `:white_check_mark:`          | 更新测试                     |
| :lock: (锁)                                  | `:lock:`                      | 修复安全问题                 |
| :apple: (苹果)                               | `:apple:`                     | 修复 macOS 下的问题          |
| :penguin: (企鹅)                             | `:penguin:`                   | 修复 Linux 下的问题          |
| :checkered_flag: (旗帜)                      | `:checked_flag:`              | 修复 Windows 下的问题        |
| :robot:（机器人）                            | `:robot:`                     | 修复 Android 下的问题        |
| :green_apple: (绿苹果)                       | `:green_apple:`               | 修复 iOS 下的问题            |
| :bookmark: (书签)                            | `:bookmark:`                  | 发行/版本标签                |
| :rotating_light: (警车灯)                    | `:rotating_light:`            | 移除 linter 警告             |
| :construction: (施工)                        | `:construction:`              | 工作进行中                   |
| :construction_worker: (工人)                 | `:construction_worker:`       | 添加 CI 构建系统             |
| :green_heart: (绿心)                         | `:green_heart:`               | 修复 CI 构建问题             |
| :arrow_up: (上升箭头)                        | `:arrow_up:`                  | 升级依赖                     |
| :arrow_down: (下降箭头)                      | `:arrow_down:`                | 降级依赖                     |
| :pushpin: (图钉)                             | `:pushpin:`                   | 将依赖项固定到特定版本       |
| :chart_with_upwards_trend: (上升趋势图)      | `:chart_with_upwards_trend:`  | 添加分析或跟踪代码           |
| :recycle: （回收）                           | `:recycle:`                   | 重构代码                     |
| :whale: (鲸鱼)                               | `:whale:`                     | Docker 相关工作              |
| :globe_with_meridians: (带子午线的地球仪)    | `:globe_with_meridians:`      | 国际化与本地化               |
| :heavy_plus_sign: (加号)                     | `:heavy_plus_sign:`           | 增加一个依赖                 |
| :heavy_minus_sign: (减号)                    | `:heavy_minus_sign:`          | 减少一个依赖                 |
| :wrench: (扳手)                              | `:wrench:`                    | 修改配置文件                 |
| :hammer: (锤子)                              | `:hammer:`                    | 重大重构                     |
| :pencil2: (铅笔)                             | `:pencil2:`                   | 修复 typo                    |
| :hankey: (粑粑...)                           | `:hankey:`                    | 写了辣鸡代码需要优化         |
| :rewind: (倒带)                              | `:rewind:`                    | 恢复更改                     |
| :twisted_rightwards_arrows: (交叉向右的箭头) | `:twisted_rightwards_arrows:` | 合并分支                     |
| :package: (包裹)                             | `:package:`                   | 更新编译的文件或包           |
| :alien: (外星人)                             | `:alien:`                     | 由于外部API更改而更新代码    |
| :truck: (货车)                               | `:truck:`                     | 移动或者重命名文件           |
| :page_facing_up: (正面朝上的页面)            | `:page_facing_up:`            | 增加或更新许可证书           |
| :boom: (爆炸)                                | `:boom:`                      | 引入突破性的变化             |
| :bento: (铅笔)                               | `:bento:`                     | 增加或更新资源               |
| :ok_hand: (OK手势)                           | `:ok_hand:`                   | 由于代码审查更改而更新代码   |
| :wheelchair: (轮椅)                          | `:wheelchair:`                | 改善无障碍交互               |
| :bulb: (灯泡)                                | `:bulb:`                      | 给代码添加注释               |
| :beers: (啤酒)                               | `:beers:`                     | 醉醺醺地写代码...            |
| :speech_balloon: (消息气泡)                  | `:speech_balloon:`            | 更新文本文档                 |
| :card_file_box: (卡片文件盒)                 | `:card_file_box:`             | 执行与数据库相关的更改       |
| :loud_sound: (音量大)                        | `:loud_sound:`                | 增加日志                     |
| :mute: (静音)                                | `:mute:`                      | 移除日志                     |
| :busts_in_silhouette: (轮廓中的半身像)       | `:busts_in_silhouette:`       | 增加贡献者                   |
| :children_crossing: (孩童通行)               | `:children_crossing:`         | 优化用户体验、可用性         |
| :building_construction: (建筑建造)           | `:building_construction:`     | 结构变动                     |
| :iphone: (iPhone)                            | `:iphone:`                    | 做响应式设计                 |
| :clown_face: (小丑脸)                        | `:clown_face:`                | 嘲弄事物（直译，这个没明白） |
| :egg: (鸡蛋)                                 | `:egg:`                       | 增加彩蛋                     |
| :see_no_evil: (看不见邪恶)                   | `:see_no_evil:`               | 增加或更改gitignore          |
| :camera_flash: (照相机闪光灯)                | `:camera_flash:`              | 增加或更新截图               |
| :alembic: (蒸馏器)                           | `:alembic:`                   | 尝试新东西                   |
| :mag: (放大镜)                               | `:mag:`                       | SEO优化                      |
| :wheel_of_dharma: (船的方向盘)               | `:wheel_of_dharma:`           | 关于Kubernetes的工作         |
| :label: (标签)                               | `:label:`                     | 增加类型（FLow、Typescript） |

### 6. 保持本地仓库最新

在准备发起 Pull Request 之前，需要同步原仓库最新的代码，记得检查目前的项目是否是最新的版本。

### 7. 推送到远程仓库（Ppush）

push 到开发者自己的远程仓库中。

### 8. 发起并完成合并请求（Pull Request）

在 git 仓库中选择自己修改了的分支，点击 create pull request 按钮发起 pull request。



## 三、提交代码的一些约定

发起请求成功后，本项目维护人就可以看到你提交的代码。pull request如果被同意，你的代码就会被合并到仓库中。这样一次pull request就成功了。

至此，我们就完成了一次代码贡献的过程。
