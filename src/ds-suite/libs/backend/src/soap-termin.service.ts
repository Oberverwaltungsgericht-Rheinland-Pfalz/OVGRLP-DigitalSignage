import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';

import { Display, Termin, AppConfig } from '@ds-suite/model';
import { TerminService } from '@ds-suite/core';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

@Injectable()
export class SoapTerminService implements TerminService {
  constructor(private http: HttpClient, private config: AppConfig) {}

  getTermine(displayName: string): Observable<Termin[]> {
    return this.http.get<Termin[]>(`${this.config.webApiUrl}/settings/displays/${displayName}/termine`);
  }
}
