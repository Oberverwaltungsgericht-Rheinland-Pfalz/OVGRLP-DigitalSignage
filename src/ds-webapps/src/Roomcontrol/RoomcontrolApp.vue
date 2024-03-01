<script setup lang="ts">
// SPDX-FileCopyrightText: © 2019 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import RcHome from './RcHome.vue'
import Navbar from '../components/Navbar.vue'
</script>

<template>
  <Navbar app-name="Roomcontrol" :on-goto="()=>{}" :nav="[{name: 'Saalsteuerung', icon: 'monitor'}]"/>
  <div class="space"></div>
  <RcHome />
</template>

<style lang="stylus">

</style>
