import { App } from 'vue'
import axios from 'axios'
import 'picnic/picnic.min.css'
import { OpenAPI } from './apis/WebApiCore'
import 'material-icons/iconfont/material-icons.css'
import { AppSettings } from './models/AppSettings'
import './style.styl'

export default async function (app: App<Element>): Promise<void> {
  // @ts-expect-error
  const version: string = __VERSION__ ?? ''
  // @ts-expect-error
  const buildDate: string = __BUILDDATE__ ?? ''
  console.log(`OVGRLP Digitalsignage WebApps v${version}, erstellt am ${buildDate}`)

  app.provide<string>('app-version', version)
  app.provide<string>('build-date', buildDate)

  await axios.get<AppSettings>('appsettings.json?a=0#', { headers: { cache: 'no-store' } }).then(response => {
    const appsettings = response.data
    app.provide<AppSettings>('settings', appsettings)

    if (appsettings.webApiUrl?.length > 0) OpenAPI.BASE = appsettings.webApiUrl
  })
}
