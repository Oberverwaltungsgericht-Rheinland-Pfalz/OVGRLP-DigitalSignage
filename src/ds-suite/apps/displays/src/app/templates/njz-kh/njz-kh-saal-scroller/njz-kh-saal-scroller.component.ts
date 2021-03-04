import { Component, OnInit, ViewChild, ViewChildren, ElementRef, QueryList  } from '@angular/core';
import { trigger, state, style, animate, transition, stagger, query, keyframes } from '@angular/animations';
import { Subscription } from 'rxjs/Subscription';
import { Observable } from 'rxjs/Observable';

import { DisplayTemplateComponent } from '../../../display-template/display-template.component';

import { Termin, Objekt } from '@ds-suite/model';

function isNullOrUndefined (value) {
  return value === undefined || value === null
}

@Component({
  selector: 'app-njz-kh-saal-scroller',
  templateUrl: './njz-kh-saal-scroller.component.html',
  styleUrls: ['./njz-kh-saal-scroller.component.css'],
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

export class NjzKhSaalScrollerComponent extends DisplayTemplateComponent {
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
    this.updateInterval=5000;
    super.ngOnInit();
  }
  termineLoaded() {
    super.termineLoaded();
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

  GetComposedObjects(termin: Termin): Objekt[] {
    var objects: Objekt[] = termin.objekte.map(x => Object.assign({}, x));    // deep copy der Objekte
    var compObjects: Objekt[] = [];
    var delimiter:string = "<br>"
    var delimiter2:string = ", "
    objects.forEach(obj=> {
      // Umbrücke durch HTML Umbrüche ersetzen (in HTML kann der Text als Innerhtml mit der safehtml-Pipe genutzt werden) 
      if (!isNullOrUndefined(obj.wirtschaftsart)) {
        obj.wirtschaftsart=obj.wirtschaftsart.replace("\\n",delimiter2);
      }

      // gibt es bereits ein Objekt mit dem blatt...
      var ind=compObjects.findIndex(o=>o.blatt==obj.blatt)
      if (ind>=0) {
        // ... wenn ja muss die Wirtschaftsart mit einem Umbruch dran gehangen werden
        var wirtsch = obj.wirtschaftsart;
        if (!isNullOrUndefined(compObjects[ind].wirtschaftsart)) {
          wirtsch=compObjects[ind].wirtschaftsart.concat(delimiter,wirtsch);
        }
        compObjects[ind].wirtschaftsart=wirtsch;
        var flurstueck = obj.flur;
        if (!isNullOrUndefined(compObjects[ind].flur)) {
          flurstueck=compObjects[ind].flur.concat(delimiter,flurstueck);
        }
        compObjects[ind].flur=flurstueck;
      }
      else {
        // ... wenn nicht muss das komplette Objekt aufgenommen werden
        compObjects.push(obj);
      }

    })
    return compObjects;
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
