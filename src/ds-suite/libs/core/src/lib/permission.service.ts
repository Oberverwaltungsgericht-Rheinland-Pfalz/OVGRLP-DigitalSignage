// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { Injectable } from '@angular/core'

import { Observable } from 'rxjs/Observable'
import { BasicPermissions } from '@ds-suite/model'

@Injectable()
export abstract class PermissionService {
  abstract getBasicPermissions (): Observable<BasicPermissions>
}
