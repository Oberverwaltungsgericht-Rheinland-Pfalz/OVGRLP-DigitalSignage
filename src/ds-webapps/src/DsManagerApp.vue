<script setup lang="ts">
import { ref } from 'vue';
import Navbar, { navType } from './components/Navbar.vue';

const navigationPoints: navType[] = [
{
  name: 'Displays', 
  icon: 'monitor'
},
{
  name: 'Termine', 
  icon: 'calendar_month'
},
{
  name: 'Sondermeldungen', 
  icon: 'announcement'
}]
const nav = ref(navigationPoints)
</script>

<template>
  <Navbar app-name="DS-Manager" 
    :on-goto="()=>{}" 
    :nav="nav"/>
  <div class="space"></div>
</template>

<style lang="stylus" scoped>

</style>
