import { HttpClient } from '@angular/common/http';
import { TerminService } from './termin.service';
import { Termin } from '../model';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
export declare class TerminSoapService implements TerminService {
    private http;
    constructor(http: HttpClient);
    getTermine(displayName: string): Observable<Termin[]>;
}
