<script lang="ts">
// SPDX-FileCopyrightText: © 2019 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { defineComponent } from 'vue'
import Display from '../../src/models/Display'
import DisplayStatus from '../../src/components/DisplayStatus.vue'

export default  defineComponent({
  components: {DisplayStatus},
  setup(){
    return {
      status: 0,
      screenshot: '',
      ImageHeight: 0,
      display: {} as Display,
      displays:[] as Display[]
    }
  },
  methods: {
    async startClick(){ },
    async updateClick(){ },
    async shutdownClick(){ },
    async restartClick(){ }
  }
})
</script>

<template>
  <div class="card" v-if="display">
  <div class="card-header">
    Anzeigegerät {{ display.name }}
  </div>
  <div class="card-img"  id="ScreenshotImage" v-if="screenshot">
    <img :src="screenshot" @click="modal.open(screenshot)" class="img-middle-proportion" :style="{height: ImageHeight+'px'}">
  </div>
  <div class="card-block">
    <div class="card-text">
      <DisplayStatus :status="status" :description="'Status: '" />
    </div>
  </div>
  <div class="card-footer">
    <button class="btn btn-sm btn-link" v-if="(status < 1)" @click="startClick()"><clr-icon shape="play"></clr-icon> Anschalten</button>
    <button class="btn btn-sm btn-link" v-if="(status >= 1)" @click="updateClick()"><clr-icon shape="refresh"></clr-icon> Aktualisieren</button>
    <button class="btn btn-sm btn-link" v-if="(status >= 1)" @click="shutdownClick()"><clr-icon shape="power"></clr-icon> Ausschalten</button>
    <button class="btn btn-sm btn-link" v-if="(status >= 1)" @click="restartClick()"><clr-icon shape="replay-all"></clr-icon> Neu starten</button>
  </div>
</div>


<app-display-dialog></app-display-dialog>
</template>