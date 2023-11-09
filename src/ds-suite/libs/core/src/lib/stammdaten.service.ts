// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs/Observable';
import { Stammdaten } from '@ds-suite/model';

@Injectable()
export abstract class StammdatenService {
  abstract getStammdaten(): Observable<Stammdaten[]>;
}