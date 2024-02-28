/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { Besetzung } from './Besetzung';
import type { Objekte } from './Objekte';
import type { ParteienAktiv } from './ParteienAktiv';
import type { ParteienBeigeladen } from './ParteienBeigeladen';
import type { ParteienBeteiligt } from './ParteienBeteiligt';
import type { ParteienPassiv } from './ParteienPassiv';
import type { ParteienSV } from './ParteienSV';
import type { ParteienZeugen } from './ParteienZeugen';
import type { ProzBevAktiv } from './ProzBevAktiv';
import type { ProzBevBeigeladen } from './ProzBevBeigeladen';
import type { ProzBevPassiv } from './ProzBevPassiv';
import type { Stammdaten } from './Stammdaten';
export type Verfahren = {
    verfahrensId?: number;
    stammdatenId?: number;
    stammdaten?: Stammdaten;
    lfdnr?: number;
    kammer?: number;
    sitzungssaal?: string | null;
    sitzungssaalNr?: number | null;
    uhrzeitPlan?: string | null;
    uhrzeitAktuell?: string | null;
    status?: string | null;
    oeffentlich?: string | null;
    az?: string | null;
    gegenstand?: string | null;
    bemerkung1?: string | null;
    bemerkung2?: string | null;
    art?: string | null;
    besetzung?: Array<Besetzung> | null;
    parteienAktiv?: Array<ParteienAktiv> | null;
    parteienPassiv?: Array<ParteienPassiv> | null;
    parteienBeigeladen?: Array<ParteienBeigeladen> | null;
    parteienSV?: Array<ParteienSV> | null;
    parteienZeugen?: Array<ParteienZeugen> | null;
    prozBevAktiv?: Array<ProzBevAktiv> | null;
    prozBevBeigeladen?: Array<ProzBevBeigeladen> | null;
    prozBevPassiv?: Array<ProzBevPassiv> | null;
    parteienBeteiligt?: Array<ParteienBeteiligt> | null;
    objekte?: Array<Objekte> | null;
};

