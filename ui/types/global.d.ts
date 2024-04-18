declare interface ViteEnv {
  VITE_USE_MOCK: boolean;
  VITE_PUBLIC_PATH: string;
  VITE_GLOB_APP_TITLE: string;
  VITE_BUILD_COMPRESS: 'gzip' | 'brotli' | 'none';
}
