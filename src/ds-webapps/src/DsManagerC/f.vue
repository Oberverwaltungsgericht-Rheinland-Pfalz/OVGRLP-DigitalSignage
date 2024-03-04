<script lang="ts">
import { defineComponent } from 'vue'
import { VerfahrenDto, VerfahrenService } from '../apis/WebApiCore';

export default defineComponent({
  data(){return{
    verfahren: [] as VerfahrenDto[]
  }},
  async mounted() {
    const res = await VerfahrenService.getDatenVerfahren()
    this.verfahren.splice(0, Infinity, ...res)
  }
})
</script>

<template>
  <main>
    <div class="filter"></div>
    <div class="content">
      <table>
        <thead>
          <tr>
            <th></th><th>Uhrzeit</th>
            <th>Gericht</th>
            <th>Saal</th>
            <th>Aktenzeichen</th>
            <th>Aktivpartei</th>
            <th>Passivpartei</th>
            <th>Öffentlich</th>
            <th>Status</th>
          </tr>
        </thead>
        <tbody v-for="(v, idx) of verfahren" :key="'verf'+idx">
          <tr>
            <td><button class="material-icons">edit</button></td>
            <td>{{ v.uhrzeitAktuell }}</td>
            <td>{{ v.gericht }}</td>
            <td>{{ v.sitzungssaal }}</td>
            <td>{{ v.az }}</td>
            <td>{{ v.parteienAktivKurz }}</td>
            <td>{{ v.parteienPassivKurz }}</td>


            <td>{{ v.oeffentlich }}</td>
            <td>{{ v.status }}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </main>
</template>

<style lang="stylus" scoped>
.filter
  width 250px
</style>
