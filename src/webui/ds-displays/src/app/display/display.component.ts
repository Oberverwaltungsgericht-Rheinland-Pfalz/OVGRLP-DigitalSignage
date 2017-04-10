import 'rxjs/add/operator/switchMap';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';

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
  folgendeTermine: Termin[];
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
        this.loadTermine();
      });
  }

  loadTermine() {
    this.terminService.getTermine(this.display.name).then(termine => {
      this.folgendeTermine = termine;
      this.aktiverTermin = termine[1];  //TODO: Ermittlung des aktuellen Termins implementieren
      this.folgendeTermine = termine.slice(2);    //TODO: Ermittlung des nächsten Termins implementieren
    });
  }

  ngOnInit() {
    this.datum = new Date();
    this.loadDisplay();
  }
}
