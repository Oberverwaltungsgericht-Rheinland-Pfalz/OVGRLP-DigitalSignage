import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/catch";
import "rxjs/add/operator/map";
import { Termin } from "../model";
import { TerminService } from "./termin.service";
export declare class TerminDummyService implements TerminService {
    private apiUrl;
    constructor();
    getTermine(displayName: string): Observable<Termin[]>;
}
