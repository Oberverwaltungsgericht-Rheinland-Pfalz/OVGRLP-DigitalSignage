import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DummyDisplayService } from './dummy-display.service';
import { DummyTerminService } from './dummy-termin.service';
import { TerminService, DisplayService } from '@ds-suite/core';

@NgModule({
  imports: [CommonModule],
  providers: [
    { provide: TerminService, useClass: DummyTerminService },
    { provide: DisplayService, useClass: DummyDisplayService }
  ]
})
export class BackendDevModule { }
