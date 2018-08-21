import { Component, Input, OnInit, ViewChild, AfterViewInit} from '@angular/core';

import { Display, DisplayStatus } from '@ds-suite/model';
import { DisplayService } from '@ds-suite/core';

import { DisplayDialogComponent } from '../display-dialog/display-dialog.component';
import {Resizer  } from '@ds-suite/core';

@Component({
  selector: 'app-display-control',
  templateUrl: './display-control.component.html',
  styleUrls: ['./display-control.component.css']
})
export class DisplayControlComponent implements OnInit, AfterViewInit {
  _display: Display;
  _sizeToScreenHeigt: boolean = false;
  screenshot: String;
  status: DisplayStatus;
  public ImageHeight: string = null;
  private resizer: Resizer;
  
  @ViewChild(DisplayDialogComponent) modal: DisplayDialogComponent;

  @Input()
  set sizeToScreenHeigt(sizeToScreenHeigt: boolean) {
    this._sizeToScreenHeigt = sizeToScreenHeigt;
    if (sizeToScreenHeigt) {
      this.ImageHeight="200"
    }
  };
  get sizeToScreenHeigt(): boolean { return this._sizeToScreenHeigt; }
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

  constructor(private displayService: DisplayService) { 
    this.resizer = new Resizer();
  }  

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

  ngAfterViewInit() {
    // Hack: das geht bestimmt auch besser, aber ich habe kein Event gefunden
    //       welches nach der vollen Darstellung feuert (auch nicht in der Doku von Angular Lifecycle Hooks), zumindest sind die höhen der Frames dort nicht auslesbar 
    //!\TODO: evtl. bessere Löung finden
    var foo = new Promise<void>(resolve => {
      setTimeout(resolve, 500);
    }).then(() => {
      this.onResize();
    });
    
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

  private onResize() {
    if(this.sizeToScreenHeigt) {
      var ImageFramename : string ="ScreenshotImage";
      var height: number = this.resizer.GetMaxHeight(document.getElementById(ImageFramename))-100;
      this.ImageHeight = height.toString();
    }
  }
}