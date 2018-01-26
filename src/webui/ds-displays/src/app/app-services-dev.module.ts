import { NgModule } from '@angular/core';

import { DisplayService, DisplayDummyService } from 'ds-common';
import { TerminService, TerminDummyService } from 'ds-common';

@NgModule({
  declarations: [
  ],
  imports: [
  ],
  providers: [
    { provide: TerminService, useClass: TerminDummyService },
    { provide: DisplayService, useClass: DisplayDummyService }
  ]
})
export class AppServicesDevModule { }
