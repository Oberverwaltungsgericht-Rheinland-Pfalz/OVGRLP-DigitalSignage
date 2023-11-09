// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { APP_INITIALIZER, NgModule } from '@angular/core'

import { HTTP_INTERCEPTORS, HttpInterceptor } from '@angular/common/http'
import { CredentialsInterceptor, DefaultInterceptor, ConfigService, AlertService } from '@ds-suite/core'

import { JsonConfigService } from '@ds-suite/backend'

export function ConfigLoader (configService: JsonConfigService) {
  return async () => await configService.load('./assets/config.json')
}

export function InterceptorLoader (configService: JsonConfigService, alertService: AlertService): HttpInterceptor {
  /*
    In dieser Factory ist der ConfigLoader (welcher im APP_INITIALIZER genutzt wird) noch nicht fertig,
    sodass hier configServicegetConfig() immer undefined ist
    da Angular bis jetzt keine asynchronen provider unterstützt - siehe auch Angular Issue #23279 - muss es
    erst einmal so gelöst werden, dass im CredentialsInterceptor die Settings geprüft werden
    //!\TODO: Prüfen ob es evtl. mittlerweile eine Möglichkeit gibt!!
  */
  // return new DefaultInterceptor();
  return new CredentialsInterceptor(configService, alertService)
}

@NgModule({
  imports: [],
  exports: [],
  declarations: [],
  providers: [
    JsonConfigService,
    {
      provide: APP_INITIALIZER,
      useFactory: ConfigLoader,
      deps: [JsonConfigService],
      multi: true
    },
    {
      provide: ConfigService,
      useExisting: JsonConfigService
    },
    {
      provide: HTTP_INTERCEPTORS,
      useFactory: InterceptorLoader,
      deps: [JsonConfigService, AlertService],
      multi: true
    }
  ]
})
export class DsCommonModule { }
