// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { Routes, RouterModule } from '@angular/router'
import { FlexLayoutModule } from '@angular/flex-layout'

import { PfolgFoyerComponent } from './pfolg-foyer/pfolg-foyer.component'
import { PfolgSaalVorschauRechtsComponent } from './pfolg-saal-vorschau-rechts/pfolg-saal-vorschau-rechts.component'
import { PfolgSaalVorschauRechtsGrauComponent } from './pfolg-saal-vorschau-rechts-grau/pfolg-saal-vorschau-rechts-grau.component'
import { PfolgPforteComponent } from './pfolg-pforte/pfolg-pforte.component'

import { DsCommonModule } from '../../ds-common/ds-common.module'
import { CoreModule } from '@ds-suite/core'

const routes: Routes = [
  { path: 'pfolg-foyer', component: PfolgFoyerComponent },
  { path: 'pfolg-saal-vorschau-rechts', component: PfolgSaalVorschauRechtsComponent },
  { path: 'pfolg-saal-vorschau-rechts-grau', component: PfolgSaalVorschauRechtsGrauComponent },
  { path: 'pfolg-pforte', component: PfolgPforteComponent }
]

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FlexLayoutModule,
    DsCommonModule,
    CoreModule
  ],
  declarations: [
    PfolgFoyerComponent,
    PfolgSaalVorschauRechtsComponent,
    PfolgSaalVorschauRechtsGrauComponent,
    PfolgPforteComponent
  ]
})
export class PfolgModule { }
