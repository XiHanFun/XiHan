import type {
  VNode,
  VNodeChild,
  DefineComponent,
  ComponentPublicInstance,
  FunctionalComponent,
  PropType as VuePropType,
} from 'vue';

// 声明一个模块，防止引入文件时报错
declare module '*.json';
declare module '*.png';
declare module '*.jpg';
declare module '*.scss';
declare module '*.ts';
declare module '*.js';
// 声明文件，*.vue 后缀的文件交给 vue 模块来处理
declare module '*.vue' {
  const component: DefineComponent<{}, {}, any>;
  export default component;
}

/**
 * 定义一个泛型类型 PropType，用于指定 Vue 组件的属性类型。
 * @template T 属性的类型。
 */
declare type PropType<T> = VuePropType<T>;

/**
 * 定义一个类型 VueNode，表示 Vue 的虚拟节点，可以是 VNodeChild 或 JSX.Element。
 */
declare type VueNode = VNodeChild | JSX.Element;

/**
 * 定义一个泛型类型 Writable，用于创建一个可以被写的对象类型。
 * 它通过将对象的所有属性标记为非只读（-readonly）来实现。
 * @template T 原始对象的类型。
 */
export type Writable<T> = {
  -readonly [P in keyof T]: T[P];
};

/**
 * 定义一个泛型类型 Nullable<T>，该类型表示 T 类型或者 null。
 * 这个类型可以用于标记一个变量可能为 null，除了其原本的类型 T。
 *
 * @typeParam T - 代表任意类型。
 * @returns 返回一个联合类型，包含 T 和 null。
 */
declare type Nullable<T> = T | null;

/**
 * 定义一个类型别名 NonNullable<T>，用来排除 T 中可能为 null 或 undefined 的类型。
 * 该类型别名通过条件类型来实现，如果 T 是 null 或 undefined，则 NonNullable<T> 为 never，
 * 否则，NonNullable<T> 就是 T 本身。
 * @param T - 传入的类型参数。
 * @returns 返回经过判断后的类型，如果 T 包含 null 或 undefined，则返回 never，否则返回 T。
 */
declare type NonNullable<T> = T extends null | undefined ? never : T;

/**
 * 定义一个泛型类型Recordable，其属性键为string，属性值为泛型T。
 * 适用于创建任何键值对对象。
 */
declare type Recordable<T = any> = Record<string, T>;

/**
 * 定义一个只读的泛型类型ReadonlyRecordable，其属性键为string，属性值为泛型T。
 * 该类型确保对象的属性在使用时不能被重新赋值。
 */
declare type ReadonlyRecordable<T = any> = {
  readonly [key: string]: T;
};

/**
 * 定义一个泛型类型Indexable，其中的属性键为string，属性值为泛型T。
 * 代表可以使用字符串索引访问的类型。
 */
declare type Indexable<T = any> = {
  [key: string]: T;
};

/**
 * 定义一个深度部分泛型类型DeepPartial，其属性可以是任意深度的可选属性。
 * 用于创建一个对象，其中所有属性都是可选的，包括嵌套属性。
 */
declare type DeepPartial<T> = {
  [P in keyof T]?: DeepPartial<T[P]>;
};

/**
 * 定义一个类型，代表setTimeout函数的返回值。
 */
declare type TimeoutHandle = ReturnType<typeof setTimeout>;

/**
 * 定义一个类型，代表setInterval函数的返回值。
 */
declare type IntervalHandle = ReturnType<typeof setInterval>;

declare interface ChangeEvent extends Event {
  target: HTMLInputElement;
}

declare interface WheelEvent {
  path?: EventTarget[];
}
interface ImportMetaEnv extends ViteEnv {
  __: unknown;
}

declare function parseInt(s: string | number, radix?: number): number;

declare function parseFloat(string: string | number): number;

declare interface ViteEnv {
  VITE_USE_MOCK: boolean;
  VITE_PUBLIC_PATH: string;
  VITE_GLOB_APP_TITLE: string;
  VITE_BUILD_COMPRESS: 'gzip' | 'brotli' | 'none';
}
