import NProgress from 'nprogress';
import 'nprogress/nprogress.css';

/**
 * 配置并初始化 NProgress 库。
 * 该函数无参数也无返回值。
 * 主要作用是设置 NProgress 的动画速度，并将 NProgress 实例绑定到 window 对象上，以便在全局范围内使用。
 */
export function setupNProgress() {
  NProgress.configure({
    // 动画方式
    easing: 'ease',
    // 递增进度条的速度
    speed: 1000,
    // 是否显示加载ico
    showSpinner: false,
    // 自动递增间隔
    trickleSpeed: 200,
    // 更改启动时使用的最小百分比
    minimum: 0.3,
    //指定进度条的父容器
    parent: 'body',
  });
  // 将 NProgress 实例绑定到 window 上，方便全局访问
  window.NProgress = NProgress;
}

/**
 * 开启进度条
 */
export const start = () => {
  NProgress.start();
};

/**
 * 关闭进度条
 */
export const close = () => {
  NProgress.done();
};
