<script lang="ts">
// SPDX-FileCopyrightText: © 2019 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>, Reiner Bamberger <4329883+reinerBa@users.noreply.github.com>
// SPDX-License-Identifier: EUPL-1.2
import { defineComponent, PropType } from 'vue'
import { VerfahrenDto, VerfahrenService } from '../apis/WebApiCore'

export default  defineComponent({
  props: {
    termine: {
      type: Object as PropType<VerfahrenDto[]>,
      required: true
    }
  },
  methods: {
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
<table>
  <thead>
    <tr>
      <th>Uhrzeit</th>
      <th>Aktenzeichen</th>
      <th>Aktivpartei</th>
      <th>Passivpartei</th>
      <th>Besetzung</th>
      <th>Öffentlich</th>
      <th>Status</th>
    </tr>
  </thead>
  <tbody v-if="termine.length == 0">
    <tr><td id="no-dates" colspan="7">Es sind keine Termine vorhanden</td></tr>
  </tbody>
  <tbody>
    <tr v-for="(t, idx) in termine" :key="'tdetail'+idx">
      <td>{{ t.uhrzeitAktuell }}</td>
      <td>{{ t.az }}</td>
      <td>{{ t.parteienAktivKurz }}</td>
      <td>{{ t.parteienPassivKurz }}</td>
      <td>{{ t.besetzung!.join(', ') }}</td>
      <td>
        <button v-if="t.oeffentlich == 'ja'" @click="changePublic(t, false)" class="material-icons pseudo toggle_on">toggle_on</button>
        <button v-else @click="changePublic(t, true)" class="material-icons pseudo">toggle_off</button>
      </td>
      <td>
        <select v-model="t.status" @change="changeStatus(t)" class="status-select">
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
