import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import { Config } from '../model/config';
import { ConfigService } from '../service/config.service';
import { Display } from '../model/display';
import { DisplayStatus } from '../model/displayStatus';

@Injectable()
export class DisplayService {

  private apiUrl: string;

  constructor(private http: Http, private configService: ConfigService) {
    this.apiUrl = this.configService.getConfig().apiUrl;
  }

  getDisplays(): Observable<Display[]> {
    return this.http
      .get(`${this.apiUrl}/settings/displays`)
      .map(this.extractJson)
      .catch(this.handleError);
  }

  getDisplay(name: string): Observable<Display> {
    return this.http
      .get(`${this.apiUrl}/settings/displays/${name}`)
      .map(this.extractJson)
      .catch(this.handleError);
  }

  getDisplayStatus(display: Display): Observable<DisplayStatus> {
    return this.http
      .get(`${this.apiUrl}/settings/displays/${display.name}/status`)
      .map(this.extractDisplayStatus)
      .catch(this.handleError);
  }

  private extractJson(res: Response) {
    let body = res.json();
    return body || {};
  }

  private extractDisplayStatus(res: Response): DisplayStatus {
    let data = res.text();

    console.log(data);
    
    if(data === "0")
      return DisplayStatus.Offline;
    
    if(data === "1")
      return DisplayStatus.Online;
    
    if(data === "2")
      return DisplayStatus.Active;

    return DisplayStatus.Unknown;
  }

  private handleError(error: Response | any) {
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