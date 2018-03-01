import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import { Display, DisplayStatus, AppConfig } from '@ds-suite/model';

@Injectable()
export class DisplayService {
  constructor(private http: HttpClient, private config: AppConfig) {}

  getDisplays(): Observable<Display[]> {
    return this.http.get<Display[]>(`${this.config.webApiUrl}/settings/displays`);
  }

  getDisplay(name: string): Observable<Display> {
    return this.http.get<Display>(`${this.config.webApiUrl}/settings/displays/${name}`);
  }

  getDisplayStatus(display: Display): Observable<DisplayStatus> {
    return this.http.get<DisplayStatus>(`${this.config.webApiUrl}/settings/displays/${display.name}/status`);
  }
}
