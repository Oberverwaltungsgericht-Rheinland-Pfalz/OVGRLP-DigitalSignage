import { Component, OnInit } from '@angular/core';

import { Display } from '../display/display';
import { DisplayService } from '../display/display.service';

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
