---
title: 前端开发环境
index: false
icon: discover
category:
  - 前端开发环境
---

## 前端开发环境

### 安装开发工具

- 安装 Visual Studio Code，下载[传送门](https://code.visualstudio.com/docs/?dv=win64)；
- 安装 Visual Studio Code 的 Prettier - Code formatter 扩展插件，下载[传送门](https://marketplace.visualstudio.com/items?itemName=esbenp.prettier-vscode)，这个插件可以自动格式化代码；

### 安装 Git

下载 Git 并安装，下载[传送门](https://git-scm.com/downloads)；

### 项目 clone

```shell
git clone https://github.com/XiHanFun/XiHan.Display.git
```

### 安装 Node.js

下载 Node.js 并安装，下载[传送门](https://nodejs.org/en/download)；

### 解除安全策略

![image-20230913203617130](.\assets\image-20230913203617130.png)

通过管理员权限运行 power shell，然后输入命令：

```shell
set-ExecutionPolicy RemoteSigned
```

![image-20230913203743762](.\assets\image-20230913203743762.png)

### 安装 pnpm

> 由于本项目前端包管理器为 pnpm，遂须全局安装 pnpm。

```shell
// 全局安装
npm install pnpm -g
// 查看源
pnpm config get registry
// 切换淘宝源
pnpm config set registry https://registry.npmmirror.com/
```

![image-20230913205134735](.\assets\image-20230913205134735.png)

安装 npm-check-updates

> 用于一键升级依赖。

```shell
npm install npm-check-updates -g
```

![image-20230913210209493](.\assets\image-20230913210209493.png)

### 初始化项目

用 VS Code 打开本项目。

在 npm 脚本处运行本项目的【bootstrap】安装脚本：

![image-20230913205519098](.\assets\image-20230913205519098.png)

在 npm 脚本处运行本项目的【dev】运行脚本：

![image-20230913205652479](.\assets\image-20230913205652479.png)

在 npm 脚本处运行本项目的【update-pkg】依赖升级脚本（后续开发若需）：

![image-20230913210535578](.\assets\image-20230913210535578.png)

至此，前端开发环境搭建完成。
