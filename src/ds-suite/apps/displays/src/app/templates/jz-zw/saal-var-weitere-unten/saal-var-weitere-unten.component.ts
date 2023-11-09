// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { Component, OnInit } from '@angular/core'
import { trigger, state, style, animate, transition, stagger, query, keyframes } from '@angular/animations'

import { DisplayTemplateComponent } from '../../../display-template/display-template.component'

@Component({
  selector: 'app-saal-var-weitere-unten',
  templateUrl: './saal-var-weitere-unten.component.html',
  styleUrls: ['./saal-var-weitere-unten.component.css'],
  animations: [
    trigger('terminAnimation', [
      state('in', style({ opacity: 1, 'min-width': '*', margin: '*' })),
      transition('in => void', [
        animate(
          '2s ease-out',
          keyframes([
            style({ opacity: 0, offset: 0.3 }),
            style({ 'min-width': '0px', margin: 0, offset: 1 })
          ])
        )
      ])
    ])
  ]
})
export class JZZWSaalVarUntenComponent extends DisplayTemplateComponent {
  public updateInterval = 6000
}
