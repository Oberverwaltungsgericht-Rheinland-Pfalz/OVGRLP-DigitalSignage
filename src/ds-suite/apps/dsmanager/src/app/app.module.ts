import { NgModule,LOCALE_ID } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { Routes, RouterModule } from "@angular/router";
import { FlexLayoutModule } from '@angular/flex-layout';

import { ClarityModule } from "@clr/angular";

import { registerLocaleData } from "@angular/common";
import localeDe from "@angular/common/locales/de";

import { NxModule } from '@nrwl/nx';

import { CoreModule } from '@ds-suite/core';
import { BackendModule } from "@ds-suite/backend";
import { DisplayStatusComponent } from "@ds-suite/backend";
import { DisplayDialogComponent } from '@ds-suite/backend';

import { AppComponent } from './app.component';
import { DsCommonModule } from './ds-common/ds-common.module';
import { DisplaysComponent } from './displays/displays.component';


registerLocaleData(localeDe);

const routes: Routes = [
  { path: "", component: DisplaysComponent }
];

@NgModule({
  declarations: [AppComponent, DisplaysComponent, DisplayStatusComponent, DisplayDialogComponent],
  imports: [
    BrowserModule, 
    BrowserAnimationsModule,
    FlexLayoutModule,
    NxModule.forRoot(),
    RouterModule.forRoot(routes, {
      useHash: true,
      initialNavigation: "enabled"
    }),
    ClarityModule,
    BackendModule,
    DsCommonModule,
    CoreModule
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
