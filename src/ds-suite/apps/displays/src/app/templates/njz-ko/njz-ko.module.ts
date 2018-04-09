import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { FlexLayoutModule } from "@angular/flex-layout";

import { NjzKoFoyerComponent } from './njz-ko-foyer/njz-ko-foyer.component';
import { NjzKoSaalComponent } from './njz-ko-saal/njz-ko-saal.component';

const routes: Routes = [
  { path: "foyer", component: NjzKoFoyerComponent },
  { path: "saal", component: NjzKoSaalComponent }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FlexLayoutModule
  ],
  declarations: [
    NjzKoFoyerComponent,
    NjzKoSaalComponent
  ]
})
export class NjzKoModule { }
