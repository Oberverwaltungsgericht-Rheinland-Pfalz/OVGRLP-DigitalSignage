import { Injectable } from '@angular/core';

import { of } from "rxjs/observable/of";
import { Observable } from "rxjs/Observable";

import { DisplayService } from '@ds-suite/core';
import { Display, DisplayStatus, AppConfig } from '@ds-suite/model';

@Injectable()
export class DummyDisplayService implements DisplayService {
  getDisplays(): Observable<Display[]> {
    const displays: Display[] = [
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
      },
      {
        id: 4,
        name: "njz-ko-foyer",
        title: "Neues Justizzentrum Koblenz",
        template: "NjzFoyerComponent",
        styles: "",
        filter: "",
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
        id: 5,
        name: "njz-ko-saal",
        title: "Sitzungssaal",
        template: "NjzSaalComponent",
        styles: "",
        filter: "",
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
      display = displays.find((d) => d.name === name );
    });

    return of(display);
  }

  getDisplayStatus(display: Display): Observable<DisplayStatus> {
    const displayStatus: DisplayStatus = DisplayStatus.Active;

    console.log(
      `DisplayDummyService.getDisplayStatus(display: '${display.name}')`
    );

    return of(displayStatus);
  }


}
