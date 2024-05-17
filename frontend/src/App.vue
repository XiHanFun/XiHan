<template>
  <NConfigProvider
    :theme="theme"
    :theme-overrides="theme === null ? lightThemeOverrides : darkThemeOverrides"
  >
    <NThemeEditor>
      <AppProvider>
        <RouterView />
      </AppProvider>
    </NThemeEditor>
  </NConfigProvider>
</template>

<script lang="ts" setup>
  import { defineComponent, ref } from 'vue';
  import { useRoute } from 'vue-router';
  import {
    NConfigProvider,
    NThemeEditor,
    GlobalThemeOverrides,
    useOsTheme,
    darkTheme,
  } from 'naive-ui';
  // import { AppProvider } from '@/components/Application';
</script>
