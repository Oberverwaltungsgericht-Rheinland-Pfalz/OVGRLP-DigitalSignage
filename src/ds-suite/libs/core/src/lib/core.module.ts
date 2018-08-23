import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { YesNoBooleanPipe } from './pipes/yes-no-boolean.pipe';
@NgModule({
  imports: [CommonModule],  
  exports: [
    YesNoBooleanPipe
  ],
  declarations: [
    YesNoBooleanPipe
  ],
})
export class CoreModule {}
