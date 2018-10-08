import { Injectable } from '@angular/core';

import { Observable } from 'rxjs/Observable';
import { BasicPermissions } from '@ds-suite/model';

@Injectable()
export abstract class PermissionService {

  abstract getBasicPermissions() : Observable<BasicPermissions>;
}