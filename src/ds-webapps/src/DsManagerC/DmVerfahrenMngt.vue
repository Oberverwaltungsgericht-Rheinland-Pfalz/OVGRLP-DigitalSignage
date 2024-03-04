<script lang="ts">
// SPDX-FileCopyrightText: © 2019 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>, Reiner Bamberger <4329883+reinerBa@users.noreply.github.com>
// SPDX-License-Identifier: EUPL-1.2
import { defineComponent } from 'vue'
import { VerfahrenDto, VerfahrenService } from '../apis/WebApiCore'
import DmVerfahrenFilter from './DmVerfahrenFilter.vue'

export default  defineComponent({
  components: { DmVerfahrenFilter },
  data(){return{
    verfahren: [] as VerfahrenDto[]
  }},
  async mounted() {
    const res = await VerfahrenService.getDatenVerfahren()
    this.verfahren.splice(0, Infinity, ...res)
  },
  methods: {
    async openModal(verfahren: VerfahrenDto){
      console.log(verfahren)
    },
    async changePublic(termin: VerfahrenDto, newFlag: boolean){

      if(newFlag)
        termin.oeffentlich = 'ja'
      else
        termin.oeffentlich = 'nein'

      await this.changeStatus(termin)
    },
    async changeStatus(termin: VerfahrenDto){
      await VerfahrenService.putDatenVerfahren(termin.id ??0, termin)
      this.$emit('refresh')
    }
  }
  })
</script>

<template>
  <main class="flex">
    <DmVerfahrenFilter :verfahren="verfahren"/> 
<table>
  <thead>
    <tr>
      <th></th><th>Uhrzeit</th>
      <th>Gericht</th>
      <th>Saal</th>
      <th>Aktenzeichen</th>
      <th>Aktivpartei</th>
      <th>Passivpartei</th>
      <th>Öffentlich</th>
      <th>Status</th>
    </tr>
  </thead>
  <tbody v-if="verfahren.length == 0">
    <tr><td id="no-dates" colspan="7">Es sind keine Verfahren vorhanden</td></tr>
  </tbody>
  <tbody v-for="(v, idx) of verfahren" :key="'verf'+idx">
    <tr>
      <td><button @click="openModal(v)" class="material-icons pseudo">edit</button></td>
      <td>{{ v.uhrzeitAktuell }}</td>
      <td>{{ v.gericht }}</td>
      <td>{{ v.sitzungssaal }}</td>
      <td>{{ v.az }}</td>
      <td>{{ v.parteienAktivKurz }}</td>
      <td>{{ v.parteienPassivKurz }}</td>

      <td>
        <button v-if="v.oeffentlich == 'ja'" @click="changePublic(v, false)" class="material-icons pseudo toggle_on">toggle_on</button>
        <button v-else @click="changePublic(v, true)" class="material-icons pseudo">toggle_off</button>
      </td>
      <td>
        <select v-model="v.status" @change="changeStatus(v)" class="status-select">
          <option></option>
          <option>Läuft</option>
          <option>Abgeschlossen</option>
          <option>Aufgehoben</option>
          <option>Unterbrochen</option>
        </select>
      </td>
    </tr>
  </tbody>
</table>
</main>
</template>

<style lang="stylus" scoped>
td, th
  padding-right 10px
  padding-left 10px
  color black
th
  font-weight bold
  background-color #ccc
  border .75px solid rgb(204, 204, 204)
  padding 0.5rem 0.6rem
td
  color rgb(102, 102, 102)
td > button
  margin 0
  padding 0
  font-size 2.25rem
  width -webkit-fill-available
table
  height fit-content
  font-size 13px
  background-color white
  border .75px solid rgb(204, 204, 204)
.status-select
  min-width 3rem
.toggle_on
  color rgb(90, 162, 32)
#no-dates
  text-align center
  padding 2rem
  font-size 1.5rem
</style>
