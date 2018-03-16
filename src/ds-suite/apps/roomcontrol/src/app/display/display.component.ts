import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Params } from "@angular/router";

import 'rxjs/add/operator/switchMap';

import { Display, Termin } from "@ds-suite/model";
import { DisplayService, TerminService } from "@ds-suite/core";

@Component({
  selector: "app-display",
  templateUrl: "./display.component.html",
  styleUrls: ["./display.component.css"]
})
export class DisplayComponent implements OnInit {
  display: Display;
  termine: Termin[] = [];

  constructor(
    private displayService: DisplayService,
    private terminService: TerminService,
    private route: ActivatedRoute
  ) {}

  loadDisplay() {
    console.log(this.route.paramMap);
    this.route.params
      .switchMap((params: Params) =>
        this.displayService.getDisplay(params["name"])
      )
      .subscribe(display => {
        this.display = display;
      });
  }

  loadTermine() {
    // todo:
  }

  ngOnInit() {
    this.loadDisplay();
  }
}
