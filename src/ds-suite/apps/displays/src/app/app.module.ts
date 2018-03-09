import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Routes, RouterModule } from '@angular/router';
import { FlexLayoutModule } from '@angular/flex-layout';
import { NxModule } from '@nrwl/nx';

import { BackendModule } from '@ds-suite/backend';
import { BackendDevModule as BACKEND_MODULE } from '@ds-suite/backend-dev';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { DisplayComponent } from './display/display.component';
import { DisplayTemplateComponent } from './display-template/display-template.component';
import { TerminComponent } from './termin/termin.component';

import { AppConfig } from '@ds-suite/model';
import { DS_DISPLAYS_CONFIG } from './app.config';

import { TemplateHostDirective } from './display-template/template-host.directive';
import { TEMPLATES } from './templates';

import { CapitalizePipe } from './capitalize.pipe';

const routes: Routes = [{ path: '', component: HomeComponent }, { path: ':name', component: DisplayComponent }];

@NgModule({
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FlexLayoutModule,
    NxModule.forRoot(),
    RouterModule.forRoot(routes, { useHash: true, initialNavigation: 'enabled' }),
    BACKEND_MODULE
  ],
  providers: [{ provide: AppConfig, useValue: DS_DISPLAYS_CONFIG }],
  declarations: [
    AppComponent,
    HomeComponent,
    DisplayComponent,
    DisplayTemplateComponent,
    TemplateHostDirective,
    TEMPLATES,
    TerminComponent,
    CapitalizePipe
  ],
  entryComponents: [TEMPLATES],
  bootstrap: [AppComponent]
})
export class AppModule {}
