import { Injectable } from '@angular/core';

import { EntityManager } from 'breeze-client';
import { Observable } from 'rxjs/Observable';


@Injectable()
export abstract class NoteService {
  public breezeEntityManager: EntityManager;

  abstract getNotesByBreeze() : Promise<any>;
  abstract saveNotesByBreeze() : Promise<void>;
  abstract deleteNoteByBreeze(note: any) : Promise<void>;
}
