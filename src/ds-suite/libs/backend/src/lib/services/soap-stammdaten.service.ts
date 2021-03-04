import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';

import { StammdatenService, ConfigService } from '@ds-suite/core';
import { Stammdaten, AppConfig } from '@ds-suite/model';

@Injectable()
export class SoapStammdatenService implements StammdatenService {
  private config: AppConfig;

  constructor(private http: HttpClient, private configService: ConfigService) {
    this.config = configService.getConfig();
  }

  getStammdaten(): Observable<Stammdaten[]> {
    return this.http.get<Stammdaten[]>(`${this.config.webApiUrl}/daten/stammdaten`);
  }

}
