import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { NxModule } from '@nrwl/nx';

import { AppComponent } from './app.component';

@NgModule({
  imports: [BrowserModule, NxModule.forRoot(), RouterModule.forRoot([], { initialNavigation: 'enabled' })],
  declarations: [AppComponent],
  bootstrap: [AppComponent]
})
export class AppModule {}
