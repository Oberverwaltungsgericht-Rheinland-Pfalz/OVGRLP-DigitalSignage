import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { BreezeBridgeHttpClientModule } from 'breeze-bridge2-angular';

import { SoapDisplayService } from './services/soap-display.service';
import { SoapNoteService } from './services/soap-note.service';
import { SoapTerminService } from './services/soap-termin.service';
import { SoapPermissionService } from './services/soap-permission.service';
import { TerminService, DisplayService, NoteService, PermissionService } from '@ds-suite/core';

@NgModule({
  imports: [CommonModule, BreezeBridgeHttpClientModule, HttpClientModule],
  providers: [
    { provide: TerminService, useClass: SoapTerminService },
    { provide: DisplayService, useClass: SoapDisplayService },
    { provide: NoteService, useClass: SoapNoteService },
    { provide: PermissionService, useClass: SoapPermissionService }
  ]
})
export class BackendModule {}