import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { FlexLayoutModule } from "@angular/flex-layout";

import { NjzKhSaalWeitereRechtsComponent } from './njz-kh-saal-weitere-rechts/njz-kh-saal-weitere-rechts.component';
import { NjzKhSaalWeitereUntenComponent } from './njz-kh-saal-weitere-unten/njz-kh-saal-weitere-unten.component';

import { DsCommonModule } from '../../ds-common/ds-common.module';

const routes: Routes = [
  { path: "saal-weitere-rechts", component: NjzKhSaalWeitereRechtsComponent },
  { path: "saal-weitere-unten", component: NjzKhSaalWeitereUntenComponent },
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FlexLayoutModule,
    DsCommonModule
  ],
  declarations: [
    NjzKhSaalWeitereRechtsComponent,
    NjzKhSaalWeitereUntenComponent,
  ]
})
export class DemoModule { }
