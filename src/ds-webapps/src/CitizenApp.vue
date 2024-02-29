<script setup lang="ts">
import Navbar from './components/Navbar.vue';
</script>

<template>
  <Navbar app-name="Citizen App" :on-goto="()=>{}" :nav="[{name: 'Meine Termine', icon: 'date_range'}]"/>
  <div class="space"></div>
</template>

<style lang="stylus" scoped>

</style>
