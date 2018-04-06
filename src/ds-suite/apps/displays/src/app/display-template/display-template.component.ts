import { Component, OnInit, OnDestroy, Input, ViewChild, ViewChildren, ElementRef, QueryList } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
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
  @ViewChild('dsTermineContainer') dsTermineContainer: ElementRef;
  @ViewChildren('dsTermineChild') dsTermineChildren: QueryList<ElementRef>;

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
  scrollMode = false;

  constructor(private route: ActivatedRoute, private terminService: TerminService) { }

  loadTermine() {
    this.route.params.subscribe(params => {
      this.display = params as Display;

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
        this.scrollMode = this.isScrollMode();
  
        if (this.scrollMode)
          this.termine = this.termine.concat(tmpTermine);
        else
          this.termine = tmpTermine;
      });
    });
  }

  private isScrollMode(): boolean {
    if (this.dsTermineContainer && this.dsTermineChildren) {
      const containerHeight = this.dsTermineContainer.nativeElement.offsetHeight;
      let childrenHeight = 0;

      this.dsTermineChildren.forEach(element => {
        childrenHeight += element.nativeElement.offsetHeight;
      });

      return containerHeight < childrenHeight;
    } else {
      return false;
    }
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
      if (this.termine.length === 0 ||
        (this.scrollMode && this.termine.length <= this.termineCount) ||
        (!this.scrollMode)) {
        this.loadTermine();
      } else {
        if (this.scrollMode)
          this.termine.shift();
      }
    });
  }

  animationStarted(event: AnimationEvent) {
  }

  ngOnDestroy() {
    this.updateSub.unsubscribe();
  }
}
