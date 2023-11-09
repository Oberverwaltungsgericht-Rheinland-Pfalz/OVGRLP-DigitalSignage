// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { Component, OnInit } from '@angular/core';

import { Display } from '@ds-suite/model';
import { DisplayService } from '@ds-suite/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  displays: Display[];

  constructor(private displayService: DisplayService) { }

  getDisplays() {
    this.displayService.getDisplays()
      .subscribe(
        displays => this.displays = displays.sort((d1, d2) => d1.title > d2.title ? 1 : -1)
      );
  }

  ngOnInit() {
    this.getDisplays();
  }
}
