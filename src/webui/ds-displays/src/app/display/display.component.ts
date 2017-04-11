import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import 'rxjs/add/operator/switchMap';

import { DisplayService } from './display.service';
import { TerminService } from '../termin/termin.service';
import { Display } from './display';
import { Termin } from '../termin/termin';

@Component({
  selector: 'app-display',
  templateUrl: './display.component.html',
  styleUrls: ['./display.component.css'],
  providers: [DisplayService, TerminService]
})
export class DisplayComponent implements OnInit {
  display: Display;
  aktiverTermin: Termin;
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
      });
  }

  ngOnInit() {
    this.datum = new Date();
    this.loadDisplay();
  }
}
