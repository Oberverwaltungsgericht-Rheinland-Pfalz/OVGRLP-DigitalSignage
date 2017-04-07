import { Injectable } from '@angular/core';
import { Termin } from './termin';
import { Display } from '../display/display';

const TERMINE: Termin[] = [
  { 
    "id": 41, 
    "az": "6 K 9376/16.TR", 
    "lfdnr": 1, 
    "kammer": 6, 
    "sitzungssaal": 
    "Sitzungssaal I", 
    "uhrzeitPlan": "09:00", 
    "uhrzeitAktuell": "09:20", 
    "status": "Abgeschlossen", 
    "oeffentlich": "ja", 
    "art": "mündliche Verhandlung", 
    "gegenstand": "Asylrechts", 
    "bemerkung1": "Dr. Schröter, BE, Afghanistan", 
    "bemerkung2": "", 
    "parteienAktiv": [ "Wakil Ahmad HAIDARI\r"], 
    "prozBevAktiv": ["Proz.-Bev.: Rechtsanwalt Siegbert Busse\r"], 
    "parteienPassiv": ["Bundesrepublik Deutschland vertreten durch den Leiter des Bundesamtes für Migration und Flüchtlinge,,- Außenstelle Trier -\r"], 
    "prozBevPassiv": [], 
    "parteienBeigeladen": [], 
    "prozBevBeigeladen": [], 
    "parteienZeugen": [], 
    "parteienSv": [], 
    "parteienAktivKurz": "Wakil Ahmad HAIDARI\r", 
    "parteienPassivKurz": "Bundesrepublik Deutschland vertreten durch den Leiter des Bundesamtes für Migration und Flüchtlinge,,- Außenstelle Trier -\r", 
    "stammdatenId": 4, 
    "gericht": "Verwaltungsgericht Trier", 
    "datum": "27.03.2017", 
    "besetzung": ["Richterin Dr. Schröter"] 
  }, { 
    "id": 45, 
    "az": "6 K 8656/16.TR", 
    "lfdnr": 2, 
    "kammer": 6, 
    "sitzungssaal": "Sitzungssaal I", 
    "uhrzeitPlan": "11:30", 
    "uhrzeitAktuell": "11:30", 
    "status": "Läuft", 
    "oeffentlich": "nein", 
    "art": "mündliche Verhandlung", 
    "gegenstand": "Flüchtlingsrechts", 
    "bemerkung1": "Dr. Schröter, BE, Afghanistan", 
    "bemerkung2": "", 
    "parteienAktiv": [
      "1. Mokhtar QHALANDARI\r", 
      "2. Adela QHALANDARI\r", 
      "3. Kindes Omid QHALANDARI vertreten durch die Eltern Mokhtar,QHALANDARI Adela,QHALANDARI\r", 
      "4. Kindes Shala QHALANDARI vertreten durch die Eltern Mokhtar, QHALANDARI Adela,QHALANDARI\r", 
      "5. Kindes Sonia QHALANDARI vertreten durch die Eltern Mokhtar,QHALANDARI Adela,QHALANDARI\r", 
      "6. Kindes Faria QHALANDARI vertreten durch die Eltern Mokhtar,QHALANDARI Adela,QHALANDARI\r", 
      "7. Kindes Naweed QHALANDARI vertreten durch die Eltern Mokhtar,QHALANDARI Adela,QHALANDARI\r"], 
    "prozBevAktiv": ["Proz.-Bev.: zu 1-7: Rechtsanwälte Kolter & Christoffer\r"], 
    "parteienPassiv": ["Bundesrepublik Deutschland vertreten durch den Leiter des Bundesamtes für Migration und Flüchtlinge,,- Außenstelle Trier -\r"], 
    "prozBevPassiv": [], 
    "parteienBeigeladen": [], 
    "prozBevBeigeladen": [], 
    "parteienZeugen": [], 
    "parteienSv": [], 
    "parteienAktivKurz": "1. Mokhtar QHALANDARI\r u.a.", 
    "parteienPassivKurz": "Bundesrepublik Deutschland vertreten durch den Leiter des Bundesamtes für Migration und Flüchtlinge,,- Außenstelle Trier -\r", 
    "stammdatenId": 4, 
    "gericht": "Verwaltungsgericht Trier", 
    "datum": "27.03.2017", 
    "besetzung": ["Richterin Dr. Schröter"]
  }, { 
    "id": 48, 
    "az": "6 K 8684/16.TR", 
    "lfdnr": 3, 
    "kammer": 6, 
    "sitzungssaal": "Sitzungssaal I", 
    "uhrzeitPlan": "13:00", 
    "uhrzeitAktuell": "13:00", 
    "status": "", 
    "oeffentlich": "ja", 
    "art": "mündliche Verhandlung", 
    "gegenstand": "Flüchtlingsrechts", 
    "bemerkung1": "Dr. Schröter, BE, Afghanistan", 
    "bemerkung2": "", 
    "parteienAktiv": [
      "1. Ghulam Mohsen MAHBOOB\r", 
      "2. Fatemah MAHBOOB\r", 
      "3. Kindes Mohammad Reza MAHBOOB vertreten durch die Eltern Ghulam Mohsen MAHBOOB und Fatemah MAHBOOB\r", 
      "4. Kindes Mohammad Taha MAHBOOB vertreten durch die Eltern Ghulam Mohsen MAHBOOB und Fatemah MAHBOOB\r"], 
    "prozBevAktiv": ["Proz.-Bev.: zu 1-4: Rechtsanwalt Daniel B. Araschmid\r"], 
    "parteienPassiv": ["Bundesrepublik Deutschland vertreten durch den Leiter des Bundesamtes für Migration und Flüchtlinge,,- Außenstelle Trier -\r"], 
    "prozBevPassiv": [], 
    "parteienBeigeladen": [], 
    "prozBevBeigeladen": [], 
    "parteienZeugen": [], 
    "parteienSv": [], 
    "parteienAktivKurz": "1. Ghulam Mohsen MAHBOOB\r u.a.", 
    "parteienPassivKurz": "Bundesrepublik Deutschland vertreten durch den Leiter des Bundesamtes für Migration und Flüchtlinge,,- Außenstelle Trier -\r", 
    "stammdatenId": 4, 
    "gericht": "Verwaltungsgericht Trier", 
    "datum": "27.03.2017", 
    "besetzung": ["Richterin Dr. Schröter"]
  }]

@Injectable()
export class TerminService {

  constructor() { }

  getTermine(displayName: string): Promise<Termin[]> {
    return Promise.resolve(TERMINE);
  }
}
