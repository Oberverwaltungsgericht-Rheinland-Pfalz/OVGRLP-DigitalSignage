import { Injectable } from '@angular/core';

import { Observable } from 'rxjs/Observable';
import { Display, DisplayStatus } from '@ds-suite/model';

@Injectable()
export abstract class DisplayService {
  abstract getDisplays(): Observable<Display[]>;

  abstract getDisplay(name: string): Observable<Display>;

  abstract getDisplayStatus(display: Display): Observable<DisplayStatus>;
}
