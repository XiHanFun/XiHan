<script lang="ts">
  import { SelectProps, ButtonProps } from 'naive-ui'

  type SelectThemeOverrides = NonNullable<SelectProps['themeOverrides']>
  type ButtonThemeOverrides = NonNullable<ButtonProps['themeOverrides']>

  const selectThemeOverrides: SelectThemeOverrides = {
    menuBoxShadow:
      '0 6px 16px -9px rgba(0, 0, 0, .08), 0 9px 28px 0 rgba(0, 0, 0, .05), 0 12px 48px 16px rgba(0, 0, 0, .03)',
    peers: {
      InternalSelection: {
        textColor: '#FF0000',
        heightMedium: '42px'
      }
    }
  }
  const buttonThemeOverrides: ButtonThemeOverrides = {
    heightMedium: '40px',
    textColor: 'rgba(24, 127, 231, 1)'
  }

  // ...
</script>

<template>
  <n-select
    v-model:value="value"
    :options="options"
    :theme-overrides="selectThemeOverrides"
  />
  <n-button :theme-overrides="buttonThemeOverrides">theme</n-button>
</template>
