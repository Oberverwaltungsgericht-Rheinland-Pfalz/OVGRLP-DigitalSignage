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
  @Input() footerText: string;

  IstOeffentlich() {
    return this.termin.oeffentlich === 'ja';
  }

  IstAktiv() {
    return this.termin.status === 'Läuft';
  }

  AktivExists() {
    return (this.termin.parteienAktiv.length > 0 || this.termin.prozBevAktiv.length > 0);
  }

  PassivExists() {
    return (this.termin.parteienPassiv.length > 0 || this.termin.prozBevPassiv.length > 0) ;
  }

  AktivOrPassivExists() {
    return (this.AktivExists() || this.PassivExists()) ;
  }

  BeigeladenExists() {
    return this.termin.parteienBeigeladen.length > 0 ;
  }

  ZeugenOrSachvExists() {
    return (this.termin.parteienZeugen.length + this.termin.parteienSv.length) > 0 ;
  }

  BeteiligteExists() {
    return this.termin.parteienBeteiligt.length > 0 ;
  }

  PersonalVertr() {
    return this.termFunc.IstPersonalvertretung(this.termin)
  }

  GegenstandExists() {
    console.log("geg",this.termin.gegenstand)
    return (this.termin.gegenstand!=null && this.termin.gegenstand.trim()!="") ;
  }

  AnzeigeBemerkung1() {
    console.log("bem",this.termin.bemerkung1)
    return (this.termin.bemerkung1!=null && this.termin.bemerkung1.trim()!="");
  }

  ngOnInit() {}
}
