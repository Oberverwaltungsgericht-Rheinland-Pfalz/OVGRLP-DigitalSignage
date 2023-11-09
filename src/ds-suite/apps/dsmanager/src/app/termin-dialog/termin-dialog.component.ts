// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core'

import { Termin, TerminStatus, Stammdaten } from '@ds-suite/model'
import { TerminService, StammdatenService } from '@ds-suite/core'
import { YesNoDialogComponent } from '@ds-suite/ui'

@Component({
  selector: 'termin-dialog',
  templateUrl: './termin-dialog.component.html',
  styleUrls: ['./termin-dialog.component.css']
})
export class TerminDialogComponent implements OnInit {
  @Output() dataChanged = new EventEmitter<void>()
  @ViewChild(YesNoDialogComponent, { static: true }) yesNoDialog: YesNoDialogComponent
  public termin: any
  public stammdaten: Stammdaten[]
  public show: boolean = false
  public neuanlage: boolean = false

  constructor (private readonly terminService: TerminService,
    private readonly stammdatenService: StammdatenService) { }

  GetStatausValues (): string[] {
    return Object.values(TerminStatus)
  }

  open (termin: Termin) {
    this.terminService.getTerminByBreeze(termin.id).then(item => {
      this.neuanlage = false
      this.termin = item
      // ggf. Neuanlage...
      if (undefined == this.termin && termin.id == -1) {
        this.neuanlage = true
        this.termin = this.terminService.breezeEntityManager.createEntity('Verfahren')
        this.initStammdaten()
      }
    })
    this.show = true
  }

  initStammdaten () {
    this.stammdatenService.getStammdaten()
      .subscribe(stammdaten => {
        this.stammdaten = stammdaten
        // 1. Gericht vorschlagen
        if (this.stammdaten.length > 0) {
          this.termin.StammdatenId = this.stammdaten[0].stammdatenId
        }
      })
  }

  addItemClick (arr: any[], typeName: string) {
    arr.push(this.terminService.breezeEntityManager.createEntity(typeName))
  }

  deleteItemClick (arr: any[], element: any) {
    const index = arr.indexOf(element)
    element.entityAspect.setDeleted()
    if (index > -1) {
      arr.splice(index, 1)
    }
  }

  deleteClick () {
    this.yesNoDialog.open()
  }

  saveClick () {
    this.checkNullFields()
    this.terminService.saveTerminByBreeze(this.termin).then(() => {
      this.dataChanged.emit()
      this.close()
    })
    this.show = false
  }

  checkNullFields () {
    if (this.termin.Az == undefined || this.termin.Az == '') {
      this.termin.Az = ' '
    }
    if (this.termin.Sitzungssaal == undefined || this.termin.Sitzungssaal == '') {
      this.termin.Sitzungssaal = ' '
    }
    if (this.termin.UhrzeitPlan == undefined || this.termin.UhrzeitPlan == '') {
      this.termin.UhrzeitPlan = ' '
    }
    if (this.termin.UhrzeitAktuell == undefined || this.termin.UhrzeitAktuell == '') {
      this.termin.UhrzeitAktuell = ' '
    }
    if (this.termin.Oeffentlich == undefined || this.termin.Oeffentlich == '') {
      this.termin.Oeffentlich = ' '
    }
    if (this.termin.Gegenstand == undefined || this.termin.Gegenstand == '') {
      this.termin.Gegenstand = ' '
    }
    if (this.termin.Art == undefined || this.termin.Art == '') {
      this.termin.Art = ' '
    }
  }

  changeOeffentlich (termin: any) {
    if (termin.Oeffentlich === 'ja') { termin.Oeffentlich = 'nein' } else { termin.Oeffentlich = 'ja' }
  }

  close () {
    this.terminService.breezeEntityManager.clear()
    this.show = false
  }

  ngOnInit () {
  }

  OnDeleteResult (result: boolean) {
    if (result) {
      this.terminService.deleteTerminByBreeze(this.termin).then(() => {
        this.dataChanged.emit()
        this.close()
      })
    }
  }
}
