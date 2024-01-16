<script setup lang="ts">
// SPDX-FileCopyrightText: © 2019 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { inject } from 'vue'
import RcHome from '../roomcontrol/components/RcHome.vue'

const appVersion = inject<string>('app-version')
</script>

<template>
<div class="main-container">
  <alert></alert>
  <div class="alert alert-app-level">
    Version {{appVersion}}
  </div>
  <header class="header header-6">
    <div class="branding">
      <a href="#" class="nav-link">
        <clr-icon shape="display"></clr-icon>
        <span class="title">Saalsteuerung</span>
      </a>
    </div>
    <div class="header-actions">
      <a href="#" class="nav-link nav-icon">
        <clr-icon shape="help-info"></clr-icon>
      </a>
    </div>
  </header>
  <div class="content-container">
    <div class="content-area">
      <RcHome />
    </div>
  </div>
</div>
</template>

<style scoped>

</style>
