// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { Component, OnInit } from '@angular/core'
import { trigger, state, style, animate, transition, stagger, query, keyframes } from '@angular/animations'

import { DisplayTemplateComponent } from '../../../display-template/display-template.component'

import { Termin, TerminStatus } from '@ds-suite/model'
const MAX_TERMINE = 3
const MAX_FINISHED = 1
const MAX_TERMINITEMS = 5

@Component({
  selector: 'app-sitzungssaal',
  templateUrl: './sitzungssaal.component.html',
  styleUrls: ['./sitzungssaal.component.css'],
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
export class JZZWSitzungssaalComponent extends DisplayTemplateComponent {
  ngOnInit () {
    this.SwitchMultipleActiveTermine = true
    super.ngOnInit()
  }

  showTermineStream (termine: Termin[], maxTermine: number, minFinished: number, maxTerminitems: number): Termin[] {
    let termineTmp = termine
    if (termineTmp.length > maxTermine) {
      const termineFinished = termineTmp.filter(t => t.status == TerminStatus.abgeschlossen)
      const termineUnfinished = termineTmp.filter(t => t.status != TerminStatus.abgeschlossen)
      if (termineFinished.length > minFinished) {
        // grundsätzlich nur noch die minimale Anzahl der erledigten Termine anzeigen,
        // wenn insgesamt mehr Termine vorhanden sind, als darstellbar sind
        let clearCount: number = termineFinished.length - minFinished

        // Wenn jedoch möglich, die darstellbaren Termine wieder mit den erledigten füllen
        // (mehr als die minimale Anzahl von Terminen)
        if ((termineFinished.length - clearCount + termineUnfinished.length) < maxTermine) { clearCount = (termineFinished.length + termineUnfinished.length - (maxTermine)) }

        // ggf. erledigte Termine rausfiltern
        if (clearCount > 0) { termineFinished.splice(0, clearCount) }
      }
      termineTmp = termineFinished.concat(termineUnfinished)
      if (termineTmp.length > maxTerminitems) {
        const subtraktor: number = termineTmp.length - (maxTerminitems - termineTmp.length)
        termineTmp.splice(maxTerminitems, subtraktor)
      }
    }
    return termineTmp
  }

  filterTermine (termine: Termin[]): Termin[] {
    return this.showTermineStream(
      super.sortTermine(super.filterTermine(termine)),
      MAX_TERMINE, MAX_FINISHED, MAX_TERMINITEMS)
  }
}
