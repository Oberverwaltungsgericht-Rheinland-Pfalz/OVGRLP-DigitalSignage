import { Injectable } from '@angular/core';

import { Observable } from 'rxjs/Observable';
import { Stammdaten } from '@ds-suite/model';

@Injectable()
export abstract class StammdatenService {
  abstract getStammdaten(): Observable<Stammdaten[]>;
}