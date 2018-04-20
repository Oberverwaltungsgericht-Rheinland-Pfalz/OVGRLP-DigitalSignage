import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { FlexLayoutModule } from "@angular/flex-layout";

import { NjzKhFoyerComponent } from './njz-kh-foyer/njz-kh-foyer.component';
import { NjzKhFoyerWappenComponent } from './njz-kh-foyer-wappen/njz-kh-foyer-wappen.component';
import { NjzKhSaalComponent } from './njz-kh-saal/njz-kh-saal.component';

import { DsCommonModule } from '../../ds-common/ds-common.module';

const routes: Routes = [
  { path: "foyer", component: NjzKhFoyerComponent },
  { path: "foyer-wappen", component: NjzKhFoyerWappenComponent },
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
    NjzKhFoyerWappenComponent,
    NjzKhSaalComponent
  ]
})
export class NjzKhModule { }
