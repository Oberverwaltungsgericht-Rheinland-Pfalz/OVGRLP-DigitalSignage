// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { Component, OnInit } from '@angular/core'
import { trigger, state, style, animate, transition, stagger, query, keyframes } from '@angular/animations'

import { DisplayTemplateComponent } from '../../../display-template/display-template.component'

@Component({
  selector: 'app-saal-var-b',
  templateUrl: './saal-var-b.component.html',
  styleUrls: ['./saal-var-b.component.css'],
  animations: [
    trigger('terminAnimation', [
      state('in', style({ opacity: 1, height: '*', 'padding-top': '*' })),
      transition('in => void', [
        animate(
          '2s ease-out',
          keyframes([
            style({ opacity: 0, offset: 0.3, 'padding-top': 0 }),
            style({ height: 0, offset: 1 })
          ])
        )
      ])
    ])
  ]
})
export class DemoSaalVarBComponent extends DisplayTemplateComponent {
  public updateInterval = 5000
}
