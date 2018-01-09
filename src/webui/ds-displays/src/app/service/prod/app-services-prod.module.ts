import { NgModule } from '@angular/core';

import { DisplayService } from '../display.service';
import { DisplaySoapService } from './display-soap.service';
import { TerminService } from '../termin.service';
import { TerminSoapService } from './termin-soap.service';


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
