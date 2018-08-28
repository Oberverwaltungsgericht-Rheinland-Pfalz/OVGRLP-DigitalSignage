import { Component, OnInit } from '@angular/core';

import { Display, DisplayStatus } from '@ds-suite/model';
import { DisplayService } from '@ds-suite/core';

@Component({
  selector: 'displays',
  templateUrl: './displays.component.html',
  styleUrls: ['./displays.component.css']
})
export class DisplaysComponent implements OnInit {
  displays: Display[];
  displayGroups: string[];

  constructor(private displayService: DisplayService) { }

  getDisplays() {
    this.displayService.getDisplays()
      .subscribe(
        displays => {
          this.displays = displays.sort((d1, d2) => d1.title > d2.title ? 1 : -1)
          this.DetermineDisplayGroups(this.displays);
        }
      );
  }

  GetDisplaysFromGroup(group: string): Display[]{
    console.log ("GetDisplaysFromGroup:",group)
    return this.displays.filter(t => t.group==group);
  }

  DetermineDisplayGroups(displays: Display[]): void {
    this.displayGroups=Array.from(new Set(displays.map(t => t.group)));
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

  async getDisplayStatus(display: Display) :DisplayStatus {
    return await this.displayService.getDisplayStatus(display)
      .subscribe(response => {return response as DisplayStatus;});
  }
  

  ngOnInit() {
    this.getDisplays();
  }
}
