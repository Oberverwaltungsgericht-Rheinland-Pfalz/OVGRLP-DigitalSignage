import { APP_INITIALIZER, NgModule } from '@angular/core';

import { ConfigService } from '@ds-suite/core';
import { JsonConfigService } from '@ds-suite/backend';

import { HTTP_INTERCEPTORS, HttpInterceptor } from '@angular/common/http';
import { CredentialsInterceptor,DefaultInterceptor } from '@ds-suite/core';

export function ConfigLoader(configService: JsonConfigService) {
  return () => configService.load('./assets/config.json');
}

export function InterceptorLoader(configService: JsonConfigService): HttpInterceptor {
  //return new DefaultInterceptor();
  return new CredentialsInterceptor();     //configService.getConfig().webApiUrl;

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
      deps: [JsonConfigService],
      multi: true
    }
  ]
})
export class DsCommonModule { }