<script lang="ts">
// SPDX-FileCopyrightText: © 2019 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>, Reiner Bamberger <4329883+reinerBa@users.noreply.github.com>
// SPDX-License-Identifier: EUPL-1.2
import { defineComponent, inject } from 'vue'

export default  defineComponent({
  setup() {
    const appVersion = inject<string>('app-version')
    return {
      appVersion
    }
  },
})
</script>

<template> 
<nav class="demo">
  <div class="app">Roomcontrol <span class="version">v {{appVersion}}</span></div>
  <a href="#" class="brand">    
    <button class="pseudo"><span class="material-icons">monitor</span> Saalsteuerung</button>
  </a>
  

  <!-- responsive-->
  <input id="cmenug" type="checkbox" class="show">
  <label for="cmenug" class="burger pseudo button">&#8801;</label>

  <div class="menu">
  </div>
</nav>
</template>

<style lang="stylus" scoped>
.version
  font-size small

nav
  background-color #00364d
  color white
  margin-top 1.1em
  .app
    position absolute
    top -1.5em
    padding-left 1.5em
    left 0
    background-color #0072a3
    width 100%
    font-size .8rem
</style>
