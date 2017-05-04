import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';
import { environment } from '../environments/environment';
import { MdSidenavModule } from '@angular/material';

import 'hammerjs';

import { ConfigService } from 'ds-core';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { DisplayComponent } from './display/display.component';

const appRoutes: Routes = [
  { path: ':name', component: DisplayComponent },
  { path: '', component: HomeComponent }
];

export function ConfigLoader(configService: ConfigService) {
  return () => configService.load(environment.configFile);
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    DisplayComponent
  ],
  imports: [
    RouterModule.forRoot(appRoutes, { useHash: true }),
    BrowserModule,
    FormsModule,
    HttpModule,
    BrowserAnimationsModule,
    MdSidenavModule
  ],
  providers: [
    ConfigService, {
      provide: APP_INITIALIZER,
      useFactory: ConfigLoader,
      deps: [ConfigService],
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
