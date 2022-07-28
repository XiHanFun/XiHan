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

### 6. 保持本地仓库最新

在准备发起 Pull Request 之前，需要同步原仓库最新的代码，记得检查目前的项目是否是最新的版本。

### 7. 推送到远程仓库（Ppush）

push 到开发者自己的远程仓库中。

### 8. 发起并完成合并请求（Pull Request）

在 git 仓库中选择自己修改了的分支，点击 create pull request 按钮发起 pull request。



## 三、提交代码的一些约定

发起请求成功后，本项目维护人就可以看到你提交的代码。pull request如果被同意，你的代码就会被合并到仓库中。这样一次pull request就成功了。

至此，我们就完成了一次代码贡献的过程。
