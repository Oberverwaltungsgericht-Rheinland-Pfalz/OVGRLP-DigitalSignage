import { Component, OnInit } from '@angular/core';

import { Display } from 'ds-core';
import { DisplayService } from 'ds-core';

@Component({
  selector: 'app-home',
  template: `
    <p>
      home works!
    </p>
    <ul>
      <li *ngFor="let display of displays">
        <a [routerLink]="[display.name]">{{ display.name }}</a>
      </li>
    </ul>
  `,
  styles: [''],
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
