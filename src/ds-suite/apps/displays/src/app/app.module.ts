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
