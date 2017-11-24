import { Component, OnInit, OnDestroy } from '@angular/core';
import { Observable, Subscription } from 'rxjs/Rx';

import 'rxjs/add/operator/switchMap';

import { Display, TerminService, Termin } from 'ds-core';

@Component({
  template: `
    <h2>{{display.title}}</h2>
  `,
  styles: [''],
  providers: [TerminService]
})
export class TemplateComponent implements OnInit, OnDestroy {
  private updateTimer: any;
  private updateSub: Subscription;
  display: Display;
  aktiverTermin: Termin;
  naechsterTermin: Termin;
  alleTermine: Termin[] = [];
  offeneTermine: Termin[];
  sichtbareTermine : Termin[] = [];
  datum: Date;

  constructor(
    private terminService: TerminService
  ) { }

  loadTermine() {
    this.terminService.getTermine(this.display.name)
      .subscribe(termine => {
        this.alleTermine = termine.filter(termin => termin.uhrzeitAktuell != 'omV');
        this.aktiverTermin = this.alleTermine.find(termin => termin.status === 'Läuft');
        this.offeneTermine = this.alleTermine.filter(termin => !(termin.status === 'Abgeschlossen' || termin.status === 'Aufgehoben'));
        this.naechsterTermin = this.aktiverTermin ? null : this.offeneTermine[0];
      });
  }

  ngOnInit() {
    this.updateTimer = Observable.timer(2000, 5000);
    this.updateSub = this.updateTimer.subscribe((t: any) => {
      this.datum = new Date();
      if (this.display) {
        console.log(this.alleTermine.length);
        if(this.alleTermine.length > 0) {
          if(this.sichtbareTermine.length >= 2) {
            this.sichtbareTermine.shift();
          }
          this.sichtbareTermine.push(this.alleTermine.shift());
        } else {
          this.loadTermine();
        }
      }
    });
  }

  ngOnDestroy() {
    this.updateSub.unsubscribe();
  }
}
