import { Component, OnInit, OnDestroy, Input } from "@angular/core";
import { Observable, Subscription } from "rxjs/Rx";

import "rxjs/add/operator/switchMap";

import { Termin, Display, TerminService } from "ds-common";
import { filter } from "rxjs/operators/filter";

@Component({
  template: `
    <h2>{{display.title}}</h2>
  `,
  styles: [""]
})
export class TemplateComponent implements OnInit, OnDestroy {
  private updateTimer: any;
  private updateSub: Subscription;

  public updateInterval: number = 10000;

  display: Display;
  aktiverTermin: Termin;
  naechsterTermin: Termin;
  termine: Termin[] = [];
  termineOffen: Termin[] = [];
  termineCount: number = 0;
  datum: Date;

  constructor(private terminService: TerminService) {}

  loadTermine() {
    console.log("load termine");
    this.terminService.getTermine(this.display.name).subscribe(result => {
      let tmpTermine: Termin[] = result;

      tmpTermine = this.filterTermine(tmpTermine);
      tmpTermine = this.sortTermine(tmpTermine);
      this.termineCount = tmpTermine.length;
      this.aktiverTermin = this.findAktiverTermin(tmpTermine);
      this.termineOffen = tmpTermine.filter(
        termin =>
          !(termin.status === "Abgeschlossen" || termin.status === "Aufgehoben")
      );
      this.naechsterTermin = this.aktiverTermin ? null : this.termineOffen[0];

      this.termine = this.termine.concat(tmpTermine);
    });
  }

  sortTermine(termine: Termin[]): Termin[] {
    return termine.sort((t1, t2) => {
      if (t1.uhrzeitAktuell > t2.uhrzeitAktuell) {
        return 1;
      } else if (t1.uhrzeitAktuell < t2.uhrzeitAktuell) {
        return -1;
      }
      return 0;
    });
  }

  filterTermine(termine: Termin[]): Termin[] {
    return termine.filter(termin => termin.uhrzeitAktuell != "omV");
  }

  findAktiverTermin(termine: Termin[]): Termin {
    return termine.find(termin => termin.status === "Läuft");
  }

  ngOnInit() {
    this.updateTimer = Observable.timer(2000, this.updateInterval);
    this.updateSub = this.updateTimer.subscribe((t: any) => {
      this.datum = new Date();
      if (this.display) {
        if (
          this.termine.length === 0 ||
          this.termine.length <= this.termineCount
        ) {
          this.loadTermine();
        } else {
          this.termine.shift();
          console.log(this.termine.length);
        }
      }
    });
  }

  animationStarted(event: AnimationEvent) {
    console.log(event);
  }

  ngOnDestroy() {
    this.updateSub.unsubscribe();
  }
}
