// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { Component, OnInit } from '@angular/core'

import { BasicPermissions } from '@ds-suite/model'
import { PermissionService } from '@ds-suite/core'
const { version: appVersion } = require('../../package.app.json')

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  appVersion: string = ''
  basicPermission: BasicPermissions

  constructor (private readonly permissionService: PermissionService) {}

  loadBasicPermissions () {
    this.basicPermission = { allowDisplays: true }
    this.permissionService.getBasicPermissions()
      .subscribe(perm => {
        this.basicPermission = perm
      },
      err => {
        console.error('Berechtigungen konnten nicht geladen werden: ', err)
      })
  }

  ngOnInit () {
    this.loadBasicPermissions()
    this.appVersion = appVersion
  }
}
