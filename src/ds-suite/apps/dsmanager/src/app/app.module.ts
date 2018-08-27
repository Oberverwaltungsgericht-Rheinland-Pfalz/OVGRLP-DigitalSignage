import { NgModule,LOCALE_ID } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { Routes, RouterModule } from "@angular/router";

import { ClarityModule } from "@clr/angular";

import { registerLocaleData } from "@angular/common";
import localeDe from "@angular/common/locales/de";

import { NxModule } from '@nrwl/nx';

import { BackendModule } from "@ds-suite/backend";
import { CoreModule } from '@ds-suite/core';

import { AppComponent } from './app.component';
import { DsCommonModule } from './ds-common/ds-common.module';


registerLocaleData(localeDe);

const routes: Routes = [
  /*{ path: "", component: HomeComponent }*/
];

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule, 
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
