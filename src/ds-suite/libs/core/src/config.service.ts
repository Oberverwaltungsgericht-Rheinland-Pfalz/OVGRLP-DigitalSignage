import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { AppConfig } from '@ds-suite/model';

@Injectable()
export abstract class ConfigService {
  abstract getConfig(): AppConfig;
}
