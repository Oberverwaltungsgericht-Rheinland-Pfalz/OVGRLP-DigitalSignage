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

  BesetzungExists() {
    return this.termin.besetzung.length > 0 ;
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
    return (this.termin.gegenstand!=null && this.termin.gegenstand.trim()!="") ;
  }

  AnzeigeBemerkung1() {
    let hasBemerkung1 = this.termin.bemerkung1!=null
    let showBemerkung1 = (this.termin.gericht !== "Verwaltungsgericht Trier" && !this.termin.gericht.includes('rbeitsgericht'))
    let showBemerkung2 = this.termin.gericht !== "Verwaltungsgericht Koblenz"
    return ( hasBemerkung1 && showBemerkung1 && showBemerkung2 && this.termin.bemerkung1.trim()!="");
  }
  
  ngOnInit() {}
}
