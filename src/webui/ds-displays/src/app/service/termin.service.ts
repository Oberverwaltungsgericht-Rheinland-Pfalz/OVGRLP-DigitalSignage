import { Injectable } from '@angular/core';

import { Observable } from 'rxjs/Observable';
import { Termin } from '../model/termin';

@Injectable()
export abstract class TerminService {
  abstract getTermine(displayName: string): Observable<Termin[]>;
}