import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Config } from './config';

@Injectable()
export class ConfigService {
  private config: Config;

  constructor(private http: Http) { }

  load(url: string) {
    return new Promise((resolve) => {
      this.http.get(url).map(res => res.json())
        .subscribe(config => {
          this.config = config;
          resolve();
        });
    })
  }

  getConfig(): Config {
    return this.config;
  }
}
