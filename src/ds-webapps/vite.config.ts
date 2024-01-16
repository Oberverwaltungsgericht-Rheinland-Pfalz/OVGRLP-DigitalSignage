import { UserConfigExport, ConfigEnv } from 'vite'
import vue from '@vitejs/plugin-vue'
import fs from 'fs'
import { resolve } from 'path'
import pack from './package.json'

export default ({ command, mode }: ConfigEnv): UserConfigExport => {
  const version: string = pack.version
  const buildDate: string = new Date().toLocaleString()

  return {
    build: {
      minify: false,
      rollupOptions: {
        input: {
          roomcontrol: resolve(__dirname, 'roomcontrol/index.html'),
          dsManager: resolve(__dirname, 'ds-manager/index.html'),
          displays: resolve(__dirname, 'displays/index.html')
        },
      },
    },
  base: './',
  plugins: [vue()],
  define: {
    __VERSION__: '"' + version + '"',
    __BUILDDATE__: '"' + buildDate + '"',
  }
}}
