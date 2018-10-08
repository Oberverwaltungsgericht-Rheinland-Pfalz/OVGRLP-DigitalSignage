import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import { ConfigService, PermissionService } from '@ds-suite/core';
import { BasicPermissions, AppConfig } from '@ds-suite/model';

@Injectable()
export class SoapPermissionService implements PermissionService {
  private config: AppConfig;

  constructor(private http: HttpClient, private configService: ConfigService) {
    this.config = configService.getConfig();
  }

  getBasicPermissions(): Observable<BasicPermissions> {
    return this.http.get<BasicPermissions>(`${this.config.webApiUrl}/settings/permissions/BasicPermissions`);
  }

}