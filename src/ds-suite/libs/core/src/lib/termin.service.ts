import { Injectable } from '@angular/core';

import { Observable } from 'rxjs/Observable';

import { Termin } from '@ds-suite/model';

@Injectable()
export abstract class TerminService {
  abstract getTermine(displayName: string): Observable<Termin[]>;
  abstract saveTermin(termin: Termin): Observable<Termin>;
}
