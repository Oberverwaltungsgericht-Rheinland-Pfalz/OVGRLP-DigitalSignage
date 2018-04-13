import { Component, OnInit } from '@angular/core';
import { trigger, state, style, animate, transition, stagger, query, keyframes } from '@angular/animations';

import { Termin, Display } from '@ds-suite/model';
import { DisplayTemplateComponent } from '../../../display-template/display-template.component';

@Component({
  selector: 'app-njz-kh-foyer-wappen',
  templateUrl: './njz-kh-foyer-wappen.component.html',
  styleUrls: ['./njz-kh-foyer-wappen.component.css'],
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
export class NjzKhFoyerWappenComponent extends DisplayTemplateComponent { 
  ngOnInit() {
    //this.updateInterval = 1000;

    super.ngOnInit();
  }
}
