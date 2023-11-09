// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { Injectable } from '@angular/core'
import { HttpClient, HttpResponse } from '@angular/common/http'

import { Observable } from 'rxjs/Observable'

import { ConfigService, PermissionService } from '@ds-suite/core'
import { BasicPermissions, AppConfig } from '@ds-suite/model'

@Injectable()
export class SoapPermissionService implements PermissionService {
  private readonly config: AppConfig

  constructor (private readonly http: HttpClient, private readonly configService: ConfigService) {
    this.config = configService.getConfig()
  }

  getBasicPermissions (): Observable<BasicPermissions> {
    return this.http.get<BasicPermissions>(`${this.config.webApiUrl}/settings/permissions/BasicPermissions`)
  }
}
