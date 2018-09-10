import { Component, OnInit } from '@angular/core';

import { Termin } from '@ds-suite/model';
import { TerminService } from '@ds-suite/core';
import { getTypeNameForDebugging } from '@angular/common/src/directives/ng_for_of';

@Component({
  selector: 'termin-dialog',
  templateUrl: './termin-dialog.component.html',
  styleUrls: ['./termin-dialog.component.css']
})
export class TerminDialogComponent implements OnInit {
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

  saveClick() {
    this.terminService.saveTerminByBreeze(this.termin).then(() => {
      this.close(); 
    });
   this.show = false;
  }

  close() {
    this.terminService.breezeEntityManager.clear();
    this.show = false;
  }

  ngOnInit() {
  }

}
