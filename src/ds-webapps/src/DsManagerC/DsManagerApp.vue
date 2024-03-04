<script lang="ts">
import { defineComponent } from 'vue';
import Navbar from '../components/Navbar.vue'


export default defineComponent({
  components: {Navbar},
  data(){return{
    nav: [
      {
        name: 'Displays', 
        icon: 'monitor'
      },
      {
        name: 'Verfahren', 
        icon: 'calendar_month'
      },
      {
        name: 'Sondermeldungen', 
        icon: 'announcement'
      }]
  }},
  methods:{
    Goto(path: string) {
      console.log(path)
      this.$router.push({name: path})
    }
  }
})
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
