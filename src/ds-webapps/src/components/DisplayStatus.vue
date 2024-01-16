<script lang="ts">
// SPDX-FileCopyrightText: © 2019 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { defineComponent } from 'vue'

export default  defineComponent({
  props:{
    status: {type: Number/*Object as PropType<DisplayStatus>*/, required: true},
    description: {type: String, required: true}
  },
  computed: {
    DisplayStatusToString(): string {
    let rval: string = ''
    switch (this.status) {
      case -1:
        rval = 'unbekannt'
        break
      case 2:
        rval = 'aktiv'
        break
      case 1:
        rval = 'angeschaltet'
        break
      case 0:
        rval = 'ausgeschaltet'
    }
    return rval
  }
  }
})
</script>

<template>
  <span :class="{'ds-bg-yellow':(status < 0), 'ds-bg-red': (status === 0), 'ds-bg-green': (status >= 1)}">
    {{ description }}
    {{ DisplayStatusToString }}
  </span>
</template>

<style lang="stylus" scoped>
.ds-bg-yellow
    color yellow

.ds-bg-red
    color red

.ds-bg-green
    color green
</style>