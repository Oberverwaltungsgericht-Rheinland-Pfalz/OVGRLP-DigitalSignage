import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';

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

  constructor(private http: HttpClient, private configService: ConfigService) {
    this.apiUrl = this.configService.getConfig().apiUrl;
  }

  getDisplays(): Observable<Display[]> {
    return this.http.get<Display[]>(`${this.apiUrl}/settings/displays`);
  }

  getDisplay(name: string): Observable<Display> {
    return this.http.get<Display>(`${this.apiUrl}/settings/displays/${name}`);
  }

  getDisplayStatus(display: Display): Observable<DisplayStatus> {
    return this.http.get<DisplayStatus>(`${this.apiUrl}/settings/displays/${display.name}/status`);
  }

  private extractJson(res: Response) {
    let body = res.json();
    return body || {};
  }

  private extractDisplayStatus(res: HttpResponse<string>): DisplayStatus {
    let data = res.body;

    console.log(data);
    
    if(data === "0")
      return DisplayStatus.Offline;
    
    if(data === "1")
      return DisplayStatus.Online;
    
    if(data === "2")
      return DisplayStatus.Active;

    return DisplayStatus.Unknown;
  }

  /*
  private handleError(error: HttpResponse<any> | any) {
    let errMsg: string;
    if (error instanceof HttpResponse) {
      const body = error.ok .json() || '';
      const err = body.error || JSON.stringify(body);
      errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
    } else {
      errMsg = error.message ? error.message : error.toString();
    }
    console.error(errMsg);
    return Observable.throw(errMsg);
  }
  */
}