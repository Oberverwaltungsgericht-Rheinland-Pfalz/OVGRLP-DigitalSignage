import { Injectable } from "@angular/core";
import { of } from "rxjs/observable/of";
import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/catch";
import "rxjs/add/operator/map";

import { DisplayService } from "../display.service";
import { Display } from "../../model/display";
import { DisplayStatus } from "../../model/displayStatus";

@Injectable()
export class DisplayDummyService implements DisplayService {
  private apiUrl: string;

  constructor() {}

  getDisplays(): Observable<Display[]> {
    let displays: Display[] = [
      {
        id: 1,
        name: "saal1",
        title: "Sitzungssaal I",
        template: "NjzSaalTemplateComponent",
        styles: "",
        filter: "SitzungssaalNr = 1",
        group: "Testanzeigen",
        controlUrl: "",
        netAddress: "",
        wolIpAddress: "",
        wolMacAddress: "",
        wolUdpPort: 9,
        description: null,
        notes: null,
        dummy: false
      },
      {
        id: 2,
        name: "saal2",
        title: "Sitzungssaal II",
        template: "NjzSaalTemplateComponent",
        styles: "",
        filter: "SitzungssaalNr = 2",
        group: "Testanzeigen",
        controlUrl: "",
        netAddress: "",
        wolIpAddress: "",
        wolMacAddress: "",
        wolUdpPort: 9,
        description: null,
        notes: null,
        dummy: false
      },
      {
        id: 3,
        name: "saal3",
        title: "Sitzungssaal III",
        template: "NjzKhFoyerTemplateComponentUnten",
        styles: "",
        filter: "SitzungssaalNr = 5",
        group: "Testanzeigen",
        controlUrl: "",
        netAddress: "",
        wolIpAddress: "",
        wolMacAddress: "",
        wolUdpPort: 9,
        description: null,
        notes: null,
        dummy: false
      }
    ];

    console.log("DisplayDummyService.getDisplays()");

    return of(displays);
  }

  getDisplay(name: string): Observable<Display> {
    let display: Display = new Display();

    console.log(`DisplayDummyService.getDisplay(name: '${name}')`);

    this.getDisplays().subscribe((displays) => {
      display = displays.find((display) => { return display.name === name });
    });

    return of(display);
  }

  getDisplayStatus(display: Display): Observable<DisplayStatus> {
    let displayStatus: DisplayStatus = DisplayStatus.Active;

    console.log(
      `DisplayDummyService.getDisplayStatus(display: '${display.name}')`
    );

    return of(displayStatus);
  }
}
