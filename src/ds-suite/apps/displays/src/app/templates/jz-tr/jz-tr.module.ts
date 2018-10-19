import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { FlexLayoutModule } from "@angular/flex-layout";

import { JzTrFoyerVarTrComponent } from './foyer-var-tr/foyer-var-tr.component';
import { JzTrFoyerVarTrierComponent } from './foyer-var-trier/foyer-var-trier.component';

import { DsCommonModule } from '../../ds-common/ds-common.module';
import { CoreModule } from '@ds-suite/core';

const routes: Routes = [
  { path: "jztr-foyer-variante-tr", component: JzTrFoyerVarTrComponent },
  { path: "jztr-foyer-variante-trier", component: JzTrFoyerVarTrierComponent }
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
    JzTrFoyerVarTrComponent,
    JzTrFoyerVarTrierComponent
  ]
})
export class JzTrModule { }
