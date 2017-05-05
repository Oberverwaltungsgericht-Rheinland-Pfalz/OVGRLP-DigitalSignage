import { Component, OnInit } from '@angular/core';

import { Display, DisplayService } from 'ds-core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers: [DisplayService]
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
