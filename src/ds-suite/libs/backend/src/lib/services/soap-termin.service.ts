// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { Injectable } from '@angular/core'
import { HttpClient, HttpResponse, HttpHeaders } from '@angular/common/http'
import { EntityManager, EntityQuery } from 'breeze-client'

import { Termin, AppConfig } from '@ds-suite/model'
import { TerminService, ConfigService } from '@ds-suite/core'

import { Observable } from 'rxjs/Observable'

const headers = new HttpHeaders()
  .set('Content-Type', 'application/json')

@Injectable()
export class SoapTerminService implements TerminService {
  private readonly config: AppConfig
  public breezeEntityManager: EntityManager

  constructor (private readonly http: HttpClient, private readonly configService: ConfigService) {
    this.config = this.configService.getConfig()
    const BreezeserviceName = this.config.webApiUrl + '/breeze/EurekaDaten'
    this.breezeEntityManager = new EntityManager(BreezeserviceName)
  }

  getAllTermine (): Observable<Termin[]> {
    return this.http.get<Termin[]>(`${this.config.webApiUrl}/daten/verfahren`)
  }

  getTermine (displayName: string): Observable<Termin[]> {
    return this.http.get<Termin[]>(`${this.config.webApiUrl}/settings/displays/${displayName}/termine`)
  }

  saveTermin (termin: Termin): Observable<Termin> {
    return this.http.put<Termin>(`${this.config.webApiUrl}/daten/verfahren/${termin.id}`, termin, { headers })
  }

  async getTerminByBreeze (id: number): Promise<any> {
    const query = EntityQuery
      .from('Verfahren')
      .where('VerfahrensId', '==', id)
      .expand('Stammdaten, Besetzung, ParteienAktiv, ProzBevAktiv, ParteienPassiv, ProzBevPassiv, ParteienBeigeladen, ProzBevBeigeladen, ParteienZeugen, ParteienSV, ParteienBeteiligt, Objekte')

    const promise = this.breezeEntityManager.executeQuery(query)
      .then(res => {
        return res.results[0] //! \TODO: geht bestimmt besser
      })
      .catch(async (error) => {
        console.log(error)
        return await Promise.reject(error)
      })

    return promise
  }

  async saveTerminByBreeze (termin: any): Promise<void> {
    return this.breezeEntityManager.saveChanges()
      .then(res => {
        console.log('Daten wurden gespeichert:', res)
      })
      .catch((err) => {
        console.error('Fehler beim Speichern eines Termins:', err)
      })
  }

  async deleteTerminByBreeze (termin: any): Promise<void> {
    termin.entityAspect.setDeleted()
    return this.breezeEntityManager.saveChanges()
      .then(res => {
        console.log('Termin wurde gelöscht:', res)
      })
      .catch((err) => {
        console.error('Fehler beim Löschen eines Termins:', err)
      })
  }
}
