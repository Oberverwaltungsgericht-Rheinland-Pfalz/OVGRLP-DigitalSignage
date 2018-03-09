import { Injectable } from '@angular/core';

import { TerminService } from '@ds-suite/core';
import { Termin } from '@ds-suite/model';

import { of } from "rxjs/observable/of";
import { Observable } from "rxjs/Observable";

@Injectable()
export class DummyTerminService implements TerminService {
  getTermine(displayName: string): Observable<Termin[]> {
    const termine: Termin[] = [
      {
        id: 1,
        az: "1 K 1111/18.XY",
        lfdnr: 1,
        kammer: 1,
        sitzungssaal: "Sitzungssaal I",
        sitzungssaalNr: 1,
        uhrzeitPlan: "10:00",
        uhrzeitAktuell: "10:00",
        status: "",
        oeffentlich: "ja",
        art: "mündliche Verhandlung",
        gegenstand: "Rundfunkbeitrags",
        bemerkung1: "",
        bemerkung2: "",
        parteienAktiv: ["Frank Ferdinand\r"],
        prozBevAktiv: ["Proz.-Bev.: Rechtsanwalt Volkmar Stern\r"],
        parteienPassiv: [
          "Südwestrundfunk, vertreten durch den Intendanten\r"
        ],
        prozBevPassiv: [],
        parteienBeigeladen: [],
        prozBevBeigeladen: [],
        parteienZeugen: [],
        parteienSv: [],
        parteienAktivKurz: "Frank Ferdinand\r",
        parteienPassivKurz:
          "Südwestrundfunk, vertreten durch den Intendanten\r",
        stammdatenId: 1,
        gericht: "Testgericht Koblenz",
        datum: "09.01.2018",
        besetzung: ["Vorsitzender Richter am Testgericht Heinzmann"]
      },
      {
        id: 2,
        az: "1 K 2222/18.XY",
        lfdnr: 2,
        kammer: 1,
        sitzungssaal: "Sitzungssaal I",
        sitzungssaalNr: 1,
        uhrzeitPlan: "11:00",
        uhrzeitAktuell: "11:00",
        status: "",
        oeffentlich: "ja",
        art: "mündliche Verhandlung",
        gegenstand: "Rundfunkbeitrags",
        bemerkung1: "",
        bemerkung2: "",
        parteienAktiv: ["Simone Sterntaler\r"],
        prozBevAktiv: ["Proz.-Bev.: Rechtsanwalt Volkmar Stern\r"],
        parteienPassiv: [
          "Südwestrundfunk, vertreten durch den Intendanten\r"
        ],
        prozBevPassiv: [],
        parteienBeigeladen: [],
        prozBevBeigeladen: [],
        parteienZeugen: [],
        parteienSv: [],
        parteienAktivKurz: "Simone Sterntaler\r",
        parteienPassivKurz:
          "Südwestrundfunk, vertreten durch den Intendanten\r",
        stammdatenId: 1,
        gericht: "Testgericht Koblenz",
        datum: "09.01.2018",
        besetzung: ["Vorsitzender Richter am Testgericht Heinzmann"]
      },
      {
        id: 3,
        az: "1 K 3333/18.XY",
        lfdnr: 3,
        kammer: 1,
        sitzungssaal: "Sitzungssaal I",
        sitzungssaalNr: 1,
        uhrzeitPlan: "13:00",
        uhrzeitAktuell: "13:00",
        status: "",
        oeffentlich: "ja",
        art: "mündliche Verhandlung",
        gegenstand: "Rundfunkbeitrags",
        bemerkung1: "",
        bemerkung2: "",
        parteienAktiv: ["Peter Peterson\r"],
        prozBevAktiv: ["Proz.-Bev.: Rechtsanwalt Volkmar Stern\r"],
        parteienPassiv: [
          "Südwestrundfunk, vertreten durch den Intendanten\r"
        ],
        prozBevPassiv: [],
        parteienBeigeladen: [],
        prozBevBeigeladen: [],
        parteienZeugen: [],
        parteienSv: [],
        parteienAktivKurz: "Peter Peterson\r",
        parteienPassivKurz:
          "Südwestrundfunk, vertreten durch den Intendanten\r",
        stammdatenId: 1,
        gericht: "Testgericht Koblenz",
        datum: "09.01.2018",
        besetzung: ["Vorsitzender Richter am Testgericht Heinzmann"]
      },
    ];

    console.log(`TerminDummyService.getTermine()`);

    return of(termine);
  }
}
