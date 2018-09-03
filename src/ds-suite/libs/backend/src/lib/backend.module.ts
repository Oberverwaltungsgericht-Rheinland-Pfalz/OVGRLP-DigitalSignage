import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { SoapDisplayService } from './services/soap-display.service';
import { SoapTerminService } from './services/soap-termin.service';
import { TerminService, DisplayService } from '@ds-suite/core';

@NgModule({
  imports: [CommonModule, HttpClientModule],
  providers: [
    { provide: TerminService, useClass: SoapTerminService },
    { provide: DisplayService, useClass: SoapDisplayService }
  ]
})
export class BackendModule {}