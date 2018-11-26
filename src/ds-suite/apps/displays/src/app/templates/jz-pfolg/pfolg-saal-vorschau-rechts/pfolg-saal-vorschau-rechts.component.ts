import { Component, OnInit } from '@angular/core';
import { trigger, state, style, animate, transition, stagger, query, keyframes } from '@angular/animations';

import { DisplayTemplateComponent } from '../../../display-template/display-template.component';

import { Termin, TerminStatus } from '@ds-suite/model';
const MAX_TERMINE = 3;
const MAX_FINISHED = 1;
const MAX_TERMINITEMS = 5;

@Component({
  selector: 'app-pfolg-saal-vorschau-rechts',
  templateUrl: './pfolg-saal-vorschau-rechts.component.html',
  styleUrls: ['./pfolg-saal-vorschau-rechts.component.css'],
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
export class PfolgSaalVorschauRechtsComponent extends DisplayTemplateComponent {

  ngOnInit() {
    
    super.ngOnInit();
  }

  showTermineStream(termine: Termin[], maxTermine: number, minFinished: number, maxTerminitems: number) : Termin[] {
    var termineTmp = termine;

    if(termineTmp.length > maxTermine) {
      var termineFinished = termineTmp.filter(t => t.status == TerminStatus.abgeschlossen);
      var termineUnfinished = termineTmp.filter(t => t.status != TerminStatus.abgeschlossen);

      if(termineFinished.length > minFinished) {
        
        // grundsätzlich nur noch die minimale Anzahl der erledigten Termine anzeigen,
        // wenn insgesamt mehr Termine vorhanden sind, als darstellbar sind
        var clearCount: number = termineFinished.length - minFinished;
        
        // Wenn jedoch möglich, die darstellbaren Termine wieder mit den erledigten füllen 
        // (mehr als die minimale Anzahl von Terminen)
        if ((termineFinished.length - clearCount + termineUnfinished.length)<maxTermine) 
          clearCount = (termineFinished.length + termineUnfinished.length - (maxTermine))
        
        // ggf. erledigte Termine rausfiltern
        if (clearCount > 0)
          termineFinished.splice(0, clearCount);
      }

      termineTmp = termineFinished.concat(termineUnfinished);

      if(termineTmp.length > maxTerminitems) {
        var subtraktor:number = termineTmp.length - (maxTerminitems - termineTmp.length);
        termineTmp.splice(maxTerminitems,subtraktor);
      }

    }

    return termineTmp;
  }

  filterTermine(termine: Termin[]) : Termin[] {
    return this.showTermineStream(
      super.sortTermine(super.filterTermine(termine)), 
      MAX_TERMINE, MAX_FINISHED, MAX_TERMINITEMS);
  }

}
