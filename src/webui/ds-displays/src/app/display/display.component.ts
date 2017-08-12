import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Observable, Subscription } from 'rxjs/Rx';

import 'rxjs/add/operator/switchMap';

import { DisplayService, Display, TerminService, Termin } from 'ds-core';

@Component({
  selector: 'app-display',
  templateUrl: './display.component.html',
  styleUrls: ['./display.component.css'],
  providers: [DisplayService, TerminService]
})
export class DisplayComponent implements OnInit, OnDestroy {
  private updateTimer;
  private updateSub : Subscription;
  display: Display;
  aktiverTermin: Termin;
  naechsterTermin: Termin;
  alleTermine: Termin[];
  offeneTermine: Termin[];
  datum: Date;

  constructor(
    private displayService: DisplayService,
    private terminService: TerminService,
    private route: ActivatedRoute
    ) { }

  loadDisplay() {
    this.route.params
      .switchMap((params: Params) => this.displayService.getDisplay(params['name']))
      .subscribe(display => {
        this.display = display;
        this.loadTermine(display.name);
      });
  }

  loadTermine(name: string) {
    this.terminService.getTermine(name)
      .subscribe(termine => {
        this.alleTermine = termine.filter(termin => termin.uhrzeitAktuell != 'omV');
        this.aktiverTermin = this.alleTermine.find(termin => termin.status === 'Läuft');
        this.offeneTermine = this.alleTermine.filter(termin => !(termin.status === 'Abgeschlossen' || termin.status === 'Aufgehoben'));
        this.naechsterTermin = this.aktiverTermin ? null : this.offeneTermine[0]; 
      });
  }

  ngOnInit() {
    this.updateTimer = Observable.timer(2000, 5000);
    this.updateSub = this.updateTimer.subscribe(t => {
      this.datum = new Date();
      this.loadDisplay()
    });
  }

  ngOnDestroy() {
    this.updateSub.unsubscribe();
  }
}
