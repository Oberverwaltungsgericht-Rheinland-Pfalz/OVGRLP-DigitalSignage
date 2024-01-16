// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2

export default interface Termin {
  id: number
  az: string
  lfdnr: number
  kammer: number
  sitzungssaal: string
  sitzungssaalNr: number
  uhrzeitPlan: string
  uhrzeitAktuell: string
  status: string
  oeffentlich: string
  art: string
  gegenstand: string
  bemerkung1: string
  bemerkung2: string
  parteienAktiv: string[]
  prozBevAktiv: string[]
  parteienPassiv: string[]
  prozBevPassiv: string[]
  parteienBeigeladen: string[]
  prozBevBeigeladen: string[]
  parteienZeugen: string[]
  parteienSv: string[]
  parteienAktivKurz: string
  parteienPassivKurz: string
  stammdatenId: number
  gericht: string
  datum: string
  besetzung: string[]
  parteienBeteiligt: string[]
  objekte: Objekt[]
}

export interface Objekt {
  objektId: number
  verfahrensId: number
  objektart: string
  gemarkung: string
  flur: string
  wirtschaftsart: string
  anschrift: string
  groesse: string
  objekt: string
  blatt: string
  grundbuchamt: string
  zusatz: string
  eigentumsart: string
  nutzungsrecht: string
  kurzname: string
  verkehrswert: number
  grundbuchbezirk: string
  eigentumsanteil: string
  schiffsregisterart: string
  schiffsname: string
  imonr: string
  registerGericht: string
}
