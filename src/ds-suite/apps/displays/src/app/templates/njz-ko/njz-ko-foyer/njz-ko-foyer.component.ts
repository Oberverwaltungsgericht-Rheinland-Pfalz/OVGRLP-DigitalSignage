import { Component, OnInit } from '@angular/core';
import { trigger, state, style, animate, transition, stagger, query, keyframes } from '@angular/animations';

import { DisplayTemplateComponent } from '../../../display-template/display-template.component';

import { Termin } from '@ds-suite/model';

@Component({
  selector: 'app-njz-foyer',
  templateUrl: './njz-ko-foyer.component.html',
  styleUrls: ['./njz-ko-foyer.component.css'],
  animations: [
    trigger('terminAnimation', [
      state('in', style({ opacity: 1, height: '*', 'padding-top': '*' })),
      transition('in => void', [
        animate('2s ease-out',
          keyframes([
            style({ opacity: 0, offset: 0.3, 'padding-top': 0 }),
            style({ height: 0, offset: 1 })
          ])
        )
      ])
    ])
  ]
})
export class NjzKoFoyerComponent extends DisplayTemplateComponent {
  isFlughafenanzeige(): boolean {
    return this.display.title === 'Neues Justizzentrum Koblenz';
  }

  public ParteiOhneVertreten(name: string): string {
    var rval: string = name;
    if (name.includes(', vertreten durch')) {
      rval = name.substring(0, name.indexOf(', vertreten durch'));
    }
    if (name.includes(' vertreten durch')) {
      rval = name.substring(0, name.indexOf(' vertreten durch'));
    }
    return rval;
  }

  public KumulierteTitel(termine: Termin[]): string[] {
    return Array.from(new Set(termine.map(t => t.gericht)));
  }
}


