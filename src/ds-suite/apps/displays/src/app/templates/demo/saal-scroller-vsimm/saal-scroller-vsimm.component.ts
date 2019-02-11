import { Component, OnInit, ViewChild, ViewChildren, ElementRef, QueryList  } from '@angular/core';
import { trigger, state, style, animate, transition, stagger, query, keyframes } from '@angular/animations';
import { Subscription } from 'rxjs/Subscription';
import { Observable } from 'rxjs/Observable';

import { DisplayTemplateComponent } from '../../../display-template/display-template.component';
import { Termin, Objekt } from '@ds-suite/model';
import { isNullOrUndefined } from 'util';

@Component({
  selector: 'app-saal-scroller-vsimm',
  templateUrl: './saal-scroller-vsimm.component.html',
  styleUrls: ['./saal-scroller-vsimm.component.css'],
  animations: [
    trigger('terminAnimation', [
      state('in', style({ opacity: 1, height: '*', 'padding-top': '*' })),
      transition('in => void', [
        animate(
          '2s ease-out',
          keyframes([
            style({ opacity: 0, offset: 0.3, 'padding-top': 0 }), 
            style({ height: 0, offset: 1 })
          ])
        )
      ])
    ])
  ]
})
export class SaalScrollerVsimmComponent extends DisplayTemplateComponent {
  public ScrollingObjectsActive: boolean;
  private ScrollIntervallObjects: number;
  private ScrollIntervallTimer: any;
  private ScrollIntervallSub: Subscription;
  private lastActiveTerminID: number = -1;

  objects: Objekt[] = [];
  objectsCount: number = 0;

  @ViewChild('dsObjectsContainer') dsObjectsContainer: ElementRef;
  @ViewChildren('dsObjectsChild') dsObjectsChildren: QueryList<ElementRef>;

  ngOnInit() {
    this.updateInterval=4000;
    super.ngOnInit();
    this.initializeObjectScrolling();
  }

  initializeObjectScrolling() {
    this.ScrollIntervallObjects = 4000;
    this.ScrollIntervallTimer = Observable.timer(2000, this.ScrollIntervallObjects);
    this.ScrollIntervallSub = this.ScrollIntervallTimer.subscribe((t: any) => {
      if (this.objects.length === 0 ||
        (this.ScrollingObjectsActive && this.objects.length <= this.objectsCount) ||
        (!this.ScrollingObjectsActive)) {
        this.loadObjects();
      } else {
        if (this.ScrollingObjectsActive)
          this.objects.shift();
      }
    });
  }

  isScrollingObjectsActive(): boolean {
    if (this.dsObjectsContainer && this.dsObjectsChildren) {
      const containerHeight = this.dsObjectsContainer.nativeElement.offsetHeight;
      var childrenHeight = 0;

      this.dsObjectsChildren.forEach(element => {
        childrenHeight += element.nativeElement.offsetHeight;
      });

      return containerHeight < childrenHeight;
    } else {
      return false;
    }
  }

  termineLoaded() {
    super.termineLoaded();
    this.ScrollingObjectsActive = this.isScrollingObjectsActive();
    this.objectsCount = 0;
    if (!isNullOrUndefined(this.aktiverTermin) && !isNullOrUndefined(this.aktiverTermin.objekte)) 
      this.objectsCount = this.aktiverTermin.objekte.length;
    if (isNullOrUndefined(this.aktiverTermin) || this.lastActiveTerminID != this.aktiverTermin.id) {
      this.objects= [];
      this.loadObjects();
    }
    
    this.lastActiveTerminID = -1;
    if (!isNullOrUndefined(this.aktiverTermin))
      this.lastActiveTerminID = this.aktiverTermin.id;
  }

  loadObjects() {
    var objects: Objekt[] = [];
    if (!isNullOrUndefined(this.aktiverTermin) && !isNullOrUndefined(this.aktiverTermin.objekte))
      objects=this.aktiverTermin.objekte;

    if (this.ScrollingObjectsActive)
      this.objects = this.objects.concat(objects);
    else
      this.objects = objects;
  }

  ngOnDestroy() {
    this.ScrollIntervallSub.unsubscribe();
    super.ngOnDestroy;
  }

}
