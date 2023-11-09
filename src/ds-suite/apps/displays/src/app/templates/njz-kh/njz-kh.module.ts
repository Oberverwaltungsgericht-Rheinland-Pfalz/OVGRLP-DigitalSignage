// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { Routes, RouterModule } from '@angular/router'
import { FlexLayoutModule } from '@angular/flex-layout'

import { NjzKhFoyerComponent } from './njz-kh-foyer/njz-kh-foyer.component'
import { NjzKhSaalScrollerComponent } from './njz-kh-saal-scroller/njz-kh-saal-scroller.component'

import { DsCommonModule } from '../../ds-common/ds-common.module'
import { CoreModule } from '@ds-suite/core'

const routes: Routes = [
  { path: 'foyer', component: NjzKhFoyerComponent },
  { path: 'saal-scroller', component: NjzKhSaalScrollerComponent }
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
    NjzKhFoyerComponent,
    NjzKhSaalScrollerComponent
  ]
})
export class NjzKhModule { }
