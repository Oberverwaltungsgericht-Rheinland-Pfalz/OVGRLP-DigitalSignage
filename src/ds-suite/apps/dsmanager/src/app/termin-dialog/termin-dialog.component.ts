import { Component,  EventEmitter, OnInit, Output, ViewChild } from '@angular/core';

import { Termin } from '@ds-suite/model';
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

  open(termin: Termin) {
    this.terminService.getTerminByBreeze(termin.id).then(item => {
      this.termin=item;
    });
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
    this.terminService.saveTerminByBreeze(this.termin).then(() => {
      this.dataChanged.emit();
      this.close(); 
    });
   this.show = false;
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
