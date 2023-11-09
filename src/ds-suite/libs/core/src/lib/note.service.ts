// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { Injectable } from '@angular/core'

import { EntityManager } from 'breeze-client'
import { Observable } from 'rxjs/Observable'

@Injectable()
export abstract class NoteService {
  public breezeEntityManager: EntityManager

  abstract getNotesByBreeze (): Promise<any>
  abstract saveNotesByBreeze (): Promise<void>
  abstract deleteNoteByBreeze (note: any): Promise<void>
}
