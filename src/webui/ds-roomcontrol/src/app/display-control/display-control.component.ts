import { Component, Input, OnInit, OnDestroy } from '@angular/core';

import { Display, DisplayService, DisplayStatus } from 'ds-core';

@Component({
  selector: 'app-display-control',
  templateUrl: './display-control.component.html',
  styleUrls: ['./display-control.component.css']
})
export class DisplayControlComponent implements OnInit {

  _display: Display;
  screenshot: String;
  status: DisplayStatus;

  constructor(private displayService: DisplayService) { }

  @Input()
  set display(display: Display) {
    this._display = display;
    this.displayService.getDisplayStatus(this._display)
      .subscribe(response => {
        this.status = response
      });
  }
  get display(): Display { return this._display; }

  ngOnInit() {
  }

}
