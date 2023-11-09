// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { Component, OnInit } from '@angular/core'

import { Display } from '@ds-suite/model'
import { DisplayService } from '@ds-suite/core'

const { version: appVersion } = require('../../../package.app.json')

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  displays: Display[]
  appVersion: string = ''

  constructor (private readonly displayService: DisplayService) {}

  getDisplays () {
    this.displayService.getDisplays().subscribe(displays => (this.displays = displays))
  }

  ngOnInit () {
    this.appVersion = appVersion
    this.getDisplays()
  }
}
