<script setup lang="ts">
  import {ref} from 'vue'
  import DisplaysApp from './DisplaysApp.vue'
  import DsManagerApp from './DsManagerApp.vue'
  import RoomcontrolApp from './RoomcontrolApp.vue'
  const show = ref('rc')
</script>

<template>
  <nav>
    <button @click="show='rc'">
      <h5>Roomcontrol</h5>
    </button>
    <button @click="show='dm'">
      <h5>Display-Manager</h5>
    </button>
    <button @click="show='ds'">
      <h5>Displays</h5>
    </button>
  </nav>

  <roomcontrol-app v-if="show==='rc'"/>
  <ds-manager-app v-else-if="show==='dm'"/>
  <displays-app v-else-if="show==='ds'"/>
</template>

<style lang="stylus" scoped>
nav
  display flex
  justify-content space-around
  width 100vw
  position fixed
  
h5
  margin 0
</style>
