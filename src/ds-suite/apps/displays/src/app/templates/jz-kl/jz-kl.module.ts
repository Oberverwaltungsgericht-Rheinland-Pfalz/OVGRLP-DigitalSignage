// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { Routes, RouterModule } from '@angular/router'
import { FlexLayoutModule } from '@angular/flex-layout'

import { JZKLFoyerVarAComponent } from './foyer-var-a/foyer-var-a.component'
import { JZKLFoyerVarBComponent } from './foyer-var-b/foyer-var-b.component'
import { JZKLSaalVarAComponent } from './saal-var-a/saal-var-a.component'
import { JZKLSaalVarBComponent } from './saal-var-b/saal-var-b.component'
import { JZKLSaalVarRechtsComponent } from './saal-var-weitere-rechts/saal-var-weitere-rechts.component'
import { JZKLSaalVarUntenComponent } from './saal-var-weitere-unten/saal-var-weitere-unten.component'

import { DsCommonModule } from '../../ds-common/ds-common.module'
import { CoreModule } from '@ds-suite/core'

const routes: Routes = [
  { path: 'jzkl-foyer-variante-a', component: JZKLFoyerVarAComponent },
  { path: 'jzkl-foyer-variante-b', component: JZKLFoyerVarBComponent },
  { path: 'jzkl-saal-variante-a', component: JZKLSaalVarAComponent },
  { path: 'jzkl-saal-variante-b', component: JZKLSaalVarBComponent },
  { path: 'jzkl-saal-variante-weitere-rechts', component: JZKLSaalVarRechtsComponent },
  { path: 'jzkl-saal-variante-weitere-unten', component: JZKLSaalVarUntenComponent }
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
    JZKLFoyerVarAComponent,
    JZKLFoyerVarBComponent,
    JZKLSaalVarAComponent,
    JZKLSaalVarBComponent,
    JZKLSaalVarRechtsComponent,
    JZKLSaalVarUntenComponent
  ]
})
export class JzKlModule { }
