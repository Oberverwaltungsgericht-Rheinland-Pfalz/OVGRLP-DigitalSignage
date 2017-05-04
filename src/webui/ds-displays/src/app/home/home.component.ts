import { Component, OnInit } from '@angular/core';

import { Display } from 'ds-core';
import { DisplayService } from 'ds-core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers: [DisplayService]
})
export class HomeComponent implements OnInit {
  displays: Display[];

  getDisplays() {
    this.displayService.getDisplays()
      .subscribe(
        displays => this.displays = displays);
  }

  constructor(private displayService: DisplayService) { }

  ngOnInit() {
    this.getDisplays();
  }
}
