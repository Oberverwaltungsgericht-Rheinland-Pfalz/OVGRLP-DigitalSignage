import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { FlexLayoutModule } from "@angular/flex-layout";

import { DemoFoyerVarAComponent } from './foyer-var-a/foyer-var-a.component';
import { DemoFoyerVarBComponent } from './foyer-var-b/foyer-var-b.component';
import { DemoFoyerVarTrComponent } from './foyer-var-tr/foyer-var-tr.component';
import { DemoFoyerVarTrierComponent } from './foyer-var-trier/foyer-var-trier.component';
import { DemoSaalVarAComponent } from './saal-var-a/saal-var-a.component';
import { DemoSaalVarBComponent } from './saal-var-b/saal-var-b.component';
import { DemoSaalVarRechtsComponent } from './saal-var-weitere-rechts/saal-var-weitere-rechts.component';
import { DemoSaalVarUntenComponent } from './saal-var-weitere-unten/saal-var-weitere-unten.component';
import { DemoSaalVorschauRechtsComponent } from './saal-vorschau-rechts/saal-vorschau-rechts.component';
import { NjzKhSaalWeitereRechtsComponent } from './njz-kh-saal-weitere-rechts/njz-kh-saal-weitere-rechts.component';
import { NjzKhSaalWeitereUntenComponent } from './njz-kh-saal-weitere-unten/njz-kh-saal-weitere-unten.component';
import { SaalScrollerVsimmComponent } from './saal-scroller-vsimm/saal-scroller-vsimm.component';

import { DsCommonModule } from '../../ds-common/ds-common.module';
import { CoreModule } from '@ds-suite/core';

const routes: Routes = [
  { path: "demo-foyer-variante-a", component: DemoFoyerVarAComponent },
  { path: "demo-foyer-variante-b", component: DemoFoyerVarBComponent },
  { path: "demo-foyer-variante-tr", component: DemoFoyerVarTrComponent },
  { path: "demo-foyer-variante-trier", component: DemoFoyerVarTrierComponent },
  { path: "demo-saal-variante-a", component: DemoSaalVarAComponent },
  { path: "demo-saal-variante-b", component: DemoSaalVarBComponent },
  { path: "demo-saal-variante-weitere-rechts", component: DemoSaalVarRechtsComponent },
  { path: "demo-saal-variante-weitere-unten", component: DemoSaalVarUntenComponent },
  { path: "demo-saal-vorschau-rechts", component: DemoSaalVorschauRechtsComponent },
  { path: "saal-weitere-rechts", component: NjzKhSaalWeitereRechtsComponent },
  { path: "saal-weitere-unten", component: NjzKhSaalWeitereUntenComponent },
  { path: "demo-saal-scroller-vsimm", component: SaalScrollerVsimmComponent }
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
    DemoFoyerVarAComponent,
    DemoFoyerVarBComponent,
    DemoFoyerVarTrComponent,
    DemoFoyerVarTrierComponent,
    DemoSaalVarAComponent,
    DemoSaalVarBComponent,
    DemoSaalVarRechtsComponent,
    DemoSaalVarUntenComponent,
    DemoSaalVorschauRechtsComponent,
    NjzKhSaalWeitereRechtsComponent,
    NjzKhSaalWeitereUntenComponent,
    SaalScrollerVsimmComponent
  ]
})
export class DemoModule { }
