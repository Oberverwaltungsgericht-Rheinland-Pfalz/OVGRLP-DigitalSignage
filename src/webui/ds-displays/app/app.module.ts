import { BrowserModule } from '@angular/platform-browser';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';
import { FlexLayoutModule } from '@angular/flex-layout';
import { environment } from './environment';

import { ConfigService, DisplayService, TerminService } from 'ds-core';
import { AppComponent } from './app.component';
import { DisplayComponent } from './display/display.component';
import { HomeComponent } from './home/home.component';
import { TerminComponent } from './termin/termin.component';
import { CapitalizePipe } from './capitalize.pipe';

import { TEMPLATES } from './display/templates/index';
import { TemplateHostDirective } from './display/template-host.directive';

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
    DisplayComponent,
    HomeComponent,
    TemplateHostDirective,
    CapitalizePipe,
    TerminComponent,
    TEMPLATES
  ],
  imports: [
    RouterModule.forRoot(appRoutes, { useHash: true }),
    BrowserModule,
    HttpModule,
    FlexLayoutModule
  ],
  entryComponents: [
    TEMPLATES
  ],
  providers: [
    ConfigService, 
    {
      provide: APP_INITIALIZER,
      useFactory: ConfigLoader,
      deps: [ConfigService],
      multi: true
    },
    DisplayService,
    TerminService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
