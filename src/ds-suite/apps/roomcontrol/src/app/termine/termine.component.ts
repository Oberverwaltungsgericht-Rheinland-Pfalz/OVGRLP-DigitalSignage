import { Component, Input, OnInit } from '@angular/core';

import { Termin } from '@ds-suite/model';
import { TerminService } from '@ds-suite/core';

@Component({
  selector: 'app-termine',
  templateUrl: './termine.component.html',
  styleUrls: ['./termine.component.css']
})
export class TermineComponent implements OnInit {

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
    this.terminService.getTermine(displayName)
      .subscribe(termine => {
        this.termine = termine;
      });
  }
  get displayName(): string { return this._displayName; }

  changeOeffentlich(termin: Termin) {
    console.log("now");
  }

  ngOnInit() {
  }

  onSubmit() {
  }
}
