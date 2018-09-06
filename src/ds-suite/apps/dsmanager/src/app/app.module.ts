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
import { UiModule } from '@ds-suite/ui';

import { AppComponent } from './app.component';
import { DsCommonModule } from './ds-common/ds-common.module';
import { DisplaysComponent } from './displays/displays.component';
import { TermineComponent } from './termine/termine.component';


registerLocaleData(localeDe);

const routes: Routes = [
  { path: "", component: DisplaysComponent },
  { path: "termine", component: TermineComponent },
  { path: 'termine/:saal', component: TermineComponent }
];

@NgModule({
  declarations: [AppComponent, DisplaysComponent, TermineComponent],
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
