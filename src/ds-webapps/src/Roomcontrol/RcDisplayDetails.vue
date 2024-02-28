<script setup lang="ts">
// SPDX-FileCopyrightText: © 2019 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>, Reiner Bamberger <4329883+reinerBa@users.noreply.github.com>
// SPDX-License-Identifier: EUPL-1.2
import axios from 'axios'
import Display from '../models/Display'
import { PropType, onBeforeUnmount, ref } from 'vue'
import Termin from '../models/Termin'
import RcDisplay from './RcDisplay.vue'
import RcTermine from './RcTermine.vue'

const props = defineProps({
  display: {
    type: Object as PropType<Display>,
    required: true
  }
})

const termine = ref([] as Termin[])

function GetTermine(): void {
  axios.get<Termin[]>(`/settings/displays/${props.display.name}/termine`).then(res => {
    if(JSON.stringify(termine.value) !== JSON.stringify(res.data))
      termine.value.splice(0, Infinity, ...res.data) 
  })
}

const intervalId = setInterval(GetTermine, 1e4)
GetTermine()

onBeforeUnmount(()=>{
  clearInterval(intervalId)
})

/*import { defineComponent, PropType, ref } from 'vue'
import RcDisplay from './RcDisplay.vue'
import RcTermine from './RcTermine.vue'
import Display from '../models/Display'
import axios from 'axios'
import Termin from '../models/Termin'

export default  defineComponent({
  components: { RcDisplay, RcTermine },
  props:{
    display: {
      type: Object as PropType<Display>,
      required: true
    }
  },
  async setup() {
    const termine = ref([] as Termin[])

    async function GetTermine(){
      var res = await axios.get<Termin[]>('')
      termine.value.splice(0, Infinity, ...res.data)
    }
    setInterval(GetTermine, 1e4)
    await GetTermine()

    return{
      termine
    }
  }
  })*/
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
  grid-template-columns auto 350px
  gap 0 1em
</style>