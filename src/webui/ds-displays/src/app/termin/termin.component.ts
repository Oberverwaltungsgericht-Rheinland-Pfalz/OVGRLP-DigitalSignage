import { Component, Input, OnInit } from '@angular/core';

import { Termin } from 'ds-core';

@Component({
  selector: 'app-termin',
  templateUrl: './termin.component.html',
  styleUrls: ['./termin.component.css']
})
export class TerminComponent implements OnInit {

  constructor() { }

  @Input() termin: Termin;

  ngOnInit() {
  }

}
