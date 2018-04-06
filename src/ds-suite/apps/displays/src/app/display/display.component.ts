import { Component, AfterViewInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';

import { Observable } from 'rxjs/Observable';
import { Subscription } from 'rxjs/Subscription';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/observable/timer';

import { Display } from '@ds-suite/model';
import { DisplayService } from '@ds-suite/core';

import { DisplayTemplateComponent } from './../display-template/display-template.component';
import { TemplateHostDirective } from './../display-template/template-host.directive';

@Component({
  selector: 'app-display',
  templateUrl: './display.component.html',
  styleUrls: ['./display.component.css']
})
export class DisplayComponent implements AfterViewInit, OnDestroy {
  private updateTimer: any;
  private updateSub: Subscription;
  display: Display;
  currentTemplate: DisplayTemplateComponent;

  constructor(
    private displayService: DisplayService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  loadDisplay() {
    this.route.params
      .switchMap((params: Params) => this.displayService.getDisplay(params['name']))
      .subscribe(display => {
        this.display = display;
        this.router.navigate([display.template, display], { relativeTo: this.route, skipLocationChange: true });
      });
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
