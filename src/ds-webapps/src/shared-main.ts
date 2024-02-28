import 'picnic/picnic.min.css'
import 'material-icons/iconfont/material-icons.css'
import { App } from 'vue'

export default function(app: App<Element>): void {
  // @ts-expect-error
  const version: string = __VERSION__ ?? ''
  // @ts-expect-error
  const buildDate: string = __BUILDDATE__ ?? ''
  console.log(`OVGRLP Digitalsignage WebUI v${version}, erstellt am ${buildDate}`)

  app.provide<string>('app-version', version)
  app.provide<string>('build-date', buildDate)
}
