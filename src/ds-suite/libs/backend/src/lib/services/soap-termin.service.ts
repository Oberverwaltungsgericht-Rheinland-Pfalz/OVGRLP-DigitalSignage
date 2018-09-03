import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders } from '@angular/common/http';

import { Display, Termin, AppConfig } from '@ds-suite/model';
import { TerminService, ConfigService } from '@ds-suite/core';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

const headers = new HttpHeaders()
  .set("Content-Type", "application/json");

@Injectable()
export class SoapTerminService implements TerminService {
  private config: AppConfig;

  constructor(private http: HttpClient, private configService: ConfigService) {
    this.config = this.configService.getConfig();
  }

  getTermine(displayName: string): Observable<Termin[]> {
    return this.http.get<Termin[]>(`${this.config.webApiUrl}/settings/displays/${displayName}/termine`);
  }

  saveTermin(termin: Termin): Observable<Termin> {
    return this.http.put<Termin>(`${this.config.webApiUrl}/daten/verfahren/${termin.id}`, termin, { headers })
  }
}
