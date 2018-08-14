import { Component, OnInit } from '@angular/core';
const { version: appVersion } = require('../../package.app.json')

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  appVersion: string = "";

  constructor() {}

  ngOnInit() {
    this.appVersion = appVersion;
  }
}
