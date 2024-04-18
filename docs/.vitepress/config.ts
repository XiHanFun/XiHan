import { defineConfig } from "vitepress";
import type { DefaultTheme } from "vitepress/types";
import { version } from "../package.json";

const title: string = "曦寒官方文档";
const description: string = "新型全场景应用软件";
const keywords: string =
  "曦寒,曦寒懿,官方文档,全场景,应用软件,开源,摘繁华,XiHanFun,xihan,docs";

const Nav: DefaultTheme.NavItem[] = [
  { text: "项目简介", link: "guide", activeMatch: "guide" },
  {
    text: "探索未知",
    items: [
      {
        text: "官方网站",
        link: "https://www.xihan.fun",
      },
      {
        text: "官方文档",
        link: "https://docs.xihan.fun",
      },
    ],
  },
  {
    text: "在线体验",
    items: [
      {
        text: "用例",
        link: "https://demo.xihan.fun",
      },
      {
        text: "后端",
        link: "https://backend.xihan.fun",
      },
      {
        text: "前端",
        link: "https://frontend.xihan.fun",
      },
    ],
  },
  {
    text: "代码仓库",
    items: [
      {
        text: "Github(国际)",
        items: [
          {
            text: "组织",
            link: "https://github.com/XiHanFun",
          },
          {
            text: "项目",
            link: "https://github.com/XiHanFun/XiHan",
          },
        ],
      },
      {
        text: "Gitee(国内)",
        items: [
          {
            text: "组织",
            link: "https://gitee.com/XiHanFun",
          },
          {
            text: "项目",
            link: "https://gitee.com/XiHanFun/XiHan",
          },
        ],
      },
    ],
  },
  {
    text: `v${version}`,
    items: [
      {
        text: "贡献代码",
        link: "docs/develop/contribution-code",
      },
      {
        text: "变更日志",
        link: "docs/develop/change-log",
      },
    ],
  },
];

export default defineConfig({
  srcDir: "src",
  ignoreDeadLinks: true,
  lang: "zh-CN",
  title: title,
  description: description,
  head: [
    ["meta", { name: "author", content: "ZhaiFanhua" }],
    [
      "meta",
      {
        name: "keywords",
        content: keywords,
      },
    ],
    ["link", { rel: "icon", href: "/favicon.ico" }],
  ],
  lastUpdated: true,
  cleanUrls: true,
  themeConfig: {
    logo: "/logo.png",
    socialLinks: [{ icon: "github", link: "https://github.com/XiHanFun" }],
    search: {
      provider: "local",
    },
    nav: Nav,
    editLink: {
      text: "在 GitHub 上编辑此页",
      pattern: "https://github.com/XiHanFun/XiHan-Docs/tree/main/src/:path",
    },
    footer: {
      message:
        "Released under the <a href='http://license.coscl.org.cn/MulanPSL2' target='_blank'>MulanPSL2</a> License.",
      copyright:
        "Copyright ©2023 <a href='https://www.zhaifanhua.com' target='_blank'>ZhaiFanhua</a> All Rights Reserved.",
    },
  },
});
