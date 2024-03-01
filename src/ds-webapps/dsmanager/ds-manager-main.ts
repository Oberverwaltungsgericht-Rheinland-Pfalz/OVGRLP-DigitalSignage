import { createApp } from 'vue'
import App from '../src/DsManagerC/DsManagerApp.vue'
import sharedMain from '../src/shared-main'
import router from '../src/Routes/DmRouter'

const app = createApp(App)

void sharedMain(app).then(() => {
  app.use(router)
  app.mount('#app')
})
