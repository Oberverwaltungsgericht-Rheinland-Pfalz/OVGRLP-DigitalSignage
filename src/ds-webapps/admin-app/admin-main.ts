import { createApp } from 'vue'
import App from '../src/AdminApp.vue'
import sharedMain from '../src/shared-main'
import '../src/style.styl'

const app = createApp(App)

sharedMain(app).then(() => {
  app.mount('#app')
})
