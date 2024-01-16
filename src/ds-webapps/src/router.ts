// SPDX-FileCopyrightText: © 2019 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>, Reiner Bamberger <4329883+reinerBa@users.noreply.github.com>
// SPDX-License-Identifier: EUPL-1.2
import { createRouter, createWebHistory } from 'vue-router'
import Roomcontrol from './RoomcontrolApp.vue'
import DsManager from './DsManagerApp.vue'
import Displays from './DisplaysApp.vue'

const history = createWebHistory()
const routes = [
  { path: '/roomcontrol', component: Roomcontrol, name: 'Saalsteuerung' },
  { path: '/dsmanager', component: DsManager, name: 'Administration' },
  { path: '/displays', component: Displays, name: 'Anzeigen' },
  { path: '/:pathMatch(.*)', component: Roomcontrol, name: 'Home' }
]
const router = createRouter({ history, routes })

router.afterEach((to) => {
  document.title = `Digitalsignage | ${String(to.name)}`
})

export default router
