import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ClarityModule } from "@clr/angular";
import { DisplayStatusComponent } from './components/display-status/display-status.component';
import { DisplayDialogComponent } from './components/display-dialog/display-dialog.component';

@NgModule({
  imports: [
    CommonModule,
    ClarityModule,
    HttpClientModule
  ],
  declarations: [DisplayStatusComponent, DisplayDialogComponent],
  exports: [DisplayStatusComponent, DisplayDialogComponent],
  providers: []
})
export class UiModule {}