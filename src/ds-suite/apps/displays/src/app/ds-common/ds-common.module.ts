import { APP_INITIALIZER, NgModule } from '@angular/core';
import { CapitalizePipe } from './capitalize.pipe';

import { ConfigService } from '@ds-suite/core';
import { JsonConfigService } from '@ds-suite/backend';

export function ConfigLoader(configService: JsonConfigService) {
  return () => configService.load('/assets/config.json');
}

@NgModule({
  imports: [],
  exports: [
    CapitalizePipe
  ],
  declarations: [
    CapitalizePipe
  ],
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
  ]
})
export class DsCommonModule { }