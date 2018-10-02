import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ClarityModule } from "@clr/angular";
import { DisplayStatusComponent } from './components/display-status/display-status.component';
import { DisplayDialogComponent } from './components/display-dialog/display-dialog.component';
import { YesNoDialogComponent } from './components/yes-no-dialog/yes-no-dialog.component';
import { AlertComponent } from './components/alert/alert.component';
import { AlertService } from '@ds-suite/core';

@NgModule({
  imports: [
    CommonModule,
    ClarityModule,
    HttpClientModule
  ],
  declarations: [DisplayStatusComponent, DisplayDialogComponent, YesNoDialogComponent, AlertComponent],
  exports: [DisplayStatusComponent, DisplayDialogComponent, YesNoDialogComponent, AlertComponent],
  providers: [AlertService]
})
export class UiModule {}