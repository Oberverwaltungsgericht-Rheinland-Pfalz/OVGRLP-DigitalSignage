// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { Termin } from "@ds-suite/model";

export class TerminFunctions {

  public static AnzahlAktivparteien(termin: Termin) {
    return (termin.parteienAktiv.length);
  }

  public static AnzahlPassivparteien(termin: Termin) {
    return (termin.parteienPassiv.length);
  }

  public static IstPersonalvertretung(termin: Termin) {
    return termin.gericht === "Oberverwaltungsgericht Rheinland-Pfalz" &&
      (termin.kammer == 4 || termin.kammer == 5);
  }

  public static IstBerufgerichtlichesVerfahren(termin: Termin) {
    return termin.gericht === "Oberverwaltungsgericht Rheinland-Pfalz" &&
      (termin.az.includes('LBG-H'));
  }

  public static IstVghVerfahren(termin: Termin) {
    return termin.az.includes('VGH');
  }

  public static IstBeschlussverfahren(termin: Termin) {
    return termin.gericht === "Arbeitsgericht Koblenz" &&
      termin.az.includes(' BV ');
  }

  public static IstSenat(termin: Termin) {
    return (termin.gericht === "Oberverwaltungsgericht Rheinland-Pfalz" ||
    termin.gericht === "Verfassungsgerichtshof Rheinland-Pfalz");
  }

}