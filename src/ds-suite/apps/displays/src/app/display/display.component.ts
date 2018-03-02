import { Component, AfterViewInit, OnDestroy, ViewChild, ComponentFactoryResolver } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';

import { Observable, Subscription } from 'rxjs/Rx';
import 'rxjs/add/operator/switchMap';

import { Display } from '@ds-suite/model';
import { DisplayService } from '@ds-suite/backend';

import { DisplayTemplateComponent } from './../display-template/display-template.component';
import { TemplateHostDirective } from './../display-template/template-host.directive';
import { TEMPLATES } from './../display-template/templates';

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

  @ViewChild(TemplateHostDirective) templateHost: TemplateHostDirective;

  constructor(
    private displayService: DisplayService,
    private route: ActivatedRoute,
    private componentFactoryResolver: ComponentFactoryResolver
  ) {}

  loadDisplay() {
    this.route.params
      .switchMap((params: Params) => this.displayService.getDisplay(params['name']))
      .subscribe(display => {
        if (!this.display || display.template != this.display.template) {
          const templateName = display.template;
          let viewContainer = this.templateHost.viewContainerRef;
          viewContainer.clear();

          let component = TEMPLATES.filter(item => {
            return item.name === templateName;
          })[0];

          if (component) {
            let factory = this.componentFactoryResolver.resolveComponentFactory(component);
            let componentRef = viewContainer.createComponent(factory);
            this.currentTemplate = <DisplayTemplateComponent>componentRef.instance;
            this.currentTemplate.display = display;
            console.log(this.currentTemplate);
          }
        }

        this.display = display;
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
