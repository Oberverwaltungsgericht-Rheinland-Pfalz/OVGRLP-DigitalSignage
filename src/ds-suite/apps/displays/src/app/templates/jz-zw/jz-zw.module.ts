import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { FlexLayoutModule } from "@angular/flex-layout";

import { JZZWFoyerVarAComponent } from './foyer-var-a/foyer-var-a.component';
import { JZZWFoyerVarBComponent } from './foyer-var-b/foyer-var-b.component';
import { JZZWSaalVarAComponent } from './saal-var-a/saal-var-a.component';
import { JZZWSaalVarBComponent } from './saal-var-b/saal-var-b.component';
import { JZZWSaalVarRechtsComponent } from './saal-var-weitere-rechts/saal-var-weitere-rechts.component';
import { JZZWSaalVarUntenComponent } from './saal-var-weitere-unten/saal-var-weitere-unten.component';
import { JZZWSitzungssaalComponent } from './sitzungssaal/sitzungssaal.component';

import { DsCommonModule } from '../../ds-common/ds-common.module';
import { CoreModule } from '@ds-suite/core';

const routes: Routes = [
  { path: "jzzw-foyer-variante-a", component: JZZWFoyerVarAComponent },
  { path: "jzzw-foyer-variante-b", component: JZZWFoyerVarBComponent },
  { path: "jzzw-saal-variante-a", component: JZZWSaalVarAComponent },
  { path: "jzzw-saal-variante-b", component: JZZWSaalVarBComponent },
  { path: "jzzw-saal-variante-weitere-rechts", component: JZZWSaalVarRechtsComponent },
  { path: "jzzw-saal-variante-weitere-unten", component: JZZWSaalVarUntenComponent },
  { path: "jzzw-sitzungssaal", component: JZZWSitzungssaalComponent }
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
    JZZWFoyerVarAComponent,
    JZZWFoyerVarBComponent,
    JZZWSaalVarAComponent,
    JZZWSaalVarBComponent,
    JZZWSaalVarRechtsComponent,
    JZZWSaalVarUntenComponent,
    JZZWSitzungssaalComponent
  ]
})
export class JzZwModule { }
