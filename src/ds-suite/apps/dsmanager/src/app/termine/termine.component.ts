import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Display } from '@ds-suite/model';
import { DisplayService } from '@ds-suite/core';

import { Termin } from '@ds-suite/model';
import { TerminStatus } from '@ds-suite/model';
import { TerminService } from '@ds-suite/core';
import { ClrDatagrid } from '@clr/angular';

@Component({
  selector: 'termine',
  templateUrl: './termine.component.html',
  styleUrls: ['./termine.component.css']
})
export class TermineComponent implements OnInit {
  termine: Termin[];
  filteredTermine: Termin[];
  loadingTermine: boolean = true;
  public selStatus: boolean[] = [false, false, false, false, false];
  public selGericht: boolean[] = [];
  public selSaal: boolean[] = [];
  private routedSaal: string = "";
  @ViewChild('DataGridTermine') DataGridTermine: ClrDatagrid;

  constructor(private terminService: TerminService,
    private route: ActivatedRoute) { 
  }

  changeSelection() {
    //console.log("selStatus:",this.selStatus)
    //console.log("selGericht:",this.selGericht)
    //console.log("selSaal:",this.selSaal)
    this.filterTermine();
  }

  filterTermine() {
    this.filteredTermine=[];
    var i:number;
    var selStatus: string[] = [];
    var selGericht: string[] = [];
    var selSaal: string[] = [];

    var statusValues: string[] = this.GetStatusValues();
    var gerichtValues: string[] = this.GetGerichtValues();
    var saalValues: string[] = this.GetSaalValues();

    for (i=0; i<this.selStatus.length; i++) {
      if (this.selStatus[i]==true){
        selStatus.push(statusValues[i]);
      }
    }
    for (i=0; i<this.selGericht.length; i++) {
      if (this.selGericht[i]==true){
        selGericht.push(gerichtValues[i]);
      }
    }
    for (i=0; i<this.selSaal.length; i++) {
      if (this.selSaal[i]==true){
        selSaal.push(saalValues[i]);
      }
    }
    
    this.filteredTermine=this.termine.filter(t=> {
      if (selStatus.length>0 && selStatus.indexOf(t.status)<0)
        return false;
      if (selGericht.length>0 && selGericht.indexOf(t.gericht)<0)
        return false;
      if (selSaal.length>0 && selSaal.indexOf(t.sitzungssaal)<0)
        return false; 
      return true;
    });

  }

  GetStatusValues(forView:boolean=false) : Array<string> {
    if (this.termine==undefined) return [];
    var rval=Array.from(new Set(this.termine.map(t => t.status))).sort((d1, d2) => d1 > d2 ? 1 : -1);
    
    //für die Anzeige '' in 'offen' übersetzen
    if (forView) {
      var indOffen=rval.indexOf(TerminStatus.offen);
      if (indOffen>-1) {
        rval[indOffen]="Offen";
      }
    }
    
    return rval;
  }

  GetGerichtValues() : Array<string> {
    if (this.termine==undefined) return [];
    return  Array.from(new Set(this.termine.map(t => t.gericht))).sort((d1, d2) => d1 > d2 ? 1 : -1);
  }

  GetSaalValues() : Array<string> {
    if (this.termine==undefined) return [];
    return  Array.from(new Set(this.termine.map(t => t.sitzungssaal))).sort((d1, d2) => d1 > d2 ? 1 : -1);
  }
  
  loadTermine() {
    this.terminService.getAllTermine()
      .subscribe(termine => {
        this.termine = termine;
        this.InitSaalSelection();
        this.InitGerichtSelection();
        this.filterTermine()
        this.loadingTermine=false;
      },
      err => {
        console.error("Termine konnten nicht geladen werden: ",err);
      });
  }

  InitSaalSelection(){
    this.GetSaalValues().forEach(element => {
      var sel:boolean = (element==this.routedSaal);
      this.selSaal.push(sel);
    });
  }

  
  InitGerichtSelection(){
    this.GetGerichtValues().forEach(element => {
      var sel:boolean = (element==this.routedSaal);
      this.selGericht.push(sel);
    });
  }

  onResize(event) {
    this.DataGridTermine.resize();
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      if (params["saal"] != undefined) {
        this.routedSaal=params["saal"]
      }
    })
    this.loadTermine();
  }

}
