import { Component, OnDestroy, ElementRef, AfterViewChecked, ViewChild, Optional } from '@angular/core';
import { trigger, state, style, animate, transition, stagger, query, keyframes } from '@angular/animations';
import { Observable, Subscription, Scheduler } from 'rxjs/Rx';

import { DisplayTemplateComponent } from './../display-template.component';
import { OnInit } from '@angular/core/src/metadata/lifecycle_hooks';

import { Termin } from '@ds-suite/model';

@Component({
  template: `
    <div *ngIf="display" id="ds-display" fxLayout="column">
      <div id="ds-head-title" fxLayout="row">
        <div fxFlex="75" class="ds-head-title-left">
          {{ display.title }}
        </div>
        <div fxFlex class="ds-head-title-right" fxLayout="column">
          <div fxFlex>{{ datum | date: 'd. MMMM yyyy' }}</div>
          <div id="setmytime" fxFlex>{{ datum | date: 'H:mm' }} Uhr</div>
        </div>
      </div>

      <div id="ds-main" fxFlex fxLayout="column">
        <div id="ds-main-details">
          <!--
          <app-termin *ngIf="aktiverTermin" [termin]="aktiverTermin"></app-termin>
          <app-termin *ngIf="!aktiverTermin && naechsterTermin" [termin]="naechsterTermin"></app-termin>
          -->
          <div id="scrollarea" >
          <!-- b: Termin -->
            <div class="ds-termin" fxLayout="column" *ngFor="let termin of termine" 
                 [@terminAnimation]="'in'" (@terminAnimation.start)="animationStarted($event)">
              <!-- Gericht, Kammer, Besetzung -->
              <div fxFlex="grow" fxLayout="column">
                <div fxLayout="row" class="olg-layout-line">
                  <div fxFlex="70">
                  <h2>
                    {{ termin.gericht }}
                  </h2>
                  </div>
                  <div fxFlex="30">
                    <span *ngIf="termin.oeffentlich == 'nein'" class="olg-layout-nichtoeffentlich">
                      Nicht öffentliche Sitzung
                    </span>
                    <span *ngIf="termin.oeffentlich == 'ja'" class="olg-layout-oeffentlich">
                      Öffentliche Sitzung
                    </span>
                  </div>
                </div>

                <div fxLayout="row" class="olg-layout-line">
                  <div fxFlex="15"><h3>{{termin.uhrzeitAktuell}} Uhr</h3></div>
                  <div fxFlex="55"><h3>{{termin.art | capitalize}}</h3></div>
                  <div fxFlex="30" class="ds-text-right"><h3>{{termin.az}}</h3></div>
                </div>
                
                <div fxLayout="row" class="olg-layout-noline">
                  <div fxFlex="45">
                    <div style="font-weight:bold; margin-bottom: 8px;">{{termin.parteienAktiv}}</div>
                    <div>{{termin.prozBevAktiv}}</div>
                  </div>
                  <div fxFlex="10">
                    <div>gegen</div>
                    <div>&nbsp;</div>
                  </div>
                  <div fxFlex="45">
                    <div style="font-weight:bold; margin-bottom: 8px;">{{termin.parteienPassiv}}</div>
                    <div>{{termin.prozBevPassiv}}</div>
                  </div>
                </div>
                <div fxLayout="row" ng-hide="termin.bemerkung1 == 'FAMxy' && termin.oeffentlich == 'nein'" style="margin-top:10px">
                  <div fxFlex="15">wegen</div>
                  <div fxFlex="85"><strong>{{termin.gegenstand}}</strong></div>
                </div>
                
                <div class="olgstyle"></div>
              </div>
            </div>
            <!-- e: Termin -->
          </div>
        </div>
      </div>
    </div>
  `,
  styles: [
    `
    #scrollarea {
      height: 750px;
      overflow: hidden;
    }
    .ds-termin {
      overflow: hidden;
      padding-top: 30px;
    }
    .olgstyle {
      margin: 50px 0;
      border: 0;
      height: 1px;
      background-image: linear-gradient(to right, rgba(0, 0, 0, 0), rgba(0, 0, 0, 0.75), rgba(0, 0, 0, 0));
    }
    #ds-display {
      height: 1024px;
      width: 1280px;
      margin: 0px;
      padding: 0px;
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
      background: url('assets/img/rlp_wappen_600x735.png') center 20px no-repeat;
    }

    #ds-main-details > div {
      background-color: rgba(255, 255, 255, 0.8);
      box-shadow: 0 1px 3px 0 rgba(0,0,0,.2), 0 1px 1px 0 rgba(0,0,0,.14), 0 2px 1px -1px rgba(0,0,0,.12);
      padding: 10px;
    }

    #ds-main-overview {
      padding: 10px;
    }
    
    .olg-layout-line {
    padding: 10px 0;
    border-bottom: 1px solid #ddd;
    }
    .olg-layout-noline {
    padding: 40px 0 20px 0;
    }
    .olg-layout-oeffentlich {
    font-weight: bold;
    color: #3d7417;
    }
    .olg-layout-nichtoeffentlich {
    font-weight: bold;
    color: #871d33;
    }
  `
  ],
  animations: [
    trigger('terminAnimation', [
      state('in', style({ opacity: 1, height: '*', 'padding-top': '*' })),
      transition('in => void', [
        animate(
          '2s ease-out',
          keyframes([style({ opacity: 0, offset: 0.3, 'padding-top': 0 }), style({ height: 0, offset: 1 })])
        )
      ])
    ])
  ]
})
export class NjzKhFoyerTemplateComponentUnten extends DisplayTemplateComponent {
  ngOnInit() {
    /* set parameters here */
    this.updateInterval = 10000;
    super.ngOnInit();
  }
}
