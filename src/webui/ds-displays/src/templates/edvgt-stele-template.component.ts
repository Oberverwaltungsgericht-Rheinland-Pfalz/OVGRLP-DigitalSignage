import { Component, OnInit } from '@angular/core';

import { TemplateComponent } from '../app/display/template.component';

@Component({
  template: `
    <div id="edvgt-head">
      Saalanzeige Rheinland-Pfalz
    </div>

    <div *ngIf="display" id="ds-display" fxLayout="column">

      <!-- Öffentlich / Nicht öffentlich / Sitzung unterbrochen / Nächste Sitzung -->
      <div fxLayout="column" id="ds-head-status" [ngClass]="
            {
              'ds-head-notrunning': !aktiverTermin,
              'ds-head-public': aktiverTermin && aktiverTermin.oeffentlich == 'ja',
              'ds-head-notpublic': aktiverTermin && aktiverTermin.oeffentlich == 'nein'
            }">
        <div *ngIf="aktiverTermin" fxFlex="grow">
          {{ (aktiverTermin.oeffentlich == 'ja') ? 'Öffentliche Sitzung' : '&nbsp;' }} 
          {{ (aktiverTermin.oeffentlich == 'nein') ? 'Nicht öffentliche Sitzung' : '&nbsp;' }}
        </div>
        <div *ngIf="!aktiverTermin && naechsterTermin" [ngClass]="">
          In Kürze
        </div>
      </div>

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
        <div id="ds-main-details" fxFlex fxLayout="row">

          <app-termin *ngIf="aktiverTermin" [termin]="aktiverTermin"></app-termin>

          <app-termin *ngIf="!aktiverTermin && naechsterTermin" [termin]="naechsterTermin"></app-termin>

        </div>

        <div id="ds-main-overview" fxFlex="25" fxLayout="column">
          <div class="ds-box" fxFlex fxLayout="column">
            <div fxFlex class="ds-box-header">
              Verfahren
            </div>
            <div fxFlex="grow" class="ds-box-content">
              <div class="ds-overview-termin" *ngFor="let termin of alleTermine">
                {{ termin.uhrzeitAktuell }} Uhr - {{ termin.az }}<br /> {{ termin.art }}<br /> {{ termin.status }}
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <div id="edvgt-foot">
      weitere Informationen
    </div>
  `,
  styles: [`
    #ds-display {
      height: 864px;
      width: 1080px;
      margin: 0px;
      padding: 0px;
      font-size: 18px;
      z-index: 1;
    }
    
    #edvgt-head {
      background-color: #871d33;
      color:#F1F1F1;
      font-weight: bold;
      height: 128px;
      font-size: 60px;
      text-align: center;
      line-height: 128px;
      box-shadow: 0 1px 3px 0 rgba(0,0,0,.2), 0 1px 1px 0 rgba(0,0,0,.14), 0 2px 1px -1px rgba(0,0,0,.12);
      z-index: 2;
      position: relative;
    }
    
    #ds-head-status {
      height: 80px;
      font-size: 1.8em;
      font-weight: bold;
      text-align: center;
    }
    
    #ds-head-status div {
      padding: 10px;
    }
    
    #ds-head-title {
      height: 80px;
      background-color: rgb(51,51,51);
      color: rgb(240,240,240);
    }

    .ds-head-title-left {
      padding: 10px;
      font-size: 2.4em;
      font-weight: bold;
      letter-spacing: 0.2em;
      line-height: 1.0em;
    }
    
    .ds-head-title-right {
      padding: 8px;
      text-align: right;
      line-height: 1.20;
      font-size: 1.2em;
      font-weight: bold;
    }
    
    #ds-main-details {
      padding:10px;
      background: url('assets/img/rlp_wappen_500x613.png') center no-repeat;
    }
    
    #ds-main-details > div {
      background-color: rgba(255, 255, 255, 0.8);
      box-shadow: 0 1px 3px 0 rgba(0,0,0,.2), 0 1px 1px 0 rgba(0,0,0,.14), 0 2px 1px -1px rgba(0,0,0,.12);
      padding: 10px;
    }
    
    #ds-main-overview {
      padding: 10px;
    }
  `]
})
export class EdvgtSteleTemplateComponent extends TemplateComponent {
}