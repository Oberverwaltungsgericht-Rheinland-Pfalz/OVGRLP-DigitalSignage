<script lang="ts">
// SPDX-FileCopyrightText: © 2019 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>, Reiner Bamberger <4329883+reinerBa@users.noreply.github.com>
// SPDX-License-Identifier: EUPL-1.2
import { defineComponent } from 'vue'
import { DisplaysService, DisplayExDto } from '../apis/WebApiCore'

interface DisplayGroup { name: string, displays: DisplayExDto[]}

export default  defineComponent({
  data(){
    return {
      groups: [] as Array<DisplayGroup>
    }
  },
  methods: {
    async GetDisplays(): Promise<void> {
      const data = await DisplaysService.getSettingsDisplaysDisplaysEx()
      const groups: DisplayGroup[] = []
      data.forEach(dis => {
        const groupName = dis.group ??''
        const match = groups.find(g => g.name === groupName)
        
        if (match != undefined)
          match.displays.push(dis)
        else
          groups.push({name: groupName, displays: [dis]})
      })
      groups.forEach(g => {
        g.displays.sort((a, b) => (a.title??'') > (b.title??'') ? 1 : -1)
      })
      this.groups.splice(0, Infinity,...groups)
    },
    status2Text(val: number){
      return val === 0 ? 'ausgeschaltet' : val === 1 ? 'eingeschaltet' : val === 2 ? 'aktiv' : 'unbekannt'
    }
  },
  async mounted() {
    await this.GetDisplays()
  }
})
</script>

<template>
<article>
  <details v-for="(group, idx1) of groups" class="display-group" open>
    <summary>
      <span class="group-name">
        <i class="material-icons">expand_more</i>
        {{ group.name }} </span>
      <div>
        <button class="material-icons pseudo">replay</button>
        <button class="material-icons pseudo">smart_display</button>
        <button class="material-icons pseudo">replay_circle_filled</button>
        <button class="material-icons pseudo">power_settings_new</button>
      </div>
    </summary>
  <table>
    <tbody>
      <tr v-for="(display, idx2) of group.displays" :key="idx1 + 'dr' + idx2">
        <td><button class="material-icons pseudo">replay</button></td>
        <td>{{ display.title }}</td>
        <td><button class="material-icons pseudo">info</button></td>
        <td>{{ status2Text(display.status) }}</td>
        <td><button class="material-icons pseudo">smart_display</button>Anschalten</td>
        <td><img :src="display.screenshotUrl"/></td>
        <td>TERMINE ANZEIGEN</td>
      </tr>
    </tbody>
  </table>
  </details>
</article>
</template>

<style lang="stylus" scoped>
details
  margin 0 1rem
  border-left .5px solid darkgrey
  border-right .5px solid darkgrey
article
  margin-top 1rem
summary
  height fit-content
  font-size 13px
  background-color white
  border .75px solid rgb(204, 204, 204)
  display inline-flex
  width 100%
  justify-content space-between
  background-color rgb(217, 228, 234)
.group-name
  padding .5rem
  font-weight 500
  font-size 1.1rem
table
  width 100%
</style>
