<script setup lang="ts">
import { ref } from 'vue';
import Navbar, { navType } from '../components/Navbar.vue'
import { useRouter } from 'vue-router'

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

function Goto(path: string){
  debugger
  useRouter().push({name: path})
}

</script>

<template>
  <Navbar app-name="DS-Manager" 
    @goto="Goto" 
    :nav="nav"/>
  <div class="space"></div>
  <router-view></router-view>
</template>

<style lang="stylus" scoped>

</style>
