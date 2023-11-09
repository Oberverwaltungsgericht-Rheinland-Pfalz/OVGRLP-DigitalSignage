// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { NgModule, LOCALE_ID } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FlexLayoutModule } from "@angular/flex-layout";
import { registerLocaleData } from "@angular/common";
import localeDe from "@angular/common/locales/de";

import { NxModule } from '@nrwl/nx';

import { BackendModule } from '@ds-suite/backend';

import { AppComponent } from './app.component';
import { DisplayTemplateComponent } from './display-template/display-template.component';
import { AppRoutingModule, routingComponents } from './app.routing';
import { DsCommonModule } from './ds-common/ds-common.module';
import { CoreModule } from '@ds-suite/core';
import { UiModule } from '@ds-suite/ui';

registerLocaleData(localeDe);

@NgModule({
  declarations: [
    AppComponent,
    DisplayTemplateComponent,
    routingComponents
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FlexLayoutModule,
    NxModule.forRoot(),
    AppRoutingModule,
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
export class AppModule { }
