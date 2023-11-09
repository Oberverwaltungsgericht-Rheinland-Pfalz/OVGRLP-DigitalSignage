// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { Component, OnInit, ViewChild } from '@angular/core'
import { ActivatedRoute } from '@angular/router'

import { Display, Restriction, Termin, TerminStatus, BasicPermissions } from '@ds-suite/model'
import { DisplayService, TerminService, PermissionService } from '@ds-suite/core'

import { ClrDatagrid } from '@clr/angular'

import { TerminDialogComponent } from '../termin-dialog/termin-dialog.component'

@Component({
  selector: 'termine',
  templateUrl: './termine.component.html',
  styleUrls: ['./termine.component.css']
})
export class TermineComponent implements OnInit {
  termine: Termin[]
  filteredTermine: Termin[]
  loadingTermine: boolean = true
  basicPermission: BasicPermissions
  public selStatus: boolean[] = [false, false, false, false, false]
  public selGericht: boolean[] = []
  public selSaal: boolean[] = []
  private routedSaal: string = ''
  @ViewChild('DataGridTermine', { static: true }) DataGridTermine: ClrDatagrid
  @ViewChild(TerminDialogComponent, { static: true }) terminDialog: TerminDialogComponent

  constructor (private readonly terminService: TerminService,
    private readonly permissionService: PermissionService,
    private readonly route: ActivatedRoute) {
  }

  changeSelection () {
    this.filterTermine()
  }

  editTerminClick (termin: Termin) {
    this.terminDialog.open(termin)
  }

  filterTermine () {
    this.filteredTermine = []
    let i: number
    const selStatus: string[] = []
    const selGericht: string[] = []
    const selSaal: string[] = []

    const statusValues: string[] = this.GetStatusValues()
    const gerichtValues: string[] = this.GetGerichtValues()
    const saalValues: string[] = this.GetSaalValues()

    for (i = 0; i < this.selStatus.length; i++) {
      if (this.selStatus[i]) {
        selStatus.push(statusValues[i])
      }
    }
    for (i = 0; i < this.selGericht.length; i++) {
      if (this.selGericht[i]) {
        selGericht.push(gerichtValues[i])
      }
    }
    for (i = 0; i < this.selSaal.length; i++) {
      if (this.selSaal[i]) {
        selSaal.push(saalValues[i])
      }
    }

    this.filteredTermine = this.termine.filter(t => {
      if (selStatus.length > 0 && !selStatus.includes(t.status)) { return false }
      if (selGericht.length > 0 && !selGericht.includes(t.gericht)) { return false }
      if (selSaal.length > 0 && !selSaal.includes(t.sitzungssaal)) { return false }
      return true
    })
  }

  GetStatusValues (forView: boolean = false): string[] {
    if (this.termine == undefined) return []
    const rval = Array.from(new Set(this.termine.map(t => t.status))).sort((d1, d2) => d1 > d2 ? 1 : -1)

    // für die Anzeige '' in 'offen' übersetzen
    if (forView) {
      const indOffen = rval.indexOf(TerminStatus.offen)
      if (indOffen > -1) {
        rval[indOffen] = 'Offen'
      }
    }

    return rval
  }

  GetGerichtValues (): string[] {
    if (this.termine == undefined) return []
    return Array.from(new Set(this.termine.map(t => t.gericht))).sort((d1, d2) => d1 > d2 ? 1 : -1)
  }

  GetSaalValues (): string[] {
    if (this.termine == undefined) return []
    return Array.from(new Set(this.termine.map(t => t.sitzungssaal))).sort((d1, d2) => d1 > d2 ? 1 : -1)
  }

  loadTermine () {
    this.terminService.getAllTermine()
      .subscribe(termine => {
        this.termine = this.sortTermine(termine)
        this.InitSaalSelection()
        this.InitGerichtSelection()
        this.filterTermine()
        this.loadingTermine = false
      },
      err => {
        console.error('Termine konnten nicht geladen werden: ', err)
      })
  }

  loadBasicPermissions () {
    this.basicPermission = { allowTermine: Restriction.read }
    this.permissionService.getBasicPermissions()
      .subscribe(perm => {
        this.basicPermission = perm
      },
      err => {
        console.error('Berechtigungen konnten nicht geladen werden: ', err)
      })
  }

  InitSaalSelection () {
    this.GetSaalValues().forEach(element => {
      const sel: boolean = (element == this.routedSaal)
      this.selSaal.push(sel)
    })
  }

  updateClick () {
    this.loadTermine()
  }

  addNewTerminClick () {
    // Neuen Termin anlegen
    let termin: Termin
    termin = new Termin()
    termin.id = -1
    this.terminDialog.open(termin)
  }

  InitGerichtSelection () {
    this.GetGerichtValues().forEach(element => {
      const sel: boolean = (element == this.routedSaal)
      this.selGericht.push(sel)
    })
  }

  onResize (event) {
    this.DataGridTermine.resize()
  }

  ngOnInit () {
    this.route.params.subscribe(params => {
      if (params.saal != undefined) {
        this.routedSaal = params.saal
      }
    })
    this.loadBasicPermissions()
    this.loadTermine()
  }

  sortTermine (termine: Termin[]): Termin[] {
    return termine.sort((t1, t2) => {
      if (t1.uhrzeitAktuell > t2.uhrzeitAktuell) {
        return 1
      } else if (t1.uhrzeitAktuell < t2.uhrzeitAktuell) {
        return -1
      }
      return 0
    })
  }
}
