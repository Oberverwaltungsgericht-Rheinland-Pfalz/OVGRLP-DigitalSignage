import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

import { DisplayService } from './display.service';
import { TerminService } from './termin.service';

@NgModule({
  imports: [
    CommonModule, 
    HttpClientModule
  ],
  providers: [
    DisplayService,
    TerminService
  ]
})
export class BackendModule {}
