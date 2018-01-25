import { NgModule } from '@angular/core';

import { DisplayService } from '../display.service';
import { DisplayDummyService } from './display-dummy.service';
import { TerminService } from '../termin.service';
import { TerminDummyService } from './termin-dummy.service';


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
