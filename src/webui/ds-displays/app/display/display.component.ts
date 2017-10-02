import { Component, AfterViewInit, OnDestroy, ViewChild, ComponentFactoryResolver } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Observable, Subscription } from 'rxjs/Rx';

import 'rxjs/add/operator/switchMap';

import { DisplayService, Display } from 'ds-core';

import { TemplateComponent } from './template.component';
import { TemplateHostDirective } from './template-host.directive';
import { TEMPLATES } from './templates/index';

@Component({
  selector: 'app-display',
  template: `
    <ng-template template-host></ng-template>
  `
})
export class DisplayComponent implements AfterViewInit, OnDestroy {

  private updateTimer: any;
  private updateSub: Subscription;
  display: Display;
  currentTemplate: TemplateComponent;

  @ViewChild(TemplateHostDirective) templateHost: TemplateHostDirective;

  constructor(
    private displayService: DisplayService,
    private route: ActivatedRoute,
    private componentFactoryResolver: ComponentFactoryResolver
  ) { }

  loadDisplay() {
    this.route.params
      .switchMap((params: Params) => this.displayService.getDisplay(params['name']))
      .subscribe(display => {
        const templateName = display.template; //TODO: read from display
        let viewContainer = this.templateHost.viewContainerRef;
        viewContainer.clear();

        let component = TEMPLATES.filter((item) => {
          return item.name === templateName;
        })[0];

        if (component) {
          let factory = this.componentFactoryResolver.resolveComponentFactory(component);
          let componentRef = viewContainer.createComponent(factory);
          this.currentTemplate = <TemplateComponent>componentRef.instance;
          this.currentTemplate.display = display;
        }

        this.display = display;
      });
  }

  ngAfterViewInit(): void {
    this.updateTimer = Observable.timer(5000, 60000);
    this.updateSub = this.updateTimer.subscribe((t: any) => {
      this.loadDisplay()
    });
  }

  ngOnDestroy() {
    this.updateSub.unsubscribe();
  }
}
