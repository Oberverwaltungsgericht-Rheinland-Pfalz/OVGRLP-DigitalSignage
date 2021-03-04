import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders } from '@angular/common/http';

import { AppConfig } from '@ds-suite/model';
import { ConfigService } from '@ds-suite/core';

const headers = new HttpHeaders()
  .set("Content-Type", "application/json");

@Injectable()
export class JsonConfigService implements ConfigService {
  private config: AppConfig;

  constructor(private http: HttpClient) { }

  load(url: string) {
    return new Promise((resolve) => {
      this.http.get<AppConfig>(url).subscribe(config => {
        this.config = config;
        resolve(null);
      });
    });
  }

  getConfig(): AppConfig {
    return this.config;
  }
}
