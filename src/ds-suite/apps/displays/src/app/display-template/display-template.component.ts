import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Subscription } from 'rxjs/Subscription';

import { Termin, Display } from '@ds-suite/model';
import { TerminService } from '@ds-suite/core';

import 'rxjs/add/operator/switchMap';
import { filter } from 'rxjs/operators/filter';

@Component({
  selector: 'app-display-template',
  templateUrl: './display-template.component.html',
  styleUrls: ['./display-template.component.css']
})
export class DisplayTemplateComponent implements OnInit, OnDestroy {
  private updateTimer: any;
  private updateSub: Subscription;

  public updateInterval = 10000;

  display: Display;
  aktiverTermin: Termin;
  naechsterTermin: Termin;
  termine: Termin[] = [];
  termineOffen: Termin[] = [];
  termineCount = 0;
  datum: Date;

  constructor(private terminService: TerminService) {}

  loadTermine() {
    this.terminService.getTermine(this.display.name).subscribe(result => {
      let tmpTermine: Termin[] = result;

      tmpTermine = this.filterTermine(tmpTermine);
      tmpTermine = this.sortTermine(tmpTermine);
      this.termineCount = tmpTermine.length;
      this.aktiverTermin = this.findAktiverTermin(tmpTermine);
      this.termineOffen = tmpTermine.filter(
        termin => !(termin.status === 'Abgeschlossen' || termin.status === 'Aufgehoben')
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
    return termine.filter(termin => termin.uhrzeitAktuell !== 'omV');
  }

  findAktiverTermin(termine: Termin[]): Termin {
    return termine.find(termin => termin.status === 'Läuft');
  }

  ngOnInit() {
    this.updateTimer = Observable.timer(2000, this.updateInterval);
    this.updateSub = this.updateTimer.subscribe((t: any) => {
      this.datum = new Date();
      if (this.display) {
        if (this.termine.length === 0 || this.termine.length <= this.termineCount) {
          this.loadTermine();
        } else {
          this.termine.shift();
        }
      }
    });
  }

  animationStarted(event: AnimationEvent) {
  }

  ngOnDestroy() {
    this.updateSub.unsubscribe();
  }
}
