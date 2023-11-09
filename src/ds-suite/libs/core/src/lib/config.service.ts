// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { Injectable } from '@angular/core';

import { AppConfig } from '@ds-suite/model';

@Injectable()
export abstract class ConfigService {
  abstract getConfig(): AppConfig;
}
