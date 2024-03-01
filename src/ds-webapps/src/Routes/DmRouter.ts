// SPDX-FileCopyrightText: © 2019 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>, Reiner Bamberger <4329883+reinerBa@users.noreply.github.com>
// SPDX-License-Identifier: EUPL-1.2
import { createRouter, createWebHistory } from 'vue-router'
import DmTerminMngt from '../DsManagerC/DmTerminMngt.vue'
import DmDisplayGroups from '../DsManagerC/DmDisplayGroups.vue'
import DmAnnoncementMngt from '../DsManagerC/DmAnnouncementMngt.vue'

const history = createWebHistory()

const routes = [
  { path: '/displays', component: DmDisplayGroups, name: 'Displays' },
  { path: '/termine', component: DmTerminMngt, name: 'Termine' },
  { path: '/sondermeldungen', component: DmAnnoncementMngt, name: 'Sondermeldungen' },
  { path: '/:pathMatch(.*)', component: DmDisplayGroups, name: 'Home' }
]

const router = createRouter({ history, routes })

router.afterEach((to) => {
  document.title = `Digitalsignage | ${String(to.name)}`
})

export default router
