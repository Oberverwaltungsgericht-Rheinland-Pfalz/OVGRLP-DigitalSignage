import { Component, Input, OnInit, ViewChild } from '@angular/core';

import { Display, DisplayStatus } from '@ds-suite/model';
import { DisplayService } from '@ds-suite/core';

import { DisplayDialogComponent } from '../display-dialog/display-dialog.component';

@Component({
  selector: 'app-display-control',
  templateUrl: './display-control.component.html',
  styleUrls: ['./display-control.component.css']
})
export class DisplayControlComponent implements OnInit {
  _display: Display;
  screenshot: String;
  status: DisplayStatus;
  @ViewChild(DisplayDialogComponent) modal: DisplayDialogComponent;

  constructor(private displayService: DisplayService) { }

  @Input()
  set display(display: Display) {
    this._display = display;
    this.displayService.getDisplayStatus(this._display)
      .subscribe(response => {
        this.status = response
        this.updateScreenshot();
      });
  }
  get display(): Display { return this._display; }

  updateScreenshot() {
    if (this.status === DisplayStatus.Offline)
      this.screenshot = '/assets/img/offline.jpg'
    else if (this.status === DisplayStatus.Online)
      this.screenshot = `${this.display.controlUrl}/api/screenshot`;
    else
      this.screenshot = '/assets/img/unknown.jpg';
  }

  ngOnInit() {
  }

  DisplayStatusToString(stat: DisplayStatus) : string {
    var rval : string ="";
    switch (stat) {
      case DisplayStatus.Unknown:
        rval="unbekannt"
        break;
      case DisplayStatus.Active:
        rval="aktiv"
        break;
      case DisplayStatus.Online:
        rval="angeschaltet"
        break;
      case DisplayStatus.Offline:
        rval="ausgeschaltet"
        break;
      }
      return rval;
  }

}
