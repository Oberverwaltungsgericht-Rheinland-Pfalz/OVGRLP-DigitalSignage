// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { Component, OnInit } from '@angular/core';
import { trigger, state, style, animate, transition, stagger, query, keyframes } from '@angular/animations';

import { DisplayTemplateComponent } from '../../../display-template/display-template.component';

import { Termin, TerminStatus } from '@ds-suite/model';
const MAX_TERMINE = 3;
const MAX_FINISHED = 1;
const MAX_TERMINITEMS = 5;

@Component({
  selector: 'app-pfolg-saal-vorschau-rechts',
  templateUrl: './pfolg-saal-vorschau-rechts.component.html',
  styleUrls: ['./pfolg-saal-vorschau-rechts.component.css'],
  animations: [
    trigger('terminAnimation', [
      state('in', style({ opacity: 1, height: '*', 'padding-top': '*' })),
      transition('in => void', [
        animate(
          '3s ease-out',
          keyframes([
            style({ opacity: 0, offset: 0.3, 'padding-top': 0 }), 
            style({ height: 0, offset: 1 })
          ])
        )
      ])
    ])
  ]
})
export class PfolgSaalVorschauRechtsComponent extends DisplayTemplateComponent {
  public updateInterval = 4000;
}
