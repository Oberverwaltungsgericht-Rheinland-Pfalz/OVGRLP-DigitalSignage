<script lang="ts">
// SPDX-FileCopyrightText: © 2019 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>, Reiner Bamberger <4329883+reinerBa@users.noreply.github.com>
// SPDX-License-Identifier: EUPL-1.2
import { defineComponent, PropType } from 'vue'
import { VerfahrenDto } from '../apis/WebApiCore'

export default  defineComponent({
  props: {
    verfahren: {
      type: Object as PropType<VerfahrenDto[]>,
      required: true
    }
  },
  emits: ['add', 'refresh', 'filter'],
  data(){return{
    selectedStatus: [] as string[],
    selectedGericht: [] as string[],
    selectedSaal: [] as string[]
  }},
  computed:{
    StatusValues (): string[] {
      const rval = [...new Set(this.verfahren.map(t => t.status??''))].sort((d1, d2) => d1 > d2 ? 1 : -1)
      // für die Anzeige '' in 'offen' übersetzen
        const indOffen = rval.indexOf('')
        if (indOffen > -1)
          rval[indOffen] = 'Offen'

      return rval
    },
    GerichtValues (): string[] {
      return [...new Set(this.verfahren.map(t => t.gericht??''))].sort((d1, d2) => d1 > d2 ? 1 : -1)
    },
    SaalValues (): string[] {
      return [...new Set(this.verfahren.map(t => t.sitzungssaal??''))].sort((d1, d2) => d1 > d2 ? 1 : -1)
    }
  },
  methods: {
    toggleStatus (status: string) {
      if (this.selectedStatus.includes(status))
        this.selectedStatus.splice(this.selectedStatus.findIndex(v => v === status) ,1)
      else
        this.selectedStatus.push(status)
    },
    toggleGericht (Gericht: string) {
      if (this.selectedGericht.includes(Gericht))
        this.selectedGericht.splice(this.selectedGericht.findIndex(v => v === Gericht) ,1)
      else
        this.selectedGericht.push(Gericht)
    }, 
    toggleSaal (saal: string) { 
      if (this.selectedGericht.includes(saal))
        this.selectedGericht.splice(this.selectedGericht.findIndex(v => v === saal) ,1)
      else
        this.selectedGericht.push(saal)
    }
  }
})
</script>

<template>
  <div class="filter">
    <h6>Status</h6>
    <div v-for="(status, idx) of StatusValues" :key="'stat'+idx" @click="toggleStatus(status)">
      <button v-show="selectedStatus.includes(status)" class="material-icons pseudo">check_box</button>
      <button class="material-icons pseudo">check_box_outline_blank</button>
      <span>{{ status }}</span>
    </div>

    <h6>Gericht</h6>
    <div v-for="(gericht, idx) of GerichtValues" :key="'stat'+idx" @click="toggleGericht(gericht)">
      <button v-show="selectedGericht.includes(gericht)" class="material-icons pseudo">check_box</button>
      <button class="material-icons pseudo">check_box_outline_blank</button>
      <span>{{ gericht }}</span>
    </div>

    <h6>Saal</h6>
    <div v-for="(saal, idx) of GerichtValues" :key="'stat'+idx" @click="toggleGericht(saal)">
      <button v-show="selectedGericht.includes(saal)" class="material-icons pseudo">check_box</button>
      <button class="material-icons pseudo">check_box_outline_blank</button>
      <span>{{ saal }}</span>
    </div>

    <button class="vert"><span class="material-icons">replay</span> Aktualisieren</button>
    <button class="vert"><span class="material-icons">add</span> Hinzufügen</button>
  </div>
</template>

<style lang="stylus" scoped>
.filter
  width 300px
  max-width 300px
.vert
  display block
  width 100%
</style>