import { createApp } from 'vue'
import App from '../src/Roomcontrol/RoomcontrolApp.vue'
import sharedMain from '../src/shared-main'

const app = createApp(App)

void sharedMain(app).then(() => {
  app.mount('#app')
})
