import { Component, OnInit } from '@angular/core';
const { version: appVersion } = require('../../package.app.json');

import { BasicPermissions } from '@ds-suite/model';
import { PermissionService } from '@ds-suite/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  appVersion: string = "";
  basicPermission: BasicPermissions;

  constructor(private permissionService: PermissionService) {}

  loadBasicPermissions() {
    this.basicPermission = {allowDisplays:true};
    this.permissionService.getBasicPermissions()
      .subscribe(perm => {
        this.basicPermission = perm;
      },
      err => {
        console.error("Berechtigungen konnten nicht geladen werden: ",err);
      });
  }

  ngOnInit() {
    this.loadBasicPermissions();
    this.appVersion = appVersion;
  }
}