import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { FlexLayoutModule } from "@angular/flex-layout";
import { DsCommonModule } from '../../ds-common/ds-common.module';
import { CoreModule } from '@ds-suite/core';

import { NjzKoFoyerComponent } from './njz-ko-foyer/njz-ko-foyer.component';
import { NjzKoSaalComponent } from './njz-ko-saal/njz-ko-saal.component';

import { TerminComponent } from './termin/termin.component';

const routes: Routes = [
  { path: "foyer", component: NjzKoFoyerComponent },
  { path: "saal", component: NjzKoSaalComponent }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FlexLayoutModule,
    DsCommonModule,
    CoreModule
  ],
  declarations: [
    NjzKoFoyerComponent,
    NjzKoSaalComponent,
    TerminComponent
  ]
})
export class NjzKoModule { }
