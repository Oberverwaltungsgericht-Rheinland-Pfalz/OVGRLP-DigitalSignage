import { Component, Input, OnInit } from '@angular/core';

import { Termin } from 'ds-core';

@Component({
  selector: 'app-termine',
  templateUrl: './termine.component.html',
  styleUrls: ['./termine.component.css']
})
export class TermineComponent implements OnInit {

  constructor() { }

  @Input() termine: Termin[];

  ngOnInit() {
  }
}
