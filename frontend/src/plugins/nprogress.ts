import NProgress from 'nprogress';

/**
 * 配置并初始化 NProgress 库。
 * 该函数无参数也无返回值。
 * 主要作用是设置 NProgress 的动画速度，并将 NProgress 实例绑定到 window 对象上，以便在全局范围内使用。
 */
export function setupNProgress() {
  // 配置 NProgress 动画的速度为 500 毫秒
  NProgress.configure({ speed: 500 });
  // 将 NProgress 实例绑定到 window 上，方便全局访问
  window.NProgress = NProgress;
}
