import { Component, OnInit } from '@angular/core';
import { DisplayTemplateComponent } from '../../../display-template/display-template.component';

import { TerminFunctions } from '../shared/termin.functions';
import { Termin } from '@ds-suite/model';

const MAX_TERMINE = 9;
const MAX_FINISHED = 3;

@Component({
  selector: 'app-edvgt-stele',
  templateUrl: './edvgt-stele.component.html',
  styleUrls: ['./edvgt-stele.component.css']
})
export class EdvgtSteleComponent extends DisplayTemplateComponent {
  termFunc = TerminFunctions;

  filterTermine(termine: Termin[]) : Termin[] {
    return super.removeFinishedTermine(
      super.sortTermine(super.filterTermine(termine)),
      MAX_TERMINE, MAX_FINISHED);
  }

}
