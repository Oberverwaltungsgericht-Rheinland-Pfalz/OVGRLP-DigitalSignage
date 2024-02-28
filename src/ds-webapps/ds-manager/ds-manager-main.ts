import { createApp } from 'vue'
import App from '../src/DsManagerApp.vue'
import sharedMain from '../src/shared-main'

const app = createApp(App)

sharedMain(app).then(() => {
  app.mount('#app')
})