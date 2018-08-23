import { NgModule, enableProdMode, LOCALE_ID } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { Routes, RouterModule } from "@angular/router";
import { FormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { registerLocaleData } from "@angular/common";
import localeDe from "@angular/common/locales/de";
import { NxModule } from "@nrwl/nx";
import { ClarityModule } from "@clr/angular";

import { BackendModule } from "@ds-suite/backend";

import { AppComponent } from "./app.component";
import { HomeComponent } from "./home/home.component";

import { DisplayComponent } from './display/display.component';
import { DisplayControlComponent } from './display-control/display-control.component';
import {DisplayDialogComponent } from './display-dialog/display-dialog.component';
import { TermineComponent } from './termine/termine.component';

import { DsCommonModule } from './ds-common/ds-common.module';
import { CoreModule } from '@ds-suite/core';

registerLocaleData(localeDe);

const routes: Routes = [
  { path: "", component: HomeComponent },
  { path: ":name", component: DisplayComponent },
  { path: ":name/:representation", component: DisplayComponent }
];

@NgModule({
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
    CoreModule
  ],
  providers: [
    {
      provide: LOCALE_ID,
      useValue: "de"
    }
  ],
  declarations: [AppComponent, HomeComponent, DisplayComponent, DisplayControlComponent, TermineComponent,DisplayDialogComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
