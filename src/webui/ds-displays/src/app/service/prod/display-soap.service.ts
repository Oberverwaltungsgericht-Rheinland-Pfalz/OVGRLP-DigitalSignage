import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import { DisplayService } from '../display.service';
import { Config } from '../../model/config';
import { ConfigService } from '../config.service';
import { Display } from '../../model/display';
import { DisplayStatus } from '../../model/displayStatus';

@Injectable()
export class DisplaySoapService implements DisplayService {

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
}