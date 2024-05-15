// 声明一个模块，防止引入文件时报错
declare module '*.json';
declare module '*.png';
declare module '*.jpg';
declare module '*.scss';
declare module '*.ts';
declare module '*.js';

// 声明文件，*.vue 后缀的文件交给 vue 模块来处理
declare module '*.vue' {
  import type { DefineComponent } from 'vue';
  const component: DefineComponent<{}, {}, any>;
  export default component;
}

// 声明文件，定义全局变量
declare interface Window {
  NProgress?: import('nprogress').NProgress;
}

declare interface ViteEnv {
  VITE_USE_MOCK: boolean;
  VITE_PUBLIC_PATH: string;
  VITE_GLOB_APP_TITLE: string;
  VITE_BUILD_COMPRESS: 'gzip' | 'brotli' | 'none';
}

// api统一返回结果
declare interface ApiResult<T = any> {
  /**
   * 业务状态码
   */
  statusCode: number;
  /**
   * 是否成功
   */
  succeeded: boolean;
  /**
   * 错误消息
   */
  errors?: string;
  /**
   * 结果
   */
  data?: T;

  /**
   * 扩展值
   */
  extras?: any;

  /**
   * 时间戳
   */
  timestamp: number;
}
