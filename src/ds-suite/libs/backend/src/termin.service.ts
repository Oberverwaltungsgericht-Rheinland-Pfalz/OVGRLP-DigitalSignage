import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';

import { Display, Termin } from '@ds-suite/model';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

const WEBAPI_URL = 'http://...';

@Injectable()
export class TerminService {
  constructor(private http: HttpClient) {}

  getTermine(displayName: string): Observable<Termin[]> {
    return this.http.get<Termin[]>(`${WEBAPI_URL}/settings/display/${displayName}/termine`);
  }
}
