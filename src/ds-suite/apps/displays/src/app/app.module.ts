import { NgModule, enableProdMode, LOCALE_ID } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { FlexLayoutModule } from "@angular/flex-layout";
import { registerLocaleData } from "@angular/common";
import localeDe from "@angular/common/locales/de";

import { NxModule } from "@nrwl/nx";

import { BackendModule } from "@ds-suite/backend";

import { AppComponent } from "./app.component";
import { AppRoutingModule, routingComponents } from './app.routing';
import { DsCommonModule } from './ds-common/ds-common.module';

import { AppConfig } from "@ds-suite/model";
import { DS_DISPLAYS_CONFIG } from "./app.config";

import { environment } from "../environments/environment";

registerLocaleData(localeDe);

@NgModule({
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FlexLayoutModule,
    NxModule.forRoot(),
    AppRoutingModule,
    BackendModule,
    DsCommonModule
  ],
  providers: [
    {
      provide: AppConfig,
      useValue: DS_DISPLAYS_CONFIG
    },
    {
      provide: LOCALE_ID,
      useValue: "de"
    }
  ],
  declarations: [
    AppComponent,
    routingComponents
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
