import { Component } from '@angular/core';

import { TemplateComponent } from '../app/display/template.component';

@Component({
  template: `
  <div *ngIf="display" id="ds-display" fxLayout="column">

    <div id="ds-head-title" fxLayout="row">
      <div fxFlex="75" class="ds-head-title-left">
        {{ display.title }}
      </div>
      <div fxFlex class="ds-head-title-right" fxLayout="column">
        <div fxFlex>{{ datum | date: 'd. MMMM yyyy' }}</div>
        <div fxFlex>{{ datum | date: 'H:mm' }} Uhr</div>
      </div>
    </div>
  
    <div id="ds-main" fxFlex fxLayout="row">
      <div id="ds-main-overview" fxFlex fxLayout="row">
        <div class="ds-overview-termin" *ngFor="let termin of alleTermine">
          {{ termin.uhrzeitAktuell }} Uhr - {{ termin.az }}<br />
          {{ termin.art }}<br />
          {{ termin.status }}
        </div>
      </div>
    </div>
  </div>
  `,
  styles: [`
    #ds-display {
      height: 1080px;
      width: 1920px;
      margin: 0px;
      padding: 0px;
    }
  
    #ds-head-status div {
      padding: 10px;
    }
    
    #ds-head-title {
      height: 120px;
      background-color: rgb(51,51,51);
      color: rgb(240,240,240);
    }

    .ds-head-title-left {
      padding: 10px;
      font-size: 3.2em;
      font-weight: bold;
      letter-spacing: 0.2em;
      line-height: 1.2em;
    }
    
    .ds-head-title-right {
      padding: 8px;
      text-align: right;
      line-height: 1.20;
      font-size: 1.8em;
      font-weight: bold;
    }
  
    #ds-main-overview {
      padding:10px;
      background: url('assets/img/rlp_wappen_600x735.png') center no-repeat;
    }
  `]
})
export class NjzFoyerTemplateComponent extends TemplateComponent { }