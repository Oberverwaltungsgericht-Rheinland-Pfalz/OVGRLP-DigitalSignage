import { createApp } from 'vue'
import './style.styl'
import App from './App.vue'
import sharedMain from './shared-main'

const app = createApp(App)
void sharedMain(app).then(() => {
  app.mount('#app')
})
