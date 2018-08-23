import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { YesNoBooleanPipe } from './pipes/yes-no-boolean.pipe';
import { CapitalizePipe } from './pipes/capitalize.pipe';

@NgModule({
  imports: [CommonModule],  
  exports: [
    CapitalizePipe,
    YesNoBooleanPipe
  ],
  declarations: [
    CapitalizePipe,
    YesNoBooleanPipe
  ],
})
export class CoreModule {}
