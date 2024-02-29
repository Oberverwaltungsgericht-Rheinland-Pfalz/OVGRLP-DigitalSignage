<script setup lang="ts">
import { ref } from 'vue';
import Navbar, { navType } from './components/Navbar.vue';

const navigationPoints: navType[] = [
{
  name: 'Räume verwalten', 
  icon: 'edit'
},
{
  name: 'Anzeige hinzufügen', 
  icon: 'add_chart'
},
{
  name: 'Anzeigen verwalten', 
  icon: 'edit'
}]
const nav = ref(navigationPoints)
</script>

<template>
  <Navbar app-name="Admin App" 
    :on-goto="()=>{}" 
    :nav="nav"/>
  <div class="space"></div>
</template>

<style lang="stylus" scoped>

</style>
