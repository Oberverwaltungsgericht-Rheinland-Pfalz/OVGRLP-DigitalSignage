import { createApp } from 'vue'
import './style.styl'
import App from './App.vue'
import sharedMain from './shared-main'

const app = createApp(App)
sharedMain(app)

app.mount('#app')
