import { Component } from '@angular/core';
import { trigger, state, style, animate, transition, stagger, query, keyframes } from '@angular/animations';
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
  
    <div id="ds-main" fxFlex fxLayout="column">
      <div *ngFor="let termin of termine" [@terminAnimation]="'in'">
        <div class="ds-overview-termin" fxLayout="row">
          <div fxFlex="10">
            <strong>{{ termin.uhrzeitAktuell }} Uhr</strong>
          </div>
          <div fxFlex="25">
            <div>
              <strong>{{ termin.az }}</strong>
            </div>
            <div>
              {{ termin.gericht }}
            </div>
          </div>
          <div fxFlex>
            {{ termin.parteienAktivKurz }} ./. {{ termin.parteienPassivKurz }}
          </div>
          <div fxFlex="15">
            {{ termin.sitzungssaal }}
          </div>
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

    #ds-main {
      background: url('assets/img/rlp_wappen_600x735.png') center no-repeat;
    }

    .ds-overview-termin {
      font-size: 30px;
      margin: 10px;
      -webkit-box-shadow: 5px 5px 2px 0px rgba(0,0,0,0.2);
      -moz-box-shadow: 5px 5px 2px 0px rgba(0,0,0,0.2);
      box-shadow: 5px 5px 2px 0px rgba(0,0,0,0.2);
      background: rgba(255,255,255,0.6);
    }
  `],
  animations: [
    trigger('terminAnimation', [
      state('in', style({ opacity: 1, height: '*', 'padding-top': '*' })),
      transition('in => void', [
        animate('2s ease-out',
          keyframes([
            style({ opacity: 0, offset: 0.3, 'padding-top': 0 }),
            style({ height: 0, offset: 1 })
          ])
        )
      ])
    ])
  ]
})
export class NjzFoyer extends TemplateComponent { }