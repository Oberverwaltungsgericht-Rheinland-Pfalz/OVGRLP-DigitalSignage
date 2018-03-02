import { Component, OnInit } from '@angular/core';

import { Display } from '@ds-suite/model';
import { DisplayService } from '@ds-suite/backend';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  displays: Display[];

  constructor(private displayService: DisplayService) {}

  getDisplays() {
    this.displayService.getDisplays().subscribe(displays => (this.displays = displays));
  }

  ngOnInit() {
    this.getDisplays();
  }
}
