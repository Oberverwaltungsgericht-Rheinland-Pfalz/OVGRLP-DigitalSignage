<script lang="ts">
// SPDX-FileCopyrightText: © 2019 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>, Reiner Bamberger <4329883+reinerBa@users.noreply.github.com>
// SPDX-License-Identifier: EUPL-1.2
import { defineComponent } from 'vue'
import Display from '../models/Display'
import axios from 'axios'
import RcDisplayDetails from './RcDisplayDetails.vue'

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
      const url = window.location.origin + '/settings/displays'
      var res = await axios.get<Display[]>(url)
      this.displays.splice(0, Infinity,...res.data)
    }
  },
  async mounted() {
    await this.GetDisplays()
  }
  })
</script>

<template>
<div class="container">

  <h3 v-if="!displaySelected">Anzeigen:</h3>
  <h3 v-else>
    <button @click="displaySelected = null" class="material-icons pseudo">arrow_back</button>
    <span>{{ displaySelected.name }}</span>&ensp;
  </h3>
  <div v-if="displays.length === 0">Keine Anzeigen vorhanden</div>
  <button v-show="!displaySelected" v-for="display of displays" :key="'dis'+display.id" 
    @click="displaySelected = display">
    <span class="material-icons">monitor</span>
    <a>{{ display.title }}</a>
    <div>
      
    </div>
  </button>
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
</style>