import { Component, Input, OnInit } from '@angular/core';

import { Termin } from '@ds-suite/model';
import { TerminFunctions } from '../shared/termin.functions';

@Component({
  selector: 'app-termin',
  templateUrl: './termin.component.html',
  styleUrls: ['./termin.component.css']
})
export class TerminComponent implements OnInit {
  public termFunc = TerminFunctions;

  constructor() {}

  @Input() termin: Termin;

  IstOeffentlich() {
    return this.termin.oeffentlich === 'ja';
  }

  IstAktiv() {
    return this.termin.status === 'Läuft';
  }

  ngOnInit() {}
}
