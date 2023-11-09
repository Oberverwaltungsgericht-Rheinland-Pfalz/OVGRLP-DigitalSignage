// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { FlexLayoutModule } from "@angular/flex-layout";
import { DsCommonModule } from '../../ds-common/ds-common.module';
import { CoreModule } from '@ds-suite/core';

import { NjzKoFoyerComponent } from './njz-ko-foyer/njz-ko-foyer.component';
import { NjzKoSaalComponent } from './njz-ko-saal/njz-ko-saal.component';
import { EdvgtSteleComponent } from './edvgt-stele/edvgt-stele.component';

import { TerminComponent } from './termin/termin.component';

const routes: Routes = [
  { path: "foyer", component: NjzKoFoyerComponent },
  { path: "saal", component: NjzKoSaalComponent },
  { path: "edvgt-stele", component: EdvgtSteleComponent}
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
    EdvgtSteleComponent,
    TerminComponent
  ]
})
export class NjzKoModule { }
