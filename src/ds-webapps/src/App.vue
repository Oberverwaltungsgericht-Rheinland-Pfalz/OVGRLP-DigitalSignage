<script setup lang="ts">
</script>

<template>
  <nav>
    <router-link :to="'/roomcontrol'">
      <h5>Roomcontrol</h5>
    </router-link>
    <router-link :to="'/dsmanager'">
      <h5>DS-Manager</h5>
    </router-link>
    <router-link :to="'/displays'">
      <h5>Displays</h5>
    </router-link>
  </nav>
  
  <router-view/>
</template>

<style lang="stylus" scoped>
nav
  display flex
  justify-content space-around
  width 100vw
  position fixed
  
h5
  margin 0
</style>
