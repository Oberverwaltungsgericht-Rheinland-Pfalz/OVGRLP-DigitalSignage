import { Component, Input, OnInit, OnDestroy, AfterViewInit, ViewChild } from '@angular/core';

import { timer } from 'rxjs';
import { Subscription } from 'rxjs/Subscription';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/observable/timer';

import { Termin } from '@ds-suite/model';
import { TerminStatus } from '@ds-suite/model';
import { TerminService } from '@ds-suite/core';
import { ClrDatagrid } from '@clr/angular';
import { JsonConfigService } from '@ds-suite/backend/src';
import { AppConfigRoomcontrol } from '@ds-suite/model/src/lib/app-config';

@Component({
  selector: 'app-termine',
  templateUrl: './termine.component.html',
  styleUrls: ['./termine.component.css']
})
export class TermineComponent implements OnInit, OnDestroy, AfterViewInit {
  private updateTimer: any;
  private updateSub: Subscription;
  @ViewChild('DataGridTermine', { static: true }) DataGridTermine: ClrDatagrid;
  loadingTermine: boolean = true;
  _displayName: string;
  termine: Termin[];
  showBesetzung: boolean = false;

  GetStatausValues() : Array<string> {
    return Object.values(TerminStatus)
}

  constructor(
    private terminService: TerminService, private jsonConfigService: JsonConfigService) {
      let config = jsonConfigService.getConfig() as  AppConfigRoomcontrol
      this.showBesetzung = Boolean(config.showBesetzung)
  }

  @Input()
  set displayName(displayName: string) {
    this._displayName = displayName;
    this.loadTermine();
  }
  get displayName(): string { return this._displayName; }

  loadTermine() {
    this.terminService.getTermine(this.displayName)
      .subscribe(termine => {
        this.termine = this.sortTermine(termine);
        this.loadingTermine=false;
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

  changeOeffentlich(termin: Termin) {
    if(termin.oeffentlich === 'ja')
      termin.oeffentlich = 'nein';
    else
      termin.oeffentlich = 'ja';

    this.terminService.saveTermin(termin).subscribe(val => { },
      err => {
        console.error(err);
      });
  }

  changeStatus(termin: Termin) {
    this.terminService.saveTermin(termin).subscribe(val => { },
      err => {
        console.error(err);
      });
  }

  ngOnInit() {
  }

  onResize(event) {
    this.DataGridTermine.resize();
  }

  onSubmit() {
  }

  ngAfterViewInit(): void {
    this.updateTimer = timer(5000, 10000);
    this.updateSub = this.updateTimer.subscribe((t: any) => {
      this.loadTermine();
    });
  }

  ngOnDestroy() {
    this.updateSub.unsubscribe();
  }
}
