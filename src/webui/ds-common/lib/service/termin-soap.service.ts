import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';

import { TerminService } from './termin.service';
import { Display, Termin } from '../model';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import { WEBAPI_URL } from '../main';

@Injectable()
export class TerminSoapService implements TerminService {

  constructor(private http: HttpClient) { 
  }

  getTermine(displayName: string): Observable<Termin[]> {

    return this.http
      .get<Termin[]>(`${WEBAPI_URL}/settings/displays/${displayName}/termine`);
  }
}
