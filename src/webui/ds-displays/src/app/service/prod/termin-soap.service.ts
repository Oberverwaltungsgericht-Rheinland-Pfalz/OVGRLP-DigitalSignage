import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';

import { TerminService } from '../termin.service';
import { Termin } from '../../model/termin';
import { Display } from '../../model/display';
import { Config } from '../../model/config';
import { ConfigService } from '../config.service';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

@Injectable()
export class TerminSoapService implements TerminService {

  private apiUrl: string;

  constructor(private http: HttpClient, private configService: ConfigService) { 
    this.apiUrl = this.configService.getConfig().apiUrl;
  }

  getTermine(displayName: string): Observable<Termin[]> {

    return this.http
      .get<Termin[]>(`${this.apiUrl}/settings/displays/${displayName}/termine`);
  }
}