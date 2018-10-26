import { Component, OnInit } from '@angular/core';
import { DisplayTemplateComponent } from '../../../display-template/display-template.component';
import { Termin, TerminStatus } from '@ds-suite/model';

import { TerminFunctions } from '../shared/termin.functions';

const MAX_TERMINE = 9;
const MAX_FINISHED = 3;

@Component({
  selector: 'app-njz-saal',
  templateUrl: './njz-ko-saal.component.html',
  styleUrls: ['./njz-ko-saal.component.css']
})
export class NjzKoSaalComponent extends DisplayTemplateComponent {
  termFunc = TerminFunctions;

  filterTermine(termine: Termin[]) : Termin[] {
    return super.removeFinishedTermine(
      super.sortTermine(super.filterTermine(termine)), 
      MAX_TERMINE, MAX_FINISHED);
  }
}
