import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import { map } from 'rxjs/operators';
import { DisplayService, ConfigService } from '@ds-suite/core';
import { Display, DisplayDto, DisplayStatus, AppConfig, Note } from '@ds-suite/model';

@Injectable()
export class SoapDisplayService implements DisplayService {
  private config: AppConfig;

  constructor(private http: HttpClient, private configService: ConfigService) {
    this.config = configService.getConfig();
  }

  getDisplays(): Observable<Display[]> {
    return this.http.get<Display[]>(`${this.config.webApiUrl}/settings/displays`);
  }

  getDisplaysDto(): Observable<DisplayDto[]> {
    return this.http.get<DisplayDto[]>(`${this.config.webApiUrl}/settings/displays/DisplaysEx`);
  }

  getDisplay(name: string): Observable<Display> {
    return this.http.get<Display>(`${this.config.webApiUrl}/settings/displays/${name}`).pipe(map(resp => {
      if (Array.isArray(resp)) {
        return resp[0];
      } else {
        return resp;
      }
    }));
  }

  getDisplayDto(name: string): Observable<DisplayDto> {
    return this.http.get<DisplayDto>(`${this.config.webApiUrl}/settings/displays/${name}/DisplayEx`).pipe(map(resp => {
      if (Array.isArray(resp)) {return resp[0];}
      else { return resp; }
    }));
  }

  getDisplayNotes(name: string): Observable<Note[]> {
    const urlString = window.location.href.replace('#','')
    const url = new URL(urlString)
    const timestamp = url.searchParams.get("timestamp") || new Date().toISOString()

    return this.http.get<Note[]>(`${this.config.webApiUrl}/settings/displays/${name}/activenotes?timestamp=${timestamp}`);
  }

  getDisplayStatus(display: Display): Observable<DisplayStatus> {
    return this.http.get<DisplayStatus>(`${this.config.webApiUrl}/settings/displays/${display.name}/status`);
  }

  getScreenshotUrl(display: Display): Observable<string> {
    return this.http.get<string>(`${this.config.webApiUrl}/settings/displays/${display.name}/ScreenshotUrl`);
  }

  startDisplay(display: Display): Observable<void> {
    console.log("wird gestaretet;",display)
    return this.http.get<void>(`${this.config.webApiUrl}/settings/displays/${display.name}/start`);
  }

  stopDisplay(display: Display): Observable<void> {
    return this.http.get<void>(`${this.config.webApiUrl}/settings/displays/${display.name}/stop`);
  }

  restartDisplay(display: Display): Observable<void> {
    return this.http.get<void>(`${this.config.webApiUrl}/settings/displays/${display.name}/restart`);
  }

}
