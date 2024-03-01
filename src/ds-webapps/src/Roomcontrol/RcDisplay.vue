<script lang="ts">
// SPDX-FileCopyrightText: © 2019 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import DisplayStatus from '../components/DisplayStatus.vue'
import { Display, DisplaysService } from '../apis/WebApiCore'
import { defineComponent, PropType } from 'vue'
import ModalSimple from '../components/ModalSimple.vue'

let statusIntervalId: number
let screenshotIntervalId: number
export default  defineComponent({
  props: {
    display: {
      type: Object as PropType<Display>,
        required: true
      }
    },
    components: { DisplayStatus, ModalSimple },
    setup() {
     
      return {
        status: 0,
        showModal: false,
        screenshotUrl: '',
        ImageHeight: 0,
        displayStatus: 0
        }
    },
    data() { return {
            compact: false,
      }},
    methods: {
      toggleModal(val: boolean) { 
        this.showModal = val
        this.$forceUpdate()
      },
      loadStatus(){
        DisplaysService.getSettingsDisplaysStatus(this.display.name??'').then(status => {
          this.displayStatus = status
          this.$forceUpdate()
        })
      },
      loadScreenshot(){
        if(this.displayStatus === 0) return

        const tmp = this.screenshotUrl 
        this.screenshotUrl = ''
        this.$nextTick(()=> {
          this.screenshotUrl = tmp
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
    <button @click="toggleModal(true)" class="material-icons pseudo">fullscreen</button>
  </header>
  <div>
    <div v-if="!displayStatus || !screenshotUrl" class="screnshot-placeholder"></div>
    <img v-else :src="screenshotUrl" @click="toggleModal(true)" class="screenshot"/>
    <DisplayStatus :status="displayStatus" :description="display.description??''" class="display-status"/>
  </div>
  <footer>
    <button v-if="displayStatus === 0" @click="startClick" class="pseudo">
      <span class="material-icons">smart_display</span> Anschalten</button>
    <button v-if="displayStatus > 0" @click="shutdownClick" class="pseudo">
      <span class="material-icons">power_settings_new</span> Ausschalten</button>
    <button v-if="displayStatus > 0" @click="restartClick" class="pseudo">
      <span class="material-icons">replay</span> Neu starten</button>
  </footer>

  <ModalSimple v-if="showModal" @close="toggleModal(false)">
    <template v-slot:header>
      <h3>Ansicht von: {{ display.name }}</h3>
    </template>
    <template #default>
      <img :src="screenshotUrl" class="screenshot"/>
    </template>
  </ModalSimple>
</article>
</template>

<style lang="stylus" scoped>
.card
  height fit-content
.screenshot
  width 100%
.display-status
  padding 0 2rem
.screnshot-placeholder
  background-color black
  width 100%
  height 400px
header h3
  margin-right 0 !important
</style>
