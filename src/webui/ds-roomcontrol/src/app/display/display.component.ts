import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';

import { DisplayService, Display } from 'ds-core';

@Component({
  selector: 'app-display',
  templateUrl: './display.component.html',
  styleUrls: ['./display.component.css']
})
export class DisplayComponent implements OnInit {
  display: Display;

  constructor(
    private displayService: DisplayService,
    private route: ActivatedRoute) { }

  loadDisplay() {
    this.route.params
      .switchMap((params: Params) => this.displayService.getDisplay(params['name']))
      .subscribe(display => {
        this.display = display;
      })
  }

  ngOnInit() {
    this.loadDisplay();
  }
}
