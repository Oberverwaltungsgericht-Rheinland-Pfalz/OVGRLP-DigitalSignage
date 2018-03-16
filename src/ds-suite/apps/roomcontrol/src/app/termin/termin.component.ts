import { Component, Input, OnInit } from '@angular/core';

import { Termin } from '@ds-suite/model';
import { TerminService } from '@ds-suite/core';

@Component({
  selector: 'app-termin',
  templateUrl: './termin.component.html',
  styleUrls: ['./termin.component.css']
})
export class TerminComponent implements OnInit {

  constructor(
    private terminService: TerminService) {
  }

  ngOnInit() {
  }

  @Input()
  termin: Termin

}
