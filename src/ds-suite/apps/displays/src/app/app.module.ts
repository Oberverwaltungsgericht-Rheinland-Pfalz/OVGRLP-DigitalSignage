import { NgModule } from "@angular/core";
import { AppComponent } from "./app.component";
import { BrowserModule } from "@angular/platform-browser";
import { NxModule } from "@nrwl/nx";
import { Routes, RouterModule } from "@angular/router";
import { HomeComponent } from "./home/home.component";

import { BackendModule } from '@ds-suite/backend';
import { AppConfig } from '@ds-suite/model';
import { DS_DISPLAYS_CONFIG } from './app.config';

const routes: Routes = [
  { path: "", component: HomeComponent }
]

@NgModule({
  imports: [
    BrowserModule,
    NxModule.forRoot(),
    RouterModule.forRoot(routes, { useHash: true, initialNavigation: "enabled" }),
    BackendModule
  ],
  providers: [
    { provide: AppConfig, useValue: DS_DISPLAYS_CONFIG }
  ],
  declarations: [AppComponent, HomeComponent],
  bootstrap: [AppComponent]
})
export class AppModule {}
