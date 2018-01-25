import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import { DisplayService } from './display.service';
import { Display, DisplayStatus } from '../model';

import { WEBAPI_URL } from '../main';

@Injectable()
export class DisplaySoapService implements DisplayService {

  constructor(private http: HttpClient) {
  }

  getDisplays(): Observable<Display[]> {
    return this.http.get<Display[]>(`${WEBAPI_URL}/settings/displays`);
  }

  getDisplay(name: string): Observable<Display> {
    return this.http.get<Display>(`${WEBAPI_URL}/settings/displays/${name}`);
  }

  getDisplayStatus(display: Display): Observable<DisplayStatus> {
    return this.http.get<DisplayStatus>(`${WEBAPI_URL}/settings/displays/${display.name}/status`);
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
}