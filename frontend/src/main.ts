import { createApp } from 'vue';
import { setupStore } from './store';
import { setupI18n } from './locales';
import { router, setupRouter } from './router';
import { setupNaiveDiscreteApi, setupNaive, setupDirectives } from '@/plugins';
import App from './App.vue';

async function setup() {
  const app = createApp(App);

  // 挂载状态管理
  setupStore(app);

  // 挂载国际化
  setupI18n(app);

  // 注册全局常用的 naive-ui 组件
  setupNaive(app);

  // 挂载 naive-ui 脱离上下文的 Api
  setupNaiveDiscreteApi();

  // 注册全局自定义指令，如：v-permission权限指令
  setupDirectives(app);

  // 挂载路由
  setupRouter(app);

  await router.isReady();

  app.mount('#app', true);
}
await setup();
