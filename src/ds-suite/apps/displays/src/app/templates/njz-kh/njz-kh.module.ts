import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { FlexLayoutModule } from "@angular/flex-layout";

import { NjzKhFoyerComponent } from './njz-kh-foyer/njz-kh-foyer.component';
import { NjzKhSaalComponent } from './njz-kh-saal/njz-kh-saal.component';

import { DsCommonModule } from '../../ds-common/ds-common.module';

const routes: Routes = [
  { path: "foyer", component: NjzKhFoyerComponent },
  { path: "saal", component: NjzKhSaalComponent }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FlexLayoutModule,
    DsCommonModule
  ],
  declarations: [
    NjzKhFoyerComponent,
    NjzKhSaalComponent
  ]
})
export class NjzKhModule { }
