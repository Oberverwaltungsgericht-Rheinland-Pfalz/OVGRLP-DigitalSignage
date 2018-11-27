import { Component, Input, OnInit, OnDestroy, AfterViewInit, ViewChild } from '@angular/core';

import { Observable } from 'rxjs/Observable';
import { Subscription } from 'rxjs/Subscription';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/observable/timer';

import { Termin } from '@ds-suite/model';
import { TerminStatus } from '@ds-suite/model';
import { TerminService } from '@ds-suite/core';
import { ClrDatagrid } from '@clr/angular';

@Component({
  selector: 'app-termine',
  templateUrl: './termine.component.html',
  styleUrls: ['./termine.component.css']
})
export class TermineComponent implements OnInit, OnDestroy, AfterViewInit {
  private updateTimer: any;
  private updateSub: Subscription;
  @ViewChild('DataGridTermine') DataGridTermine: ClrDatagrid;
  loadingTermine: boolean = true;
  _displayName: string;
  termine: Termin[];

  GetStatausValues() : Array<string> {
    return Object.values(TerminStatus)
}

  constructor(
    private terminService: TerminService) {
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
        this.termine = termine;
        this.loadingTermine=false;
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
    this.updateTimer = Observable.timer(5000, 10000);
    this.updateSub = this.updateTimer.subscribe((t: any) => {
      this.loadTermine();
    });
  }

  ngOnDestroy() {
    this.updateSub.unsubscribe();
  }
}
