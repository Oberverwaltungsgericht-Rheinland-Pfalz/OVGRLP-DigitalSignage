import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';

import { Termin } from '../model/termin';
import { Display } from '../model/display';
import { Config } from '../model/config';
import { ConfigService } from '../service/config.service';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

@Injectable()
export class TerminService {

  private apiUrl: string;

  constructor(private http: Http, private configService: ConfigService) { 
    this.apiUrl = this.configService.getConfig().apiUrl;
  }

  getTermine(displayName: string): Observable<Termin[]> {

    return this.http
      .get(`${this.apiUrl}/settings/displays/${displayName}/termine`)
      .map(this.extractData)
      .catch(this.handleError);
  }

  private extractData(res: Response) {
    let body = res.json();
    return body || { };
  }

  private handleError (error: Response | any) {
    let errMsg: string;
    if (error instanceof Response) {
      const body = error.json() || '';
      const err = body.error || JSON.stringify(body);
      errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
    } else {
      errMsg = error.message ? error.message : error.toString();
    }
    console.error(errMsg);
    return Observable.throw(errMsg);
  }
}
