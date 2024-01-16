<script lang="ts">
// SPDX-FileCopyrightText: © 2019 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { defineComponent } from 'vue'
import Display from '../../src/models/Display'

export default  defineComponent({
  setup(){
    return {
      compact: false,
      display: false,
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
<clr-tabs v-if="!compact">
  <clr-tab>
    <button clrTabLink>Termine</button>
    <clr-tab-content *clrIfActive>
      <app-termine v-if="display" [displayName]="display.name"></app-termine>
    </clr-tab-content>
  </clr-tab>
  <clr-tab>
    <button clrTabLink>Anzeige</button>
    <clr-tab-content *clrIfActive>
      <app-display-control class="display-control" v-if="display" [display]="display" [sizeToScreenHeigt]="true"></app-display-control>
    </clr-tab-content>
  </clr-tab>
</clr-tabs>

<div fxLayout="row" fxLayout.lt-md="column" v-if="compact">
  <div fxFlex="78%" fxFlex.gt-md="65%">
    <app-termine v-if="display" [displayName]="display.name"></app-termine>
  </div>
  <div fxFlex="22%" fxFlex.gt-md="35%" class="display-control">
    <app-display-control v-if="display" [display]="display"></app-display-control>
  </div>
</div>
</template>