import { Component, Input, OnInit, OnDestroy, AfterViewInit } from '@angular/core';

import { Observable } from 'rxjs/Observable';
import { Subscription } from 'rxjs/Subscription';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/observable/timer';

import { Termin } from '@ds-suite/model';
import { TerminService } from '@ds-suite/core';

@Component({
  selector: 'app-termine',
  templateUrl: './termine.component.html',
  styleUrls: ['./termine.component.css']
})
export class TermineComponent implements OnInit, OnDestroy, AfterViewInit {
  private updateTimer: any;
  private updateSub: Subscription;
  _displayName: string;
  termine: Termin[];
  statusValues: string[] = [
    '',
    'läuft',
    'abgeschlossen',
    'aufgehoben',
    'unterbrochen'
  ];

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
      });
  }

  changeOeffentlich(termin: Termin) {
    if(termin.oeffentlich === 'ja')
      termin.oeffentlich = 'nein'
    else
      termin.oeffentlich = 'ja'

    this.terminService.saveTermin(termin).subscribe(val => { },
      err => {
        console.error(err);
      });
  }

  changeStatus(termin: Termin) {
    this.terminService.saveTermin(termin);
  }

  ngOnInit() {
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
