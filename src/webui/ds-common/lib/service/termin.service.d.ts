import { Observable } from 'rxjs/Observable';
import { Termin } from '../model/termin';
export declare abstract class TerminService {
    abstract getTermine(displayName: string): Observable<Termin[]>;
}
