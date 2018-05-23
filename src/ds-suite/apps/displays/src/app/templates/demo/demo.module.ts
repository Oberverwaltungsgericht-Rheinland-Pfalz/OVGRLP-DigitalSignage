import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { FlexLayoutModule } from "@angular/flex-layout";

import { DemoFoyerVarAComponent } from './foyer-var-a/foyer-var-a.component';
import { DemoFoyerVarBComponent } from './foyer-var-b/foyer-var-b.component';
import { DemoSaalVarAComponent } from './saal-var-a/saal-var-a.component';
import { DemoSaalVarBComponent } from './saal-var-b/saal-var-b.component';
import { DemoSaalVarRechtsComponent } from './saal-var-weitere-rechts/saal-var-weitere-rechts.component';
import { DemoSaalVarUntenComponent } from './saal-var-weitere-unten/saal-var-weitere-unten.component';
import { NjzKhSaalWeitereRechtsComponent } from './njz-kh-saal-weitere-rechts/njz-kh-saal-weitere-rechts.component';
import { NjzKhSaalWeitereUntenComponent } from './njz-kh-saal-weitere-unten/njz-kh-saal-weitere-unten.component';

import { DsCommonModule } from '../../ds-common/ds-common.module';

const routes: Routes = [
  { path: "demo-foyer-variante-a", component: DemoFoyerVarAComponent },
  { path: "demo-foyer-variante-b", component: DemoFoyerVarBComponent },
  { path: "demo-saal-variante-a", component: DemoSaalVarAComponent },
  { path: "demo-saal-variante-b", component: DemoSaalVarBComponent },
  { path: "demo-saal-variante-weitere-rechts", component: DemoSaalVarRechtsComponent },
  { path: "demo-saal-variante-weitere-unten", component: DemoSaalVarUntenComponent },
  { path: "saal-weitere-rechts", component: NjzKhSaalWeitereRechtsComponent },
  { path: "saal-weitere-unten", component: NjzKhSaalWeitereUntenComponent }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FlexLayoutModule,
    DsCommonModule
  ],
  declarations: [
    DemoFoyerVarAComponent,
    DemoFoyerVarBComponent,
    DemoSaalVarAComponent,
    DemoSaalVarBComponent,
    DemoSaalVarRechtsComponent,
    DemoSaalVarUntenComponent,
    NjzKhSaalWeitereRechtsComponent,
    NjzKhSaalWeitereUntenComponent
  ]
})
export class DemoModule { }
