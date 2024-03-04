// SPDX-FileCopyrightText: © 2019 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>, Reiner Bamberger <4329883+reinerBa@users.noreply.github.com>
// SPDX-License-Identifier: EUPL-1.2
import { createRouter, createWebHashHistory } from 'vue-router'
import DmVerfahrenMngt from '../DsManagerC/DmVerfahrenMngt.vue'
import DmDisplayGroups from '../DsManagerC/DmDisplayGroups.vue'
import DmAnnoncementMngt from '../DsManagerC/DmAnnouncementMngt.vue'

const history = createWebHashHistory()

const routes = [
  { path: '/displays', component: DmDisplayGroups, name: 'Displays' },
  { path: '/Verfahren', component: DmVerfahrenMngt, name: 'Verfahren' },
  { path: '/sondermeldungen', component: DmAnnoncementMngt, name: 'Sondermeldungen' },
  { path: '/:pathMatch(.*)', component: DmVerfahrenMngt, name: 'Home' }
]

const router = createRouter({ history, routes })

router.afterEach((to) => {
  document.title = `Digitalsignage | ${String(to.name)}`
})

export default router
