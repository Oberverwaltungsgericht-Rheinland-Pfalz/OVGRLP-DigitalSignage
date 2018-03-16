import { NgModule, enableProdMode } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { Routes, RouterModule } from "@angular/router";
import { NxModule } from "@nrwl/nx";
import { ClarityModule } from "@clr/angular";

import { BackendModule } from "@ds-suite/backend";
import { BackendDevModule } from "@ds-suite/backend-dev";

import { AppComponent } from "./app.component";
import { HomeComponent } from "./home/home.component";

import { environment } from "../environments/environment";
import { DisplayComponent } from './display/display.component';

const routes: Routes = [
  { path: "", component: HomeComponent },
  { path: ":name", component: DisplayComponent }
];

let BACKEND_MODULE = BackendDevModule;
if (environment.production) {
  BACKEND_MODULE = BackendModule;
  enableProdMode();
}

@NgModule({
  imports: [
    BrowserModule,
    NxModule.forRoot(),
    RouterModule.forRoot(routes, {
      useHash: true,
      initialNavigation: "enabled"
    }),
    ClarityModule,
    BACKEND_MODULE
  ],
  declarations: [AppComponent, HomeComponent, DisplayComponent],
  bootstrap: [AppComponent]
})
export class AppModule {}
