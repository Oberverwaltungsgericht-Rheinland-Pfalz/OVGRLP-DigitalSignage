import { createApp } from 'vue'
import App from '../src/RoomcontrolApp.vue'

// @ts-expect-error
const version: string = __VERSION__ ?? ''
// @ts-expect-error
const buildDate: string = __BUILDDATE__ ?? ''
console.log('OVGRLP Digitalsignage WebUI v' + version + ', erstellt am ' + (buildDate))

const app = createApp(App)
app.provide<string>('app-version', version)
app.provide<string>('build-date', buildDate)

app.mount('#app')