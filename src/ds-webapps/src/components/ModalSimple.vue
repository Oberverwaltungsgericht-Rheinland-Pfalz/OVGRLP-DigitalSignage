<script setup lang="ts">
// SPDX-FileCopyrightText: © 2019 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { ref, watch } from 'vue'
defineProps({
  noFooter:{
    type: Boolean,
    default: true
  }
})
const emit = defineEmits(['close'])

const showModal = ref(true)
watch(showModal, async (newVal)=>{
  if(newVal === false){
    emit('close')
  }
})
</script>

<template>
<div class="modal">
  <input id="modal_1" type="checkbox" v-model="showModal" checked/>
  <label for="modal_1" class="overlay"></label>
  <article>
    <header>{{ showModal }}
      <slot name="header"></slot>
      <label for="modal_1" class="close">&times;</label>
    </header>
    <section class="content">
      <slot></slot>
    </section>
    <footer v-if="!noFooter">
      <slot name="footer"></slot>
    </footer>
  </article>
</div>
</template>

<style lang="stylus" scoped>
.modal article
  min-width 800px
  width fit-content
</style>