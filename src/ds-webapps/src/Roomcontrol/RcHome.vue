<script lang="ts">
// SPDX-FileCopyrightText: © 2019 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>, Reiner Bamberger <4329883+reinerBa@users.noreply.github.com>
// SPDX-License-Identifier: EUPL-1.2
import { defineComponent } from 'vue'
import RcDisplayDetails from './RcDisplayDetails.vue'
import { DisplaysService, Display } from '../apis/WebApiCore'

export default  defineComponent({
  components: {RcDisplayDetails},
  data(){
    return {
      displays:[] as Display[],
      displaySelected: null as Display | null
    }
  },
  methods: {
    async GetDisplays(): Promise<void> {
      const data = await DisplaysService.getSettingsDisplays()
      data.sort((a, b) => (a.title??'') > (b.title??'') ? 1 : -1)
      this.displays.splice(0, Infinity,...data)
    },
    setDisplay(dis: Display) {
      this.displaySelected = dis
      location.hash ='/'+ dis.name || ''
    },
    unsetDisplay(){
      this.displaySelected = null
      location.hash = ''
    }
  },
  async mounted() {
    await this.GetDisplays()
    
    const lastName = location.hash.replace(/#|\//g, '')
    if(lastName){
      const lastDisplay = this.displays.find(e => e.name === lastName)
      if(lastDisplay) this.displaySelected = lastDisplay
    }      
  }
  })
</script>

<template>
<div class="container">

  <h3 v-if="!displaySelected">Anzeigen:</h3>
  <h3 v-else>
    <button @click="unsetDisplay" class="material-icons pseudo">arrow_back</button>
    <span>{{ displaySelected.name }}</span>&ensp;
  </h3>

  <div v-if="displays.length === 0">Keine Anzeigen vorhanden</div>
  <div id="display-list">
  <button v-show="!displaySelected" v-for="display of displays" :key="'dis'+display.id" 
    @click="setDisplay(display)">
    <span class="material-icons">monitor</span>&ensp;
    <a>{{ display.title }}</a>
    &ensp;
    <span class="material-icons" 
      :title="`Name: ${display.name}\nGruppe: ${display.group
      }\nTemplate: ${display.template}\nFilter: ${display.filter ?? 'keiner'}`">info
      </span>
  </button>
  </div>
</div>

<RcDisplayDetails v-if="displaySelected" :display="displaySelected"/>
</template>

<style lang="stylus" scoped>
a
  cursor pointer
.container
  margin-left 1rem
button *
  color white
li span.material-icons
  vertical-align middle
h3 > button
  margin 0
.container > button
  margin-left 0.25rem
  margin-right 0.25rem
#display-list
  display list-item
  button
    min-width 30rem
    padding 1rem
    margin 1rem
</style>