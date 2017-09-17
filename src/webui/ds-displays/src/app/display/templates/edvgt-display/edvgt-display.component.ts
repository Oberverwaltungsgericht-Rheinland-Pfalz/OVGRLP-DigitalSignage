import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { DisplayComponent } from '../../display.component';

import { DisplayService, Display, TerminService, Termin } from 'ds-core';

@Component({
  selector: 'edvg-display',
  templateUrl: './edvg-display.component.html',
  styleUrls: ['./edvgt-display.component.css'],
  providers: [DisplayService, TerminService] //TODO: hier wirklich richtig?
})

export class EdvgtDisplayComponent extends DisplayComponent {
}