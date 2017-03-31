import 'rxjs/add/operator/switchMap';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';

import { DisplayService } from './display.service';
import { Display } from './display';

@Component({
  selector: 'app-display',
  templateUrl: './display.component.html',
  styleUrls: ['./display.component.css'],
  providers: [DisplayService]
})
export class DisplayComponent implements OnInit {
  display: Display;

  constructor(
    private displayService: DisplayService,
    private route: ActivatedRoute
    ) { }

  loadDisplay() {
    this.route.params
      .switchMap((params: Params) => this.displayService.getDisplay(params['name']))
      .subscribe(display => this.display = display);
  }

  ngOnInit() {
    this.loadDisplay();
  }
}
