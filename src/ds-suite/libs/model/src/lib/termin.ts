import { Objekt } from './objekt';

export class Termin {
  id: number;
  az: string;
  lfdnr: number;
  kammer: number;
  sitzungssaal: string;
  sitzungssaalNr: number;
  uhrzeitPlan: string;
  uhrzeitAktuell: string;
  status: string;
  oeffentlich: string;
  art: string;
  gegenstand: string;
  bemerkung1: string;
  bemerkung2: string;
  parteienAktiv: string[];
  prozBevAktiv: string[];
  parteienPassiv: string[];
  prozBevPassiv: string[];
  parteienBeigeladen: string[];
  prozBevBeigeladen: string[];
  parteienZeugen: string[];
  parteienSv: string[];
  parteienAktivKurz: string;
  parteienPassivKurz: string;
  stammdatenId: number;
  gericht: string;
  datum: string;
  besetzung: string[];
  parteienBeteiligt: string[];
  objekte: Objekt[];
}
