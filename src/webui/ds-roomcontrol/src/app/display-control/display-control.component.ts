import { Component, Input, OnInit } from '@angular/core';

import { Display } from 'ds-core';

@Component({
  selector: 'app-display-control',
  templateUrl: './display-control.component.html',
  styleUrls: ['./display-control.component.css']
})
export class DisplayControlComponent implements OnInit {

  constructor() { }

  @Input() display: Display;

  ngOnInit() {
  }

}
