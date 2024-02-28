import { createApp } from 'vue'
import App from '../src/Roomcontrol/RoomcontrolApp.vue'
import sharedMain from '../src/shared-main'
import '../src/style.styl'

const app = createApp(App)
sharedMain(app)

app.mount('#app')