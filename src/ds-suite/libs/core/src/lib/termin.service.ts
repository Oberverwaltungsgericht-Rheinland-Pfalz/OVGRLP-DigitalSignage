// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { Injectable } from '@angular/core'

import { EntityManager } from 'breeze-client'
import { Observable } from 'rxjs/Observable'

import { Termin } from '@ds-suite/model'

@Injectable()
export abstract class TerminService {
  public breezeEntityManager: EntityManager

  abstract getAllTermine (): Observable<Termin[]>
  abstract getTermine (displayName: string): Observable<Termin[]>
  abstract saveTermin (termin: Termin): Observable<Termin>
  abstract getTerminByBreeze (id: number): Promise<any>
  abstract saveTerminByBreeze (termin: any): Promise<void>
  abstract deleteTerminByBreeze (termin: any): Promise<void>
}
