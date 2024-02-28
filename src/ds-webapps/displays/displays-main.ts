import { createApp } from 'vue'
import App from '../src/DisplaysApp.vue'
import sharedMain from '../src/shared-main'

const app = createApp(App)
sharedMain(app)
app.mount('#app')
