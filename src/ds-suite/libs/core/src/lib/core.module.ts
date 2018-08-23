import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { YesNoBooleanPipe } from './pipes/yes-no-boolean.pipe';
import { CapitalizePipe } from './pipes/capitalize.pipe';
import { SafeHtmlPipe } from './pipes/safe-html.pipe';

@NgModule({
  imports: [CommonModule],  
  exports: [
    CapitalizePipe,
    YesNoBooleanPipe,
    SafeHtmlPipe
  ],
  declarations: [
    CapitalizePipe,
    YesNoBooleanPipe,
    SafeHtmlPipe
  ],
})
export class CoreModule {}
