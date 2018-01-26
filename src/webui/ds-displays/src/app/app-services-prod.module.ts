import { NgModule } from '@angular/core';

import { DisplayService, DisplaySoapService } from 'ds-common';
import { TerminService, TerminSoapService } from 'ds-common';

@NgModule({
  declarations: [
  ],
  imports: [
  ],
  providers: [
    { provide: TerminService, useClass: TerminSoapService },
    { provide: DisplayService, useClass: DisplaySoapService }
  ]
})
export class AppServicesProdModule { }
