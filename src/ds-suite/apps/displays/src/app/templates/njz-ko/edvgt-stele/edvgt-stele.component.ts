import { Component, OnInit } from '@angular/core';
import { DisplayTemplateComponent } from '../../../display-template/display-template.component';

import { TerminFunctions } from '../shared/termin.functions';

@Component({
  selector: 'app-edvgt-stele',
  templateUrl: './edvgt-stele.component.html',
  styleUrls: ['./edvgt-stele.component.css']
})
export class EdvgtSteleComponent extends DisplayTemplateComponent {
  termFunc = TerminFunctions;
}
