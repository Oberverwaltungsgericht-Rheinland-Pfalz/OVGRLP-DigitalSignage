<script lang="ts">
// SPDX-FileCopyrightText: © 2019 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import DisplayStatus from '../components/DisplayStatus.vue'
import { Display, DisplaysService } from '../apis/WebApiCore'
import { defineComponent, PropType } from 'vue'
import axios from 'axios'

let statusIntervalId: number
let screenshotIntervalId: number
export default  defineComponent({
  props: {
    display: {
      type: Object as PropType<Display>,
        required: true
      }
    },
    components: { DisplayStatus },
    setup() {
     
      return {
        status: 0,
        screenshotUrl: '',
          screenshot: '',
          ImageHeight: 0,
          displayStatus: 0
        };
    },
    data() { return {
            compact: false,
      }},
    methods: {
      openModal() { },
      loadStatus(){
        DisplaysService.getSettingsDisplaysStatus(this.display.name??'').then(status => {
          this.displayStatus = status
          this.$forceUpdate()
        })
      },
      loadScreenshot(){
        axios.get(this.screenshotUrl).then((res) => {
          this.screenshot = res.data
          this.$forceUpdate()
        })
      },
      async startClick() { 
        await DisplaysService.getSettingsDisplaysStart(this.display.name??'')
      },
      async shutdownClick() { 
        await DisplaysService.getSettingsDisplaysStop(this.display.name??'')
      },
      async restartClick() {
        await DisplaysService.getSettingsDisplaysRestart(this.display.name??'')
       }
    },
    async mounted() {
      statusIntervalId = setInterval(this.loadStatus, 1e4)
      this.loadStatus()
      
      this.screenshotUrl = await DisplaysService.getSettingsDisplaysScreenshotUrl(this.display.name??'')

      screenshotIntervalId = setInterval(this.loadScreenshot, 1e4)
      this.loadScreenshot()
    },
    onBeforeUnmount(){
      clearInterval(statusIntervalId)
      clearInterval(screenshotIntervalId)
    }
})
</script>

<template>
<article class="card">
  <header>
    <h3>Anzeigegerät {{ display.name }}</h3>
  </header>
  <div>
    <img v-if="screenshot" :src="screenshot" @click="openModal()" class="screenshot">
    <DisplayStatus :status="displayStatus" :description="display.description??''" class="display-status"/>
  </div>
  <footer>
    <button v-if="displayStatus === 0" @click="startClick" class="pseudo">
      <span class="material-icons">play</span> Anschalten</button>
    <button v-if="displayStatus > 0" @click="shutdownClick" class="pseudo">
      <span class="material-icons">power_settings_new</span> Ausschalten</button>
    <button v-if="displayStatus > 0" @click="restartClick" class="pseudo">
      <span class="material-icons">replay</span> Neu starten</button>
  </footer>
</article>
<!--
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
</div>-->
</template>

<style lang="stylus" scoped>
.card
  height fit-content
.screenshot
  width 100%
.display-status
  padding 0 2rem
</style>
