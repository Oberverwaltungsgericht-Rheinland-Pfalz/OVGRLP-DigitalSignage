<script setup lang="ts">
// SPDX-FileCopyrightText: © 2019 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>, Reiner Bamberger <4329883+reinerBa@users.noreply.github.com>
// SPDX-License-Identifier: EUPL-1.2
import { PropType, onBeforeUnmount, ref, getCurrentInstance } from 'vue'
import RcDisplay from './RcDisplay.vue'
import RcTermine from './RcTermine.vue'
import { DisplaysService, VerfahrenDto, Display } from '../apis/WebApiCore'

const props = defineProps({
  display: {
    type: Object as PropType<Display>,
    required: true
  }
})

const termine = ref([] as VerfahrenDto[])

function GetTermine(): void {
  DisplaysService.getSettingsDisplaysTermine(props.display.name??'').then(data => {
    if (JSON.stringify(termine.value) === JSON.stringify(data))
      return

    termine.value.splice(0, Infinity, ...data) 
    getCurrentInstance()?.proxy?.$forceUpdate()
  })
}

const intervalId = setInterval(GetTermine, 1e4)
GetTermine()

onBeforeUnmount(()=>{
  clearInterval(intervalId)
})
</script>

<template>
  <div id="grid">
    <RcTermine :termine="termine" @refresh="GetTermine"/>
    <RcDisplay :display="display"/>
  </div>
</template>

<style lang="stylus" scoped>
#grid
  display grid
  padding 0 1em
  grid-template-columns auto 400px
  gap 0 1em

@media screen and (max-width: 1048px)
  #grid
    display flow

    table
      width 100%  
    .card
      margin-top 1.5rem

</style>