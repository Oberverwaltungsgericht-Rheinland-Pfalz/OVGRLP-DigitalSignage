import { Component, OnInit, OnDestroy, Input, ViewChild, ViewChildren, ElementRef, QueryList } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { Subscription } from 'rxjs/Subscription';

import { Termin, TerminStatus, Display } from '@ds-suite/model';
import { TerminService } from '@ds-suite/core';

import 'rxjs/add/operator/switchMap';
import { filter } from 'rxjs/operators/filter';

@Component({
  selector: 'app-display-template',
  templateUrl: './display-template.component.html',
  styleUrls: ['./display-template.component.css']
})
export class DisplayTemplateComponent implements OnInit, OnDestroy {
  @ViewChild('dsTermineContainer', { static: false }) dsTermineContainer: ElementRef;
  @ViewChildren('dsTermineChild') dsTermineChildren: QueryList<ElementRef>;

  private updateTimer: any;
  private updateSub: Subscription;

  public updateInterval = 10000;
  public SwitchMultipleActiveTermine: boolean = false;

  display: Display;
  aktiverTermin: Termin;
  activeTermine: Termin[] = [];
  activeTermineCount = 0;
  activeTermineIndex = 0;
  naechsterTermin: Termin;
  termine: Termin[] = [];
  termineOffen: Termin[] = [];
  termineCount = 0;
  datum: Date;
  scrollMode = false;

  constructor(private route: ActivatedRoute, private terminService: TerminService) { }

  loadTermine() {
    this.route.params.subscribe(params => {
      this.display = params as Display;

      this.terminService.getTermine(this.display.name).subscribe(result => {
        let tmpTermine: Termin[] = result;
  
        tmpTermine = this.filterTermine(tmpTermine);
        tmpTermine = this.sortTermine(tmpTermine);
        this.termineCount = tmpTermine.length;
        
        this.activeTermine = this.findActiveTermine(tmpTermine);
        this.activeTermineCount = this.activeTermine.length;
        this.activeTermineIndex++;
        if ((this.activeTermineIndex) > this.activeTermineCount)
          this.activeTermineIndex=1;
        
        this.aktiverTermin = this.findFirstActiveTermin(tmpTermine);
        if (this.SwitchMultipleActiveTermine) 
          this.aktiverTermin = this.activeTermine[this.activeTermineIndex-1];
        
        this.termineOffen = tmpTermine.filter(
          termin => !(termin.status === 'Abgeschlossen' || termin.status === 'Aufgehoben')
        );
        this.naechsterTermin = this.aktiverTermin ? null : this.termineOffen[0];
        this.scrollMode = this.isScrollMode();
  
        if (this.scrollMode)
          this.termine = this.termine.concat(tmpTermine);
        else
          this.termine = tmpTermine;
        this.termineLoaded();
      });
    });
  }

  termineLoaded() {
    /* hier sind die Termine bereits geladen */
  }

  private isScrollMode(): boolean {
    if (this.dsTermineContainer && this.dsTermineChildren) {
      const containerHeight = this.dsTermineContainer.nativeElement.offsetHeight;
      let childrenHeight = 0;

      this.dsTermineChildren.forEach(element => {
        childrenHeight += element.nativeElement.offsetHeight;
      });

      return containerHeight < childrenHeight;
    } else {
      return false;
    }
  }

  sortTermine(termine: Termin[]): Termin[] {
    return termine.sort((t1, t2) => {
      if (t1.uhrzeitAktuell > t2.uhrzeitAktuell) {
        return 1;
      } else if (t1.uhrzeitAktuell < t2.uhrzeitAktuell) {
        return -1;
      }
      return 0;
    });
  }

  filterTermine(termine: Termin[]): Termin[] {
    return termine.filter(termin => termin.uhrzeitAktuell !== 'omV');
  }

  findFirstActiveTermin(termine: Termin[]): Termin {
    return termine.find(termin => termin.status === 'Läuft');
  }

  findActiveTermine(termine: Termin[]): Termin[]  {
    return termine.filter(termin => termin.status === 'Läuft');
  }

  ngOnInit() {
    this.updateTimer = Observable.timer(2000, this.updateInterval);
    this.updateSub = this.updateTimer.subscribe((t: any) => {
      this.datum = new Date();
      if (this.termine.length === 0 ||
        (this.scrollMode && this.termine.length <= this.termineCount) ||
        (!this.scrollMode)) {
        this.loadTermine();
      } else {
        if (this.scrollMode)
          this.termine.shift();
      }
    });
  }

  removeFinishedTermine(termine: Termin[], maxTermine: number, minFinished: number) : Termin[] {
    var termineTmp = termine;

    if(termineTmp.length > maxTermine) {
      var termineFinished = termineTmp.filter(t => t.status == TerminStatus.abgeschlossen || t.status == TerminStatus.aufgehoben);
      var termineUnfinished = termineTmp.filter(t => t.status != TerminStatus.abgeschlossen && t.status != TerminStatus.aufgehoben);

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
    }

    return termineTmp;
  }

  animationStarted(event: AnimationEvent) {
  }

  ngOnDestroy() {
    this.updateSub.unsubscribe();
  }
}
