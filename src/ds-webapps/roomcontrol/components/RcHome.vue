<script lang="ts">
// SPDX-FileCopyrightText: © 2019 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>, Reiner Bamberger <4329883+reinerBa@users.noreply.github.com>
// SPDX-License-Identifier: EUPL-1.2
import { defineComponent } from 'vue'
import Display from '../../src/models/Display'

export default  defineComponent({
  setup(){
    return {
      displays:[] as Display[]
    }
  },
  methods: {
    async GetDisplays(): Promise<Display[]> {
      // this.displays = displays.sort((d1, d2) => d1.title > d2.title ? 1 : -1)
      return Promise.all([])
    }
  },
  mounted() {
    this.GetDisplays()
  }
  })
</script>

<template>
  <li v-for="display of displays" :key="'dis'+display">
    <a :href="display.name">{{ display.title }}</a>
  </li>
</template>