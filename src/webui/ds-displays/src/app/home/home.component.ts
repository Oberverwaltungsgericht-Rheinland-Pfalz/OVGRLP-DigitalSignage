import { Component, OnInit } from "@angular/core";

import { Display } from "../model/display";
import { DisplayService } from "../service/display.service";

@Component({
  selector: "app-home",
  template: `
    <ul>
      <li *ngFor="let display of displays">
        <a [routerLink]="[display.name]">{{ display.name }}</a>
      </li>
    </ul>
  `,
  styles: [],
  providers: [DisplayService]
})
export class HomeComponent implements OnInit {
  displays: Display[];

  getDisplays() {
    this.displayService
      .getDisplays()
      .subscribe(displays => (this.displays = displays));
  }

  constructor(private displayService: DisplayService) {}

  ngOnInit() {
    this.getDisplays();
  }
}
