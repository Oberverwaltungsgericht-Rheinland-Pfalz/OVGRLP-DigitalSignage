import { Component, OnInit } from '@angular/core';

import { Termin } from '@ds-suite/model';
import { TerminService } from '@ds-suite/core';

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
    this.termin = (JSON.parse(JSON.stringify(termin)));  /*Hack: kopie von Objekt termin*/
    this.terminService.getTerminByBreeze(termin.id).then(item => {
      this.termin=item;
    });
    this.show = true;
  }

  addItemClick(arr: Array<string>) {

  }
  
  deleteItemClick(arr: Array<any>, element: any) {
    var index = arr.indexOf(element, 0);
    if (index > -1) {
      arr.splice(index, 1);
    }
  }

  saveClick() {
    this.terminService.saveTermin(this.termin).subscribe(val => { console.log("gespeichert:",this.termin); },
      err => {
        console.error(err);
      });
  }

  close() {
    this.show = false;
  }

  ngOnInit() {
  }

}
