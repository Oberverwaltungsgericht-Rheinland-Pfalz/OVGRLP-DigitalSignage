import { Injectable } from '@angular/core';
import { Termin }  from './termin';
import { Display } from '../display/display';

const TERMINE: Termin[] = [
  {
    "id": 52,
    "az": "1 K 5743/16.TR",
    "lfdnr": 1,
    "kammer": 1,
    "sitzungssaal": "Sitzungssaal III",
    "uhrzeitPlan": "omV",
    "uhrzeitAktuell": "omV",
    "status": "",
    "oeffentlich": "ja",
    "art": "",
    "gegenstand": "Flüchtlingsrechts",
    "bemerkung1": "ER Dr. Trésoret (Asyl; omV)",
    "bemerkung2": "",
    "parteienAktiv": [
      "Mohammed THAER\r"
    ],
    "prozBevAktiv": [
      "Proz.-Bev.: Rechtsanwalt Dr. Mario Geuenich\r"
    ],
    "parteienPassiv": [
      "Bundesrepublik Deutschland vertreten durch den Leiter des Bundesamtes für Migration und Flüchtlinge,,- Außenstelle Trier -\r"
    ],
    "prozBevPassiv": [],
    "parteienBeigeladen": [],
    "prozBevBeigeladen": [],
    "parteienZeugen": [],
    "parteienSv": [],
    "parteienAktivKurz": "Mohammed THAER\r",
    "parteienPassivKurz": "Bundesrepublik Deutschland vertreten durch den Leiter des Bundesamtes für Migration und Flüchtlinge,,- Außenstelle Trier -\r",
    "stammdatenId": 4,
    "gericht": "Verwaltungsgericht Trier",
    "datum": "27.03.2017",
    "besetzung": [
      "Richter am Verwaltungsgericht Dr. Trésoret"
    ]
  },
  {
    "id": 53,
    "az": "1 K 5791/16.TR",
    "lfdnr": 2,
    "kammer": 1,
    "sitzungssaal": "Sitzungssaal III",
    "uhrzeitPlan": "omV",
    "uhrzeitAktuell": "omV",
    "status": "",
    "oeffentlich": "ja",
    "art": "",
    "gegenstand": "Flüchtlingsrechts",
    "bemerkung1": "ER Dr. Trésoret (Asyl; omV)",
    "bemerkung2": "",
    "parteienAktiv": [
      "Mariam ATIA\r"
    ],
    "prozBevAktiv": [
      "Proz.-Bev.: Rechtsanwälte Eisenträger & Dauch\r"
    ],
    "parteienPassiv": [
      "Bundesrepublik Deutschland vertreten durch den Leiter des Bundesamtes für Migration und Flüchtlinge,,- Außenstelle Trier -\r"
    ],
    "prozBevPassiv": [],
    "parteienBeigeladen": [],
    "prozBevBeigeladen": [],
    "parteienZeugen": [],
    "parteienSv": [],
    "parteienAktivKurz": "Mariam ATIA\r",
    "parteienPassivKurz": "Bundesrepublik Deutschland vertreten durch den Leiter des Bundesamtes für Migration und Flüchtlinge,,- Außenstelle Trier -\r",
    "stammdatenId": 4,
    "gericht": "Verwaltungsgericht Trier",
    "datum": "27.03.2017",
    "besetzung": [
      "Richter am Verwaltungsgericht Dr. Trésoret"
    ]
  },
  {
    "id": 54,
    "az": "1 K 5996/16.TR",
    "lfdnr": 3,
    "kammer": 1,
    "sitzungssaal": "Sitzungssaal III",
    "uhrzeitPlan": "omV",
    "uhrzeitAktuell": "omV",
    "status": "",
    "oeffentlich": "ja",
    "art": "",
    "gegenstand": "Flüchtlingsrechts",
    "bemerkung1": "ER Dr. Trésoret (Asyl; omV)",
    "bemerkung2": "",
    "parteienAktiv": [
      "Mahmoud MOHAMAD\r"
    ],
    "prozBevAktiv": [
      "Proz.-Bev.: Rechtsanwälte Becher & Dieckmann\r"
    ],
    "parteienPassiv": [
      "Bundesrepublik Deutschland vertreten durch den Leiter des Bundesamtes für Migration und Flüchtlinge,,- Außenstelle Trier -\r"
    ],
    "prozBevPassiv": [],
    "parteienBeigeladen": [],
    "prozBevBeigeladen": [],
    "parteienZeugen": [],
    "parteienSv": [],
    "parteienAktivKurz": "Mahmoud MOHAMAD\r",
    "parteienPassivKurz": "Bundesrepublik Deutschland vertreten durch den Leiter des Bundesamtes für Migration und Flüchtlinge,,- Außenstelle Trier -\r",
    "stammdatenId": 4,
    "gericht": "Verwaltungsgericht Trier",
    "datum": "27.03.2017",
    "besetzung": [
      "Richter am Verwaltungsgericht Dr. Trésoret"
    ]
  },
  {
    "id": 55,
    "az": "1 K 6051/16.TR",
    "lfdnr": 4,
    "kammer": 1,
    "sitzungssaal": "Sitzungssaal III",
    "uhrzeitPlan": "omV",
    "uhrzeitAktuell": "omV",
    "status": "",
    "oeffentlich": "ja",
    "art": "",
    "gegenstand": "Flüchtlingsrechts",
    "bemerkung1": "ER Dr. Trésoret (Asyl; omV)",
    "bemerkung2": "",
    "parteienAktiv": [
      "1. Ahmad CHRCHARA\r",
      "2. Yasmin GAZIE\r",
      "3. Kindes Lana CHRCHARA vertreten durch die Eltern Ahmad CHRCHARA und Yasmin GAZIE\r"
    ],
    "prozBevAktiv": [
      "Proz.-Bev.: zu 1-3: Rechtsanwalt Winfried Karczewski\r"
    ],
    "parteienPassiv": [
      "Bundesrepublik Deutschland vertreten durch den Leiter des Bundesamtes für Migration und Flüchtlinge,,- Außenstelle Trier -\r"
    ],
    "prozBevPassiv": [],
    "parteienBeigeladen": [],
    "prozBevBeigeladen": [],
    "parteienZeugen": [],
    "parteienSv": [],
    "parteienAktivKurz": "1. Ahmad CHRCHARA\r u.a.",
    "parteienPassivKurz": "Bundesrepublik Deutschland vertreten durch den Leiter des Bundesamtes für Migration und Flüchtlinge,,- Außenstelle Trier -\r",
    "stammdatenId": 4,
    "gericht": "Verwaltungsgericht Trier",
    "datum": "27.03.2017",
    "besetzung": [
      "Richter am Verwaltungsgericht Dr. Trésoret"
    ]
  },
  {
    "id": 56,
    "az": "1 K 6076/16.TR",
    "lfdnr": 5,
    "kammer": 1,
    "sitzungssaal": "Sitzungssaal III",
    "uhrzeitPlan": "omV",
    "uhrzeitAktuell": "omV",
    "status": "",
    "oeffentlich": "ja",
    "art": "",
    "gegenstand": "Flüchtlingsrechts",
    "bemerkung1": "ER Dr. Trésoret (Asyl; omV)",
    "bemerkung2": "",
    "parteienAktiv": [
      "Salaheldin ALASADI\r"
    ],
    "prozBevAktiv": [
      "Proz.-Bev.: Rechtsanwälte Busch & Burger\r"
    ],
    "parteienPassiv": [
      "Bundesrepublik Deutschland vertreten durch den Leiter des Bundesamtes für Migration und Flüchtlinge,,- Außenstelle Trier -\r"
    ],
    "prozBevPassiv": [],
    "parteienBeigeladen": [],
    "prozBevBeigeladen": [],
    "parteienZeugen": [],
    "parteienSv": [],
    "parteienAktivKurz": "Salaheldin ALASADI\r",
    "parteienPassivKurz": "Bundesrepublik Deutschland vertreten durch den Leiter des Bundesamtes für Migration und Flüchtlinge,,- Außenstelle Trier -\r",
    "stammdatenId": 4,
    "gericht": "Verwaltungsgericht Trier",
    "datum": "27.03.2017",
    "besetzung": [
      "Richter am Verwaltungsgericht Dr. Trésoret"
    ]
  },
  {
    "id": 57,
    "az": "1 K 6146/16.TR",
    "lfdnr": 6,
    "kammer": 1,
    "sitzungssaal": "Sitzungssaal III",
    "uhrzeitPlan": "omV",
    "uhrzeitAktuell": "omV",
    "status": "",
    "oeffentlich": "ja",
    "art": "",
    "gegenstand": "Flüchtlingsrechts",
    "bemerkung1": "ER Dr. Trésoret (Asyl; omV)",
    "bemerkung2": "",
    "parteienAktiv": [
      "Ammar GANAEM\r"
    ],
    "prozBevAktiv": [
      "Proz.-Bev.: Rechtsanwältin Shabana Khan\r"
    ],
    "parteienPassiv": [
      "Bundesrepublik Deutschland vertreten durch den Leiter des Bundesamtes für Migration und Flüchtlinge,,- Außenstelle Trier -\r"
    ],
    "prozBevPassiv": [],
    "parteienBeigeladen": [],
    "prozBevBeigeladen": [],
    "parteienZeugen": [],
    "parteienSv": [],
    "parteienAktivKurz": "Ammar GANAEM\r",
    "parteienPassivKurz": "Bundesrepublik Deutschland vertreten durch den Leiter des Bundesamtes für Migration und Flüchtlinge,,- Außenstelle Trier -\r",
    "stammdatenId": 4,
    "gericht": "Verwaltungsgericht Trier",
    "datum": "27.03.2017",
    "besetzung": [
      "Richter am Verwaltungsgericht Dr. Trésoret"
    ]
  },
  {
    "id": 58,
    "az": "1 K 6252/16.TR",
    "lfdnr": 7,
    "kammer": 1,
    "sitzungssaal": "Sitzungssaal III",
    "uhrzeitPlan": "omV",
    "uhrzeitAktuell": "omV",
    "status": "",
    "oeffentlich": "ja",
    "art": "",
    "gegenstand": "Flüchtlingsrechts",
    "bemerkung1": "ER Dr. Trésoret (Asyl; omV)",
    "bemerkung2": "",
    "parteienAktiv": [
      "1. Mohammad ALDANAF alias Mohammad Al DANAF\r",
      "2. Mervat AL MOGRABY\r",
      "3. Kindes Sara ALDANAF alias Sara Al DANAF, vertr. d. d. Eltern Mohammad ALDANAF und Mervat AL MOGRABY\r",
      "4. Kindes Abd Alkarim ALDANAF alias Abd Alkarim Al DANAF, vertr. d. d. Eltern Mohammad ALDANAF und Mervat AL MOGRABY\r"
    ],
    "prozBevAktiv": [
      "Proz.-Bev.: zu 1-4: Rechtsanwältin Shabana Khan\r"
    ],
    "parteienPassiv": [
      "Bundesrepublik Deutschland vertreten durch den Leiter des Bundesamtes für Migration und Flüchtlinge,,- Außenstelle Trier -\r"
    ],
    "prozBevPassiv": [],
    "parteienBeigeladen": [],
    "prozBevBeigeladen": [],
    "parteienZeugen": [],
    "parteienSv": [],
    "parteienAktivKurz": "1. Mohammad ALDANAF alias Mohammad Al DANAF\r u.a.",
    "parteienPassivKurz": "Bundesrepublik Deutschland vertreten durch den Leiter des Bundesamtes für Migration und Flüchtlinge,,- Außenstelle Trier -\r",
    "stammdatenId": 4,
    "gericht": "Verwaltungsgericht Trier",
    "datum": "27.03.2017",
    "besetzung": [
      "Richter am Verwaltungsgericht Dr. Trésoret"
    ]
  }
]

@Injectable()
export class TerminService {

  constructor() { }

  getTermine(display: Display): Promise<Termin[]> {
    return Promise.resolve(TERMINE);
  }
}
