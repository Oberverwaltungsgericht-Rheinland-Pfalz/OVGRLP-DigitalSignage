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
export class AppModule { }
