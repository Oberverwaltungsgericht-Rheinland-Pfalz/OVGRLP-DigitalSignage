import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { EntityManager, EntityQuery } from 'breeze-client';

import { AppConfig } from '@ds-suite/model';
import { NoteService, ConfigService } from '@ds-suite/core';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

const headers = new HttpHeaders()
  .set("Content-Type", "application/json");

@Injectable()
export class SoapNoteService implements NoteService {
  private config: AppConfig;
  public breezeEntityManager: EntityManager;

  constructor(private http: HttpClient, private configService: ConfigService) {
    this.config = this.configService.getConfig();
    var BreezeserviceName = this.config.webApiUrl + '/breeze/EurekaDaten'
    this.breezeEntityManager = new EntityManager(BreezeserviceName);
    }

getNotesByBreeze(): Promise<any> {
    var query = EntityQuery
        .from('Notes')
        .expand('NotesAssignments,NotesAssignments.Display')
        .orderBy('Id desc');

    var promise = this.breezeEntityManager.executeQuery(query)
        .then(res => { 
        return res.results;
        })
        .catch((error) => {
            console.log(error);
            return Promise.reject(error);
        });

    return promise;
    }

saveNotesByBreeze(): Promise<void> {
    return this.breezeEntityManager.saveChanges()
        .then(res => { 
            console.log("Daten wurden gespeichert:",res);
        })
        .catch((err) => {
            console.error("Fehler beim Speichern der Sondermeldungen:",err);
        });
    }

deleteNoteByBreeze(note: any): Promise<void> {
    note.entityAspect.setDeleted();
    return this.breezeEntityManager.saveChanges()
        .then(res => { 
            console.log("Sondermeldung wurde gelöscht:",res);
        })
        .catch((err) => {
            console.error("Fehler beim Löschen einer Sondermeldung:",err);
        });
    }

}
