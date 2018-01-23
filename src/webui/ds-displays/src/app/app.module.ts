import { APP_INITIALIZER, NgModule, LOCALE_ID } from '@angular/core';
import { enableProdMode } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { FlexLayoutModule } from '@angular/flex-layout';
import { registerLocaleData } from '@angular/common';
import localeDe from '@angular/common/locales/de';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { DisplayComponent } from './display/display.component';
import { TerminComponent } from './termin/termin.component';
import { CapitalizePipe } from './capitalize.pipe';
import { environment } from '../environments/environment';

import { ConfigService } from './service/config.service';
//import { DisplayService } from './service/display.service';
//import { DisplaySoapService } from './service/display-soap.service';
//import { TerminService } from './service/termin.service';
//import { TerminSoapService } from './service/termin-soap.service';
import { AppServicesProdModule } from './service/prod/app-services-prod.module';
import { AppServicesDevModule } from './service/dev/app-services-dev.module';

import { TEMPLATES } from '../templates/index';
import { TemplateComponent } from './display/template.component';
import { TemplateHostDirective } from './display/template-host.directive';

export function ConfigLoader(configService: ConfigService) {
  return() => configService.load(environment.configFile);
}

let serviceModule = AppServicesDevModule;

if (environment.production) {
  serviceModule = AppServicesProdModule;
  enableProdMode();
}

registerLocaleData(localeDe);

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    DisplayComponent,
    TerminComponent,
    TemplateHostDirective,
    CapitalizePipe,
    TemplateComponent,
    TEMPLATES
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FlexLayoutModule,
    [serviceModule]
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
    {
      provide: LOCALE_ID, useValue: 'de'
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
