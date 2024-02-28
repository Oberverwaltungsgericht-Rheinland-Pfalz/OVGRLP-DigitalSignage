/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { Objekte } from './Objekte';
export type VerfahrenDto = {
    id?: number;
    az?: string | null;
    lfdnr?: number;
    kammer?: number;
    sitzungssaal?: string | null;
    sitzungssaalNr?: number | null;
    uhrzeitPlan?: string | null;
    uhrzeitAktuell?: string | null;
    status?: string | null;
    oeffentlich?: string | null;
    art?: string | null;
    gegenstand?: string | null;
    bemerkung1?: string | null;
    bemerkung2?: string | null;
    parteienAktiv?: Array<string> | null;
    prozBevAktiv?: Array<string> | null;
    parteienPassiv?: Array<string> | null;
    prozBevPassiv?: Array<string> | null;
    parteienBeigeladen?: Array<string> | null;
    prozBevBeigeladen?: Array<string> | null;
    parteienZeugen?: Array<string> | null;
    parteienSv?: Array<string> | null;
    parteienAktivKurz?: string | null;
    parteienPassivKurz?: string | null;
    stammdatenId?: number;
    gericht?: string | null;
    datum?: string | null;
    besetzung?: Array<string> | null;
    parteienBeteiligt?: Array<string> | null;
    objekte?: Array<Objekte> | null;
};

