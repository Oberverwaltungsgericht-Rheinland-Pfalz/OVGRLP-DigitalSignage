// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { Routes, RouterModule } from '@angular/router'
import { FlexLayoutModule } from '@angular/flex-layout'

import { JzTrFoyerComponent } from './foyer-trier/foyer-trier.component'

import { DsCommonModule } from '../../ds-common/ds-common.module'
import { CoreModule } from '@ds-suite/core'

const routes: Routes = [
  { path: 'jztr-foyer', component: JzTrFoyerComponent }
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
    JzTrFoyerComponent
  ]
})
export class JzTrModule { }
