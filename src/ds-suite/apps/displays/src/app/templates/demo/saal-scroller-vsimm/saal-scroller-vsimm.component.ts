import { Component, OnInit } from '@angular/core';
import { trigger, state, style, animate, transition, stagger, query, keyframes } from '@angular/animations';

import { DisplayTemplateComponent } from '../../../display-template/display-template.component';

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
  ngOnInit() {
    this.updateInterval=4000;
    super.ngOnInit();
  }
}
