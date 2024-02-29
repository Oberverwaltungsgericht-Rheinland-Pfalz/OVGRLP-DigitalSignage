import { UserConfigExport, ConfigEnv } from 'vite'
import vue from '@vitejs/plugin-vue'
import fs from 'fs'
import { resolve } from 'path'
import pack from './package.json'
import mocksimple from 'vite-plugin-mock-simple'
import mockRoutes from './mock-routes'

// @ts-expect-error
const dirname = __dirname
export default ({ command, mode }: ConfigEnv): UserConfigExport => {
  const version: string = pack.version
  const buildDate: string = new Date().toLocaleString()

  return {
    build: {
      minify: false,
      rollupOptions: {
        input: {
          roomcontrol: resolve(dirname, 'roomcontrol/index.html'),
          dsManager: resolve(dirname, 'dsmanager/index.html'),
          adminApp: resolve(dirname, 'admin-app/index.html'),
          citizenApp: resolve(dirname, 'citizen-app/index.html'),
          displays: resolve(dirname, 'displays/index.html')
        },
      },
    },
  base: './',
  plugins: [
    vue(), 
    mocksimple([...mockRoutes])
  ],
  define: {
    __VERSION__: '"' + version + '"',
    __BUILDDATE__: '"' + buildDate + '"',
  }
}}
