import { Component,  EventEmitter, OnInit, Output, ViewChild } from '@angular/core';

import { Termin, TerminStatus } from '@ds-suite/model';
import { TerminService } from '@ds-suite/core';
import { YesNoDialogComponent } from '@ds-suite/ui';

@Component({
  selector: 'termin-dialog',
  templateUrl: './termin-dialog.component.html',
  styleUrls: ['./termin-dialog.component.css']
})
export class TerminDialogComponent implements OnInit {
  @Output() dataChanged = new EventEmitter<void>();
  @ViewChild(YesNoDialogComponent) yesNoDialog: YesNoDialogComponent;
  public termin: any;
  public show: boolean = false;
 
  constructor(private terminService: TerminService) { }

  GetStatausValues() : Array<string> {
    return Object.values(TerminStatus)
  }

  open(termin: Termin) {
    this.terminService.getTerminByBreeze(termin.id).then(item => {
      this.termin=item;
      //ggf. Neuanlage...
      if (undefined==this.termin && termin.id==-1) {
        this.termin = this.terminService.breezeEntityManager.createEntity('Verfahren')
        console.log(this.termin)
      }
    })
    this.show = true;
  }

  addItemClick(arr: Array<any>,typeName:string) {
    arr.push(this.terminService.breezeEntityManager.createEntity(typeName))
  }
  
  deleteItemClick(arr: Array<any>, element: any) {
    var index = arr.indexOf(element);
    element.entityAspect.setDeleted();
    if (index > -1) {
      arr.splice(index, 1);
    }
  }

  deleteClick(){
    this.yesNoDialog.open();
  }

  saveClick() {
    this.checkNullFields();
    this.terminService.saveTerminByBreeze(this.termin).then(() => {
      this.dataChanged.emit();
      this.close(); 
    });
    this.show = false;
  }

  checkNullFields() {
    if (this.termin.Az==undefined || this.termin.Az==""){
      this.termin.Az=" ";
    }
    if (this.termin.Sitzungssaal==undefined || this.termin.Sitzungssaal==""){
      this.termin.Sitzungssaal=" ";
    }
    if (this.termin.UhrzeitPlan==undefined || this.termin.UhrzeitPlan==""){
      this.termin.UhrzeitPlan=" ";
    }
    if (this.termin.UhrzeitAktuell==undefined || this.termin.UhrzeitAktuell==""){
      this.termin.UhrzeitAktuell=" ";
    }
    if (this.termin.Oeffentlich==undefined || this.termin.Oeffentlich==""){
      this.termin.Oeffentlich=" ";
    }
    if (this.termin.Gegenstand==undefined || this.termin.Gegenstand==""){
      this.termin.Gegenstand=" ";
    }
    if (this.termin.Art==undefined || this.termin.Art==""){
      this.termin.Art=" ";
    }
  }

  changeOeffentlich(termin: any) {
    if(termin.Oeffentlich === 'ja')
      termin.Oeffentlich = 'nein'
    else
      termin.Oeffentlich = 'ja'
  }

  close() {
    this.terminService.breezeEntityManager.clear();
    this.show = false;
  }

  ngOnInit() {
  }

  OnDeleteResult(result:boolean) {
    if (result) {
      this.terminService.deleteTerminByBreeze(this.termin).then(() => {
        this.dataChanged.emit();
        this.close(); 
      });
    }
  }

}
