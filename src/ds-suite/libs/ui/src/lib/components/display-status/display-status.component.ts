// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { Component, OnInit, Input } from '@angular/core'
import { DisplayStatus } from '@ds-suite/model'

@Component({
  selector: 'display-status',
  templateUrl: './display-status.component.html',
  styleUrls: ['./display-status.component.css']
})
export class DisplayStatusComponent implements OnInit {
  _status: DisplayStatus
  _description: string

  @Input()
  set status (status: DisplayStatus) { this._status = status }

  get status (): DisplayStatus { return this._status }

  @Input()
  set description (description: string) { this._description = description }

  get description (): string { return this._description }

  constructor () { }

  ngOnInit () {
  }

  DisplayStatusToString (stat: DisplayStatus): string {
    let rval: string = ''
    switch (stat) {
      case DisplayStatus.Unknown:
        rval = 'unbekannt'
        break
      case DisplayStatus.Active:
        rval = 'aktiv'
        break
      case DisplayStatus.Online:
        rval = 'angeschaltet'
        break
      case DisplayStatus.Offline:
        rval = 'ausgeschaltet'
        break
    }
    return rval
  }
}
