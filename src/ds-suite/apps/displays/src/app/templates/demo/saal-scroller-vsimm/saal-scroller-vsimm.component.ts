import { Component, OnInit, ViewChild, ViewChildren, ElementRef, QueryList  } from '@angular/core';
import { trigger, state, style, animate, transition, stagger, query, keyframes } from '@angular/animations';
import { Subscription } from 'rxjs/Subscription';
import { timer } from 'rxjs';

import { DisplayTemplateComponent } from '../../../display-template/display-template.component';

//import { Termin, Objekt } from '@ds-suite/model';
import { Termin, Objekt, TerminStatus } from '@ds-suite/model';

function isNullOrUndefined (value) {
  return value === undefined || value === null
}
@Component({
  selector: 'app-saal-scroller-vsimm',
  templateUrl: './saal-scroller-vsimm.component.html',
  styleUrls: ['./saal-scroller-vsimm.component.css'],
  animations: [
    trigger('terminAnimation', [
      state('in', style({ opacity: 1, height: '160px', 'padding-top': '*' })),
      transition('in => void', [
        animate(
          '2s ease-out',
          keyframes([
            style({ opacity: 0, offset: 0.3, 'padding-top': 0 }),
            style({ height: 0, offset: 1 })
          ])
        )
      ])
    ]),
    trigger('objektAnimation', [
      state('in', style({ opacity: 1, height: '236px', 'margin-top': '*' })),
      transition('in => void', [
        animate(
          '1.9s linear',
          keyframes([
            style({ 'margin-top': '0', offset: 0 }),
            style({ 'margin-top': '-236px', opacity: 0.7, offset: 0.95 }),
            style({ opacity: 0, 'margin-top': '0', height: '0', offset: 1 }),
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

  naechsterTerminVSIMM: Termin;
  uebernaechsterTerminVSIMM: Termin;

  objects: Objekt[] = [];
  objectsCount: number = 0;

  @ViewChild('dsObjectsContainer') dsObjectsContainer: ElementRef;
  @ViewChildren('dsObjectsChild') dsObjectsChildren: QueryList<ElementRef>;

  ngOnInit() {
    this.updateInterval=6000;
    super.ngOnInit();
    this.initializeObjectScrolling();
  }

  initializeObjectScrolling() {
    this.ScrollIntervallObjects = 1850;
    this.ScrollIntervallTimer = timer(1850, this.ScrollIntervallObjects);
    this.ScrollIntervallSub = this.ScrollIntervallTimer.subscribe((t: any) => {
      if (this.objects.length <=1 ||
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

    // wenn sich der Termin ändert, müssen die Objekte neu geladen werden
    if (isNullOrUndefined(this.aktiverTermin) || this.lastActiveTerminID != this.aktiverTermin.id) {
      this.objects= [];
      this.loadObjects();
    }

    this.lastActiveTerminID = -1;
    if (!isNullOrUndefined(this.aktiverTermin))
      this.lastActiveTerminID = this.aktiverTermin.id;

    if(!isNullOrUndefined(this.termineOffen)) {
      this.naechsterTerminVSIMM = this.termineOffen.length>1 ? this.termineOffen[1] : null;
      this.uebernaechsterTerminVSIMM = this.termineOffen.length>2 ? this.termineOffen[2] : null;
    }

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
