import { NgModule } from '@angular/core';
import { NxModule } from '@nrwl/nx';

import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';

import { BackendModule } from '@ds-suite/backend';
import { BackendDevModule as BACKEND_MODULE } from '@ds-suite/backend-dev';

import { AppConfig } from '@ds-suite/model';
import { DS_DISPLAYS_CONFIG } from './app.config';
import { DisplayComponent } from './display/display.component';
import { DisplayTemplateComponent } from './display-template/display-template.component';

import { TemplateHostDirective } from './display-template/template-host.directive';
import { TEMPLATES } from './templates';
import { TerminComponent } from './termin/termin.component';
import { CapitalizePipe } from './capitalize.pipe';
import { NjzFoyerComponent } from './templates/njz-foyer/njz-foyer.component';
import { NjzSaalComponent } from './templates/njz-saal/njz-saal.component';
import { NjzkhFoyerComponent } from './templates/njzkh-foyer/njzkh-foyer.component';
import { EdvgtSteleComponent } from './templates/edvgt-stele/edvgt-stele.component';

const routes: Routes = [{ path: '', component: HomeComponent }, { path: ':name', component: DisplayComponent }];

@NgModule({
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
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
    CapitalizePipe,
    NjzFoyerComponent,
    NjzSaalComponent,
    NjzkhFoyerComponent,
    EdvgtSteleComponent
  ],
  entryComponents: [TEMPLATES],
  bootstrap: [AppComponent]
})
export class AppModule {}
