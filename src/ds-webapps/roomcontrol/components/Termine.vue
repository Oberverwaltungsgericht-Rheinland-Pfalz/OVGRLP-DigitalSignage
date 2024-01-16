<script lang="ts">
// SPDX-FileCopyrightText: © 2019 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { defineComponent } from 'vue'
import Termin from '../../src/models/Termin'
import DisplayStatus from '../../src/components/DisplayStatus.vue'

export default  defineComponent({
  components: {DisplayStatus},
  setup(){
    return {
      loadingTermine: true,
      _displayName: '',
      termine: [] as Termin[],
      showBesetzung: false
    }
  },
  props: ['displayName'],
  methods: {
    async changeOeffentlich(termin: Termin){
      if (termin.oeffentlich === 'ja') { termin.oeffentlich = 'nein' } else { termin.oeffentlich = 'ja' }

      this.terminService.saveTermin(termin).subscribe(val => { },
        err => {
          console.error(err)
        })
    },
    async changeStatus(termin: Termin){
      this.terminService.saveTermin(termin).subscribe(val => { },
      err => {
        console.error(err)
      })
    }
  },
  mounted(){

  }
})
</script>


<template>
  <form>
  <clr-datagrid #DataGridTermine (window:resize)="onResize($event)" [clrDgLoading]="loadingTermine">

    <clr-dg-column class="ds-termin-column-uhrzeit">Uhrzeit</clr-dg-column>
    <clr-dg-column class="ds-termin-column-az">Aktenzeichen</clr-dg-column>
    <clr-dg-column class="ds-termin-column-partei">Aktivpartei</clr-dg-column>
    <clr-dg-column class="ds-termin-column-partei">Passivpartei</clr-dg-column>
    <clr-dg-column *ngIf="showBesetzung" class="ds-termin-column-partei">Besetzung</clr-dg-column>
    <clr-dg-column class="ds-termin-column-oeffentlich">Öffentlich</clr-dg-column>
    <clr-dg-column class="ds-termin-column-status">Status</clr-dg-column>

    <clr-dg-placeholder>Es sind keine Termine vorhanden</clr-dg-placeholder>
    <clr-dg-row v-for="(termin, i) of termine" :key="'termine'+i">
      <clr-dg-cell class="ds-termin-column-uhrzeit">{{ termin.uhrzeitAktuell }}</clr-dg-cell>
      <clr-dg-cell class="ds-termin-column-az">{{ termin.az }}</clr-dg-cell>
      <clr-dg-cell class="ds-termin-column-partei">{{ termin.parteienAktivKurz }}</clr-dg-cell>
      <clr-dg-cell class="ds-termin-column-partei">{{ termin.parteienPassivKurz }}</clr-dg-cell>
      <clr-dg-cell *ngIf="showBesetzung" class="ds-termin-column-partei">{{ termin.besetzung }}</clr-dg-cell>
      <clr-dg-cell class="ds-termin-column-oeffentlich">
        <div class="toggle-switch">
          <input :id="'chk_oeff_' + i" clrToggle type="checkbox" :checked="!!termin.oeffentlich" @change="changeOeffentlich(termin)">
          <label :for="'chk_oeff_' + i"></label>
        </div>
      </clr-dg-cell>
      <clr-dg-cell class="ds-termin-column-status">
        <clr-select-container>
          <select clrSelect :name="'sel_status_' + i" :id="'sel_status_' + i" v-model="termin.status" @ngModelChange="changeStatus(termin)">
            <option v-for="(status, idx) of GetStatausValues()" :key="'valu'+idx" :value="status">{{ status }}</option>
          </select>
        </clr-select-container>
      </clr-dg-cell>
    </clr-dg-row>
  </clr-datagrid>
</form>

</template>