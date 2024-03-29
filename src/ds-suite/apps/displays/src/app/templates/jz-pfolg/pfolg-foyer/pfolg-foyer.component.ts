// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { Component, OnInit } from '@angular/core'
import { trigger, state, style, animate, transition, stagger, query, keyframes } from '@angular/animations'

import { Termin, Display } from '@ds-suite/model'
import { DisplayTemplateComponent } from '../../../display-template/display-template.component'

@Component({
  selector: 'app-pfolg-foyer',
  templateUrl: './pfolg-foyer.component.html',
  styleUrls: ['./pfolg-foyer.component.css'],

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
export class PfolgFoyerComponent extends DisplayTemplateComponent {
  ngOnInit () {
    this.updateInterval = 5000

    super.ngOnInit()
  }
}
