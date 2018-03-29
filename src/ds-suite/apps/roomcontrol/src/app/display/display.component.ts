import { Component, OnInit, AfterViewInit, OnDestroy } from "@angular/core";
import { ActivatedRoute, Params } from "@angular/router";

import { Observable } from 'rxjs/Observable';
import { Subscription } from 'rxjs/Subscription';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/observable/timer';

import { Display, Termin } from "@ds-suite/model";
import { DisplayService, TerminService } from "@ds-suite/core";

@Component({
  selector: "app-display",
  templateUrl: "./display.component.html",
  styleUrls: ["./display.component.css"]
})
export class DisplayComponent implements OnInit, OnDestroy, AfterViewInit {
  private updateTimer: any;
  private updateSub: Subscription;
  display: Display;
  termine: Termin[] = [];

  constructor(
    private displayService: DisplayService,
    private terminService: TerminService,
    private route: ActivatedRoute
  ) { }

  loadDisplay() {
    this.route.params
      .switchMap((params: Params) =>
        this.displayService.getDisplay(params["name"])
      )
      .subscribe(display => {
        this.display = display;
      });
  }

  ngOnInit() {
    this.loadDisplay();
  }

  ngAfterViewInit(): void {
    this.updateTimer = Observable.timer(5000, 60000);
    this.updateSub = this.updateTimer.subscribe((t: any) => {
      this.loadDisplay();
    });
  }

  ngOnDestroy() {
    this.updateSub.unsubscribe();
  }
}
