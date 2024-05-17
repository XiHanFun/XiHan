import type { App } from 'vue';
import {
  createRouter,
  createWebHashHistory,
  createWebHistory,
  createMemoryHistory,
  RouteRecordRaw,
  RouterHistory,
} from 'vue-router';
import { close, start } from '@/utils/nprogress';
import { basicRoutes } from './routes';
import { strict } from 'assert';

// 白名单应该包含基本静态路由
const WHITE_NAME_LIST: string[] = [];

// 创建一个可以被 Vue 应用程序使用的路由实例
export const router = createRouter({
  // 选择路由模式
  history: createWebHashHistory(import.meta.env.VITE_PUBLIC_PATH),
  // 配置路由
  routes: basicRoutes as unknown as RouteRecordRaw[],
  // 严格模式
  strict: true,
});

// 路由前置后卫
router.beforeEach(() => {
  // 开启进度条
  start();
});
// 路由后置后卫
router.afterEach(() => {
  // 关闭进度条
  close();
});

// 重置路由
export const resetRouter = () => {
  router.getRoutes().forEach(route => {
    const { name } = route;
    if (name && !WHITE_NAME_LIST.includes(name as string)) {
      router.hasRoute(name) && router.removeRoute(name);
    }
  });
};
// 配置路由
export const setupRouter = (app: App<Element>) => {
  app.use(router);
};
