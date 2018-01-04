import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FlexLayoutModule } from '@angular/flex-layout';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { DisplayComponent } from './display/display.component';
import { TerminComponent } from './termin/termin.component';
import { CapitalizePipe } from './capitalize.pipe';
import { environment } from '../environments/environment';

import { ConfigService } from './service/config.service';
import { DisplayService } from './service/display.service';
import { TerminService } from './service/termin.service';

import { TEMPLATES } from '../templates/index';
import { TemplateHostDirective } from './display/template-host.directive';

export function ConfigLoader(configService: ConfigService) {
  return() => configService.load(environment.configFile);
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    DisplayComponent,
    TerminComponent,
    TemplateHostDirective,
    CapitalizePipe,
    TEMPLATES
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
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
