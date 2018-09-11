import { NgModule,LOCALE_ID } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { Routes, RouterModule } from "@angular/router";
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule } from '@angular/forms';

import { ClarityModule } from "@clr/angular";

import { registerLocaleData } from "@angular/common";
import localeDe from "@angular/common/locales/de";

import { NxModule } from '@nrwl/nx';

import { CoreModule } from '@ds-suite/core';
import { BackendModule } from "@ds-suite/backend";
import { UiModule } from '@ds-suite/ui';

import { AppComponent } from './app.component';
import { DsCommonModule } from './ds-common/ds-common.module';
import { DisplaysComponent } from './displays/displays.component';
import { TermineComponent } from './termine/termine.component';
import { TerminDialogComponent } from './termin-dialog/termin-dialog.component';
import { SondermeldungenComponent } from './sondermeldungen/sondermeldungen.component';


registerLocaleData(localeDe);

const routes: Routes = [
  { path: "", component: DisplaysComponent },
  { path: "termine", component: TermineComponent },
  { path: 'termine/:saal', component: TermineComponent },
  { path: "sondermeldungen", component: SondermeldungenComponent }
];

@NgModule({
  declarations: [AppComponent, DisplaysComponent, TermineComponent, TerminDialogComponent, SondermeldungenComponent],
  imports: [
    BrowserModule, 
    BrowserAnimationsModule,
    FormsModule,
    FlexLayoutModule,
    NxModule.forRoot(),
    RouterModule.forRoot(routes, {
      useHash: true,
      initialNavigation: "enabled"
    }),
    ClarityModule,
    BackendModule,
    DsCommonModule,
    CoreModule,
    UiModule
  ],
  providers: [
    {
      provide: LOCALE_ID,
      useValue: "de"
    }
  ],
  bootstrap: [AppComponent]  
})
export class AppModule {}
