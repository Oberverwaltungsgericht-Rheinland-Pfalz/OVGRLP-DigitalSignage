import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Config } from '../model/config';

@Injectable()
export class ConfigService {
  private config: Config;

  constructor(private http: HttpClient) { }

  load(url: string) {
    return new Promise((resolve) => {
      this.http.get<Config>(url).subscribe(config => {
        this.config = config;
        resolve();
      });
    })
  }

  getConfig(): Config {
    return this.config;
  }
}
