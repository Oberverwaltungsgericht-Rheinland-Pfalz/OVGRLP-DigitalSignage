import { Component, Input, OnInit } from "@angular/core";

import { Termin } from "ds-common";

@Component({
  selector: "app-termin",
  template: `
    <div *ngIf="termin" fxFlex fxLayout="column" class="ds-box">
      <!-- Gericht, Kammer, Besetzung -->
      <div fxFlex="nogrow" fxLayout="row">
        <div fxFlex>
          <h1>
            {{ termin.gericht }}
          </h1>
          <h2>
            {{ termin.kammer }}. Kammer
          </h2>
        </div>
        <div fxFlex="40" fxLayout="column">
          <div *ngFor="let richter of termin.besetzung">
            {{ richter }}
          </div>
        </div>
      </div>

      <!-- Trenner -->
      <div fxFlex="nogrow" class="ds-divider"></div>

      <!-- Uhrzeit, Art, AZ -->
      <div fxFlex="nogrow" fxLayout="row">
        <div fxFlex="20">
          <h3>{{ termin.uhrzeitAktuell }} Uhr</h3>
        </div>
        <div fxFlex>
          <h3>{{ termin.art | capitalize }}</h3>
        </div>
        <div fxFlex="20">
          <h3>{{ termin.az }}</h3>
        </div>
      </div>

      <!-- Trenner -->
      <div fxFlex="nogrow" class="ds-divider"></div>

      <!-- Aktivpartei -->
      <div fxFlex="nogrow" fxLayout="row">
        <div fxFlex>
          <div *ngFor="let partei of termin.parteienAktiv">
            {{ partei }}
          </div>
        </div>
        <div fxFlex="5">
        </div>
        <div fxFlex>
          <div *ngFor="let pb of termin.prozBevAktiv">
            {{ pb }}
          </div>
        </div>
      </div>

      <!-- Trenner -->
      <div fxFlex="nogrow" class="ds-divider"></div>

      <!-- gegen -->
      <div fxFlex="nogrow" class="ds-gegen">
        gegen
      </div>

      <!-- Trenner -->
      <div fxFlex="nogrow" class="ds-divider"></div>

      <!-- Passivpartei -->
      <div fxFlex="nogrow" fxLayout="row">
        <div fxFlex>
          <div *ngFor="let partei of termin.parteienPassiv">
            {{ partei }}
          </div>
        </div>
        <div fxFlex="5">
        </div>
        <div fxFlex>
          <div *ngFor="let pb of termin.prozBevPassiv">
            {{ pb }}
          </div>
        </div>
      </div>

      <!-- Trenner -->
      <div fxFlex="nogrow" class="ds-divider"></div>

      <!-- wegen -->
      <div fxFlex="nogrow" fxLayout="row">
        <div fxFlex="8">
          wegen:
        </div>
        <div fxFlex>
          {{ termin.gegenstand }}
        </div>
      </div>
    </div>
  `,
  styles: [
    `
    :host {
      display: flex;
    }
  `
  ]
})
export class TerminComponent implements OnInit {
  constructor() {}

  @Input() termin: Termin;

  ngOnInit() {}
}
