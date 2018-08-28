import { Component, OnInit } from '@angular/core';
import { DisplayTemplateComponent } from '../../../display-template/display-template.component';

import { TerminFunctions } from '../shared/termin.functions';

@Component({
  selector: 'app-njz-saal',
  templateUrl: './njz-ko-saal.component.html',
  styleUrls: ['./njz-ko-saal.component.css']
})
export class NjzKoSaalComponent extends DisplayTemplateComponent {
  termFunc = TerminFunctions;
}
