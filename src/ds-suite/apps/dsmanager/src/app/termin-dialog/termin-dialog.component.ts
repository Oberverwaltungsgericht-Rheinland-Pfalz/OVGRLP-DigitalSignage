import { Component, OnInit } from '@angular/core';

import { Termin } from '@ds-suite/model';

@Component({
  selector: 'termin-dialog',
  templateUrl: './termin-dialog.component.html',
  styleUrls: ['./termin-dialog.component.css']
})
export class TerminDialogComponent implements OnInit {
  public termin: Termin;
  public origTermin: Termin;
  public show: boolean = false;

  constructor() { }

  open(termin: Termin) {
    this.origTermin = termin;
    this.termin = (JSON.parse(JSON.stringify(termin)));  /*Hack: kopie von Objekt termin*/
    this.show = true;
  }

  addItemClick(arr: Array<string>) {

  }
  
  deleteItemClick(arr: Array<string>, element: string) {
    var index = arr.indexOf(element, 0);
    if (index > -1) {
      arr.splice(index, 1);
    }
  }

  close() {
    this.show = false;
  }

  ngOnInit() {
    console.log(this.termin)
  }

}
