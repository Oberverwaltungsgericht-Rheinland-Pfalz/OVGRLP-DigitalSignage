import { NgModule } from '@angular/core';

import { DisplayService } from './service/display.service';
import { TerminService } from './service/termin.service';

@NgModule({
  providers: [
    DisplayService,
    TerminService]
})
export class DsCoreModule { }
