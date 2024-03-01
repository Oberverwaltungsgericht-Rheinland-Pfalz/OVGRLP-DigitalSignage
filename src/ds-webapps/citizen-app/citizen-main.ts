import { createApp } from 'vue'
import App from '../src/CitizenApp.vue'
import sharedMain from '../src/shared-main'
import '../src/style.styl'

const app = createApp(App)

void sharedMain(app).then(() => {
  app.mount('#app')
})
