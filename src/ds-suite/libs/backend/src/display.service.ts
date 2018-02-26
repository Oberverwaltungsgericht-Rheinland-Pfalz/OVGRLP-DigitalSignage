import { Injectable } from "@angular/core";
import { HttpClient, HttpResponse } from "@angular/common/http";

import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/catch";
import "rxjs/add/operator/map";

import { Display, DisplayStatus } from "@ds-suite/model";

const WEBAPI_URL = "http://...";

@Injectable()
export class DisplayService {
  constructor(private http: HttpClient) {}

  getDisplays(): Observable<Display[]> {
    return this.http.get<Display[]>(`${WEBAPI_URL}/settings/displays`);
  }

  getDisplay(name: string): Observable<Display> {
    return this.http.get<Display>(`${WEBAPI_URL}/settings/displays/${name}`);
  }

  getDisplayStatus(display: Display): Observable<DisplayStatus> {
    return this.http.get<DisplayStatus>(`${WEBAPI_URL}/settings/displays/${display.name}/status`);
  }
}
