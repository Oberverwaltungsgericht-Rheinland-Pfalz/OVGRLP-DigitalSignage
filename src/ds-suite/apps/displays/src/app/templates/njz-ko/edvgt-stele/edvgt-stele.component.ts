// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { Component, OnInit } from '@angular/core'
import { DisplayTemplateComponent } from '../../../display-template/display-template.component'

import { TerminFunctions } from '../shared/termin.functions'
import { Termin } from '@ds-suite/model'

const MAX_TERMINE = 6
const MAX_FINISHED = 2

@Component({
  selector: 'app-edvgt-stele',
  templateUrl: './edvgt-stele.component.html',
  styleUrls: ['./edvgt-stele.component.css']
})
export class EdvgtSteleComponent extends DisplayTemplateComponent {
  termFunc = TerminFunctions

  filterTermine (termine: Termin[]): Termin[] {
    return super.removeFinishedTermine(
      super.sortTermine(super.filterTermine(termine)),
      MAX_TERMINE, MAX_FINISHED)
  }
}
